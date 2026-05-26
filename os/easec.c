#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <stdarg.h>

#ifdef _MSC_VER
#define strcasecmp _stricmp
#include <sys/timeb.h>
#include <windows.h>
#else
#include <strings.h>
#include <sys/time.h>
#endif
#include <time.h>

// ============================================================================
// CROSS-PLATFORM TIME & SLEEP
// ============================================================================
long long get_time_ms() {
#ifdef _MSC_VER
    struct __timeb64 timebuffer;
    _ftime64(&timebuffer);
    return (long long)(timebuffer.time) * 1000 + timebuffer.millitm;
#else
    struct timeval tv;
    gettimeofday(&tv, NULL);
    return (long long)(tv.tv_sec) * 1000 + (tv.tv_usec) / 1000;
#endif
}

void sleep_ms(long long ms) {
#ifdef _MSC_VER
    Sleep((DWORD)ms);
#else
    struct timespec ts;
    ts.tv_sec = ms / 1000;
    ts.tv_nsec = (ms % 1000) * 1000000;
    nanosleep(&ts, NULL);
#endif
}

// ============================================================================
// MEMORY HANDLING & BOUNDS CHECKING
// ============================================================================
size_t bytes_allocated = 0;

void* safe_alloc(size_t size) {
    size_t* ptr = (size_t*)malloc(size + sizeof(size_t));
    if (!ptr) { fprintf(stderr, "Fatal: Out of memory.\n"); exit(1); }
    memset(ptr + 1, 0, size);
    *ptr = size;
    bytes_allocated += size;
    return (void*)(ptr + 1);
}

void safe_free(void* p) {
    if (!p) return;
    size_t* ptr = (size_t*)p - 1;
    bytes_allocated -= *ptr;
    free(ptr);
}

void* safe_realloc(void* p, size_t new_size) {
    if (!p) return safe_alloc(new_size);
    if (new_size == 0) { safe_free(p); return NULL; }
    size_t* ptr = (size_t*)p - 1;
    size_t old_size = *ptr;
    size_t* new_ptr = (size_t*)realloc(ptr, new_size + sizeof(size_t));
    if (!new_ptr) { fprintf(stderr, "Fatal: Out of memory.\n"); exit(1); }
    *new_ptr = new_size;
    bytes_allocated -= old_size;
    bytes_allocated += new_size;
    return (void*)(new_ptr + 1);
}

char* safe_strdup(const char* s) {
    if (!s) return NULL;
    size_t len = strlen(s) + 1;
    char* copy = (char*)safe_alloc(len);
    memcpy(copy, s, len);
    return copy;
}

// ============================================================================
// AST MEMORY ARENA (PREVENTS PERMANENT AST LEAKS)
// ============================================================================
typedef struct ArenaBlock {
    char data[65536];
    int offset;
    struct ArenaBlock* next;
} ArenaBlock;
ArenaBlock* arena = NULL;

void* ast_alloc(size_t size) {
    size = (size + 7) & ~7; // align
    if (!arena || arena->offset + size > 65536) {
        ArenaBlock* block = (ArenaBlock*)malloc(sizeof(ArenaBlock));
        block->offset = 0;
        block->next = arena;
        arena = block;
    }
    void* ptr = arena->data + arena->offset;
    arena->offset += size;
    return ptr;
}

char* ast_strdup(const char* s) {
    if (!s) return NULL;
    size_t len = strlen(s) + 1;
    char* copy = (char*)ast_alloc(len);
    memcpy(copy, s, len);
    return copy;
}

void* ast_realloc(void* ptr, size_t old_size, size_t new_size) {
    if (new_size == 0) return NULL;
    void* new_ptr = ast_alloc(new_size);
    if (ptr && old_size > 0) memcpy(new_ptr, ptr, old_size);
    return new_ptr;
}

#define AST_REALLOC_ARRAY(ptr, type, old_count, new_count) \
    (type*)ast_realloc(ptr, sizeof(type) * (old_count), sizeof(type) * (new_count))

void free_ast() {
    ArenaBlock* curr = arena;
    while (curr) {
        ArenaBlock* next = curr->next;
        free(curr);
        curr = next;
    }
    arena = NULL;
}

// ============================================================================
// TYPES & GC OBJECTS
// ============================================================================
typedef enum { VAL_NULL, VAL_BOOL, VAL_INT, VAL_FLOAT, VAL_OBJ } ValType;
typedef enum { OBJ_STRING, OBJ_ARRAY, OBJ_DICT, OBJ_JOB, OBJ_MODULE, OBJ_ENV } ObjType;

typedef struct sObject {
    ObjType type;
    int marked;
    int is_constant;
    struct sObject* next;
} Object;

typedef struct {
    ValType type;
    union {
        int boolean;
        long long integer;
        double floating;
        Object* obj;
    } as;
} Value;

typedef struct sEnv {
    Object obj;
    struct { char* name; Value val; }* entries;
    int count;
    int capacity;
    struct sEnv* parent;
} Env;

typedef struct { Object obj; char* chars; } ObjString;
typedef struct { Object obj; Value* items; int capacity; int count; } ObjArray;
typedef struct { char* key; Value val; } DictEntry;
typedef struct { Object obj; DictEntry* entries; int capacity; int count; } ObjDict;
typedef struct { Object obj; char* name; char** params; int param_count; struct sStmt** body; int body_count; Env* closure; } ObjJob;
typedef struct { Object obj; Env* env; } ObjModule;

// ============================================================================
// VIRTUAL MACHINE & GARBAGE COLLECTION
// ============================================================================
typedef struct {
    Object* objects;
    Env** env_stack;
    int env_count;
    int env_capacity;
    size_t next_gc;
    int gc_paused;
} VM;

VM vm;

void init_vm() {
    vm.objects = NULL;
    vm.env_capacity = 64;
    vm.env_stack = (Env**)safe_alloc(sizeof(Env*) * vm.env_capacity);
    vm.env_count = 0;
    vm.next_gc = 1024 * 1024; // 1 MB
    vm.gc_paused = 0;
}

void mark_value(Value val);
void mark_env(Env* env) {
    if (!env || env->obj.marked) return;
    env->obj.marked = 1;
    for (int i = 0; i < env->count; i++) mark_value(env->entries[i].val);
    if (env->parent) mark_env(env->parent);
}

void mark_object(Object* obj) {
    if (!obj || obj->marked) return;
    obj->marked = 1;
    if (obj->type == OBJ_ARRAY) {
        ObjArray* arr = (ObjArray*)obj;
        for (int i = 0; i < arr->count; i++) mark_value(arr->items[i]);
    } else if (obj->type == OBJ_DICT) {
        ObjDict* dict = (ObjDict*)obj;
        for (int i = 0; i < dict->count; i++) mark_value(dict->entries[i].val);
    } else if (obj->type == OBJ_MODULE) {
        mark_env(((ObjModule*)obj)->env);
    } else if (obj->type == OBJ_JOB) {
        mark_env(((ObjJob*)obj)->closure);
    } else if (obj->type == OBJ_ENV) {
        mark_env((Env*)obj);
    }
}

void mark_value(Value val) {
    if (val.type == VAL_OBJ) mark_object(val.as.obj);
}

void gc_collect() {
    if (vm.gc_paused) return;
    
    for (int i = 0; i < vm.env_count; i++) {
        mark_object((Object*)vm.env_stack[i]);
    }
    
    Object** object = &vm.objects;
    while (*object != NULL) {
        if (!(*object)->marked && !(*object)->is_constant) {
            Object* unreached = *object;
            *object = unreached->next;
            
            if (unreached->type == OBJ_STRING) {
                safe_free(((ObjString*)unreached)->chars);
            } else if (unreached->type == OBJ_ARRAY) {
                safe_free(((ObjArray*)unreached)->items);
            } else if (unreached->type == OBJ_DICT) {
                ObjDict* dict = (ObjDict*)unreached;
                for (int i = 0; i < dict->count; i++) safe_free(dict->entries[i].key);
                safe_free(dict->entries);
            } else if (unreached->type == OBJ_ENV) {
                Env* env = (Env*)unreached;
                for (int i = 0; i < env->count; i++) safe_free(env->entries[i].name);
                safe_free(env->entries);
            }
            
            safe_free(unreached);
        } else {
            (*object)->marked = 0;
            object = &(*object)->next;
        }
    }
    vm.next_gc = bytes_allocated * 2;
}

Object* allocate_object(size_t size, ObjType type) {
    if (bytes_allocated > vm.next_gc) gc_collect();
    Object* obj = (Object*)safe_alloc(size);
    obj->type = type;
    obj->marked = 0;
    obj->is_constant = 0;
    obj->next = vm.objects;
    vm.objects = obj;
    return obj;
}

Value make_string(const char* chars) {
    ObjString* str = (ObjString*)allocate_object(sizeof(ObjString), OBJ_STRING);
    str->chars = safe_strdup(chars);
    Value v; v.type = VAL_OBJ; v.as.obj = (Object*)str;
    return v;
}

Value make_array() {
    ObjArray* arr = (ObjArray*)allocate_object(sizeof(ObjArray), OBJ_ARRAY);
    arr->items = NULL; arr->capacity = 0; arr->count = 0;
    Value v; v.type = VAL_OBJ; v.as.obj = (Object*)arr;
    return v;
}

Value make_dict() {
    ObjDict* dict = (ObjDict*)allocate_object(sizeof(ObjDict), OBJ_DICT);
    dict->entries = NULL; dict->capacity = 0; dict->count = 0;
    Value v; v.type = VAL_OBJ; v.as.obj = (Object*)dict;
    return v;
}

Value make_null() { Value v; v.type = VAL_NULL; return v; }
Value make_bool(int b) { Value v; v.type = VAL_BOOL; v.as.boolean = b; return v; }
Value make_int(long long i) { Value v; v.type = VAL_INT; v.as.integer = i; return v; }
Value make_float(double f) { Value v; v.type = VAL_FLOAT; v.as.floating = f; return v; }

Env* create_env(Env* parent) {
    Env* env = (Env*)allocate_object(sizeof(Env), OBJ_ENV);
    env->entries = NULL;
    env->count = 0;
    env->capacity = 0;
    env->parent = parent;
    
    if (vm.env_count >= vm.env_capacity) {
        vm.env_capacity *= 2;
        vm.env_stack = (Env**)safe_realloc(vm.env_stack, sizeof(Env*) * vm.env_capacity);
    }
    vm.env_stack[vm.env_count++] = env;
    return env;
}

void pop_env() { if (vm.env_count > 0) vm.env_count--; }

void env_define(Env* env, const char* name, Value val) {
    for (int i = 0; i < env->count; i++) {
        if (strcmp(env->entries[i].name, name) == 0) {
            env->entries[i].val = val;
            return;
        }
    }
    if (env->count >= env->capacity) {
        env->capacity = env->capacity < 8 ? 8 : env->capacity * 2;
        env->entries = safe_realloc(env->entries, sizeof(*env->entries) * env->capacity);
    }
    env->entries[env->count].name = safe_strdup(name);
    env->entries[env->count].val = val;
    env->count++;
}

int env_set(Env* env, const char* name, Value val) {
    Env* curr = env;
    while (curr) {
        for (int i = 0; i < curr->count; i++) {
            if (strcmp(curr->entries[i].name, name) == 0) {
                curr->entries[i].val = val;
                return 1;
            }
        }
        curr = curr->parent;
    }
    return 0;
}

Value env_get(Env* env, const char* name) {
    Env* curr = env;
    while (curr) {
        for (int i = 0; i < curr->count; i++) {
            if (strcmp(curr->entries[i].name, name) == 0) {
                return curr->entries[i].val;
            }
        }
        curr = curr->parent;
    }
    return make_null();
}

// ============================================================================
// LEXER
// ============================================================================
typedef enum {
    TOKEN_EOF, TOKEN_NEWLINE, TOKEN_IDENTIFIER, TOKEN_NUMBER, TOKEN_DECIMAL, TOKEN_STRING,
    TOKEN_PLUS, TOKEN_MINUS, TOKEN_STAR, TOKEN_SLASH, TOKEN_EQ, TOKEN_EQEQ, TOKEN_BANGEQ,
    TOKEN_LESS, TOKEN_LESSEQ, TOKEN_GREATER, TOKEN_GREATEREQ, TOKEN_LBRACKET, TOKEN_RBRACKET,
    TOKEN_LPAREN, TOKEN_RPAREN, TOKEN_LBRACE, TOKEN_RBRACE, TOKEN_COMMA, TOKEN_COLON, TOKEN_DOT,
    TOKEN_SAY, TOKEN_VAR, TOKEN_TEXT, TOKEN_NUMBER_KW, TOKEN_DECIMAL_KW, TOKEN_BOOLEAN_KW,
    TOKEN_GET, TOKEN_ARRAY, TOKEN_DICTIONARY, TOKEN_JOB, TOKEN_IF, TOKEN_ELSE, TOKEN_REPEAT, 
    TOKEN_FOREVER, TOKEN_OUT, TOKEN_FILE, TOKEN_CREATE, TOKEN_UPDATE, TOKEN_DELETE, TOKEN_SET,
    TOKEN_TRUE, TOKEN_FALSE, TOKEN_IMPORT, TOKEN_AS, TOKEN_TIME, TOKEN_SLEEP
} TokenType;

typedef struct { TokenType type; char* text; int line, col; } Token;
typedef struct { const char* source; int current; int line; int col; } Lexer;

Lexer lexer;
void init_lexer(const char* source) { lexer.source = source; lexer.current = 0; lexer.line = 1; lexer.col = 1; }

Token make_token(TokenType type, int start, int length) {
    Token t;
    t.type = type;
    t.text = (char*)ast_alloc(length + 1);
    strncpy(t.text, lexer.source + start, length);
    t.text[length] = '\0';
    t.line = lexer.line; t.col = lexer.col - length;
    return t;
}

int is_alpha(char c) { return isalpha(c) || c == '_'; }
int is_digit(char c) { return isdigit(c); }
char advance() { lexer.col++; return lexer.source[lexer.current++]; }
char peek() { return lexer.source[lexer.current]; }
char peek_next() { return lexer.current + 1 < strlen(lexer.source) ? lexer.source[lexer.current + 1] : '\0'; }
int match(char expected) { if (peek() != expected) return 0; advance(); return 1; }

void skip_whitespace() {
    while (1) {
        char c = peek();
        if (c == ' ' || c == '\r' || c == '\t') { advance(); }
        else if (c == 'n' && peek_next() == 'o' && strncmp(lexer.source + lexer.current, "note", 4) == 0) {
            lexer.current += 4; lexer.col += 4;
            while (peek() == ' ' || peek() == '\t') advance();
            if (peek() == '[') {
                int depth = 1; advance();
                while (peek() != '\0' && depth > 0) {
                    if (peek() == '[') depth++;
                    else if (peek() == ']') depth--;
                    if (peek() == '\n') { lexer.line++; lexer.col = 0; }
                    advance();
                }
            } else {
                while (peek() != '\n' && peek() != '\0') advance();
            }
        } else break;
    }
}

Token next_token() {
    skip_whitespace();
    int start = lexer.current;
    if (peek() == '\0') return make_token(TOKEN_EOF, start, 0);

    char c = advance();
    if (c == '\n') { lexer.line++; lexer.col = 1; return make_token(TOKEN_NEWLINE, start, 1); }
    if (c == '(') return make_token(TOKEN_LPAREN, start, 1); if (c == ')') return make_token(TOKEN_RPAREN, start, 1);
    if (c == '[') return make_token(TOKEN_LBRACKET, start, 1); if (c == ']') return make_token(TOKEN_RBRACKET, start, 1);
    if (c == '{') return make_token(TOKEN_LBRACE, start, 1); if (c == '}') return make_token(TOKEN_RBRACE, start, 1);
    if (c == ',') return make_token(TOKEN_COMMA, start, 1); if (c == ':') return make_token(TOKEN_COLON, start, 1);
    if (c == '.') return make_token(TOKEN_DOT, start, 1); if (c == '+') return make_token(TOKEN_PLUS, start, 1);
    if (c == '-') return make_token(TOKEN_MINUS, start, 1); if (c == '*') return make_token(TOKEN_STAR, start, 1);
    if (c == '/') return make_token(TOKEN_SLASH, start, 1);
    if (c == '=') return match('=') ? make_token(TOKEN_EQEQ, start, 2) : make_token(TOKEN_EQ, start, 1);
    if (c == '!') return match('=') ? make_token(TOKEN_BANGEQ, start, 2) : make_token(TOKEN_EOF, start, 1);
    if (c == '<') return match('=') ? make_token(TOKEN_LESSEQ, start, 2) : make_token(TOKEN_LESS, start, 1);
    if (c == '>') return match('=') ? make_token(TOKEN_GREATEREQ, start, 2) : make_token(TOKEN_GREATER, start, 1);

    if (c == '"') {
        while (peek() != '"' && peek() != '\0') {
            if (peek() == '\n') { lexer.line++; lexer.col = 1; }
            advance();
        }
        if (peek() == '\0') return make_token(TOKEN_EOF, start, 0);
        advance();
        return make_token(TOKEN_STRING, start + 1, lexer.current - start - 2);
    }

    if (is_digit(c)) {
        int is_dec = 0;
        while (is_digit(peek())) advance();
        if (peek() == '.' && is_digit(peek_next())) {
            is_dec = 1; advance();
            while (is_digit(peek())) advance();
        }
        return make_token(is_dec ? TOKEN_DECIMAL : TOKEN_NUMBER, start, lexer.current - start);
    }

    if (is_alpha(c)) {
        while (is_alpha(peek()) || is_digit(peek())) advance();
        int len = lexer.current - start;
        char* text = (char*)ast_alloc(len + 1);
        strncpy(text, lexer.source + start, len);
        
        TokenType type = TOKEN_IDENTIFIER;
        if (strcmp(text, "say") == 0) type = TOKEN_SAY; else if (strcmp(text, "var") == 0) type = TOKEN_VAR;
        else if (strcmp(text, "text") == 0) type = TOKEN_TEXT; else if (strcmp(text, "number") == 0) type = TOKEN_NUMBER_KW;
        else if (strcmp(text, "decimal") == 0) type = TOKEN_DECIMAL_KW; else if (strcmp(text, "boolean") == 0) type = TOKEN_BOOLEAN_KW;
        else if (strcmp(text, "get") == 0) type = TOKEN_GET; else if (strcmp(text, "array") == 0) type = TOKEN_ARRAY;
        else if (strcmp(text, "dictionary") == 0) type = TOKEN_DICTIONARY; else if (strcmp(text, "job") == 0) type = TOKEN_JOB;
        else if (strcmp(text, "if") == 0) type = TOKEN_IF; else if (strcmp(text, "else") == 0) type = TOKEN_ELSE;
        else if (strcmp(text, "repeat") == 0) type = TOKEN_REPEAT; else if (strcmp(text, "forever") == 0) type = TOKEN_FOREVER;
        else if (strcmp(text, "out") == 0) type = TOKEN_OUT; else if (strcmp(text, "file") == 0) type = TOKEN_FILE;
        else if (strcmp(text, "create") == 0) type = TOKEN_CREATE; else if (strcmp(text, "update") == 0) type = TOKEN_UPDATE;
        else if (strcmp(text, "delete") == 0) type = TOKEN_DELETE; else if (strcmp(text, "set") == 0) type = TOKEN_SET;
        else if (strcmp(text, "true") == 0) type = TOKEN_TRUE; else if (strcmp(text, "false") == 0) type = TOKEN_FALSE;
        else if (strcmp(text, "import") == 0) type = TOKEN_IMPORT; else if (strcmp(text, "as") == 0) type = TOKEN_AS;
        else if (strcmp(text, "time") == 0) type = TOKEN_TIME; else if (strcmp(text, "sleep") == 0) type = TOKEN_SLEEP;
        return make_token(type, start, len);
    }
    return make_token(TOKEN_EOF, start, 0);
}

// ============================================================================
// PARSER AND AST
// ============================================================================
typedef enum { EXPR_LITERAL, EXPR_VAR, EXPR_BINOP, EXPR_UNARY, EXPR_CALL, EXPR_MEMBER, EXPR_ARRAY_GET, EXPR_DICT_GET, EXPR_TIME_GET, EXPR_TIME_SLEEP } ExprType;
typedef struct sExpr {
    ExprType type; int line;
    union {
        Value literal; char* name;
        struct { struct sExpr* left; TokenType op; struct sExpr* right; } bin;
        struct { TokenType op; struct sExpr* right; } unary;
        struct { struct sExpr* callee; struct sExpr** args; int count; } call;
        struct { struct sExpr* object; char* prop; } member;
        struct { char* name; struct sExpr* index; } array_get;
        struct { char* name; char* key; } dict_get;
        struct { struct sExpr* ms; } time_sleep;
    } as;
} Expr;

typedef enum { STMT_EXPR, STMT_SAY, STMT_VAR, STMT_ARRAY, STMT_DICT, STMT_JOB, STMT_IF, STMT_REPEAT, STMT_OUT, STMT_FILE, STMT_ASSIGN, STMT_ARRAY_SET, STMT_DICT_SET, STMT_IMPORT } StmtType;
typedef struct sStmt {
    StmtType type; int line;
    union {
        Expr* expr;
        struct { char* name; Expr* initializer; int is_get; } var_decl;
        struct { char* name; Expr** elements; int count; } arr_decl;
        struct { char* name; char** keys; Expr** values; int count; } dict_decl;
        struct { char* name; char** params; int param_count; struct sStmt** body; int body_count; } job_decl;
        struct { Expr* cond; struct sStmt** then_b; int then_c; struct sStmt** else_b; int else_c; } if_stmt;
        struct { int forever; Expr* count; struct sStmt** body; int body_count; } repeat_stmt;
        struct { char* action; Expr* file; Expr* content; } file_stmt;
        struct { char* path; char* alias; } import_stmt;
        struct { char* name; Expr* value; } assign_stmt;
        struct { char* name; Expr* index; Expr* value; } array_set;
        struct { char* name; char* key; Expr* value; } dict_set;
    } as;
} Stmt;

Expr* make_expr(ExprType type, int line) { Expr* e = (Expr*)ast_alloc(sizeof(Expr)); e->type = type; e->line = line; return e; }
Stmt* make_stmt(StmtType type, int line) { Stmt* s = (Stmt*)ast_alloc(sizeof(Stmt)); s->type = type; s->line = line; return s; }
Expr* make_error_expr(); Stmt* make_error_stmt();

Token parser_curr, parser_prev;
int had_error = 0;

void advance_parser() {
    parser_prev = parser_curr;
    parser_curr = next_token();
}

void error_at(Token* token, const char* message) {
    if (had_error) return;
    fprintf(stderr, "Parse Error line %d: %s. Got '%s'\n", token->line, message, token->text);
    had_error = 1;
}

int match_token(TokenType type) {
    if (parser_curr.type == type) { advance_parser(); return 1; }
    return 0;
}

void consume(TokenType type, const char* err) {
    if (parser_curr.type == type) { advance_parser(); return; }
    error_at(&parser_curr, err);
}

void skip_newlines() { while (parser_curr.type == TOKEN_NEWLINE) advance_parser(); }

Token peek_next_token_parser() {
    Lexer saved = lexer; Token curr = parser_curr; Token prev = parser_prev;
    advance_parser(); Token next = parser_curr;
    lexer = saved; parser_curr = curr; parser_prev = prev;
    return next;
}

void synchronize() {
    had_error = 0;
    while (parser_curr.type != TOKEN_EOF) {
        if (parser_prev.type == TOKEN_NEWLINE) return;
        switch (parser_curr.type) {
            case TOKEN_VAR: case TOKEN_SAY: case TOKEN_IF: case TOKEN_REPEAT: case TOKEN_OUT:
            case TOKEN_ARRAY: case TOKEN_DICTIONARY: case TOKEN_JOB: case TOKEN_FILE: case TOKEN_IMPORT:
                return;
            default: break;
        }
        advance_parser();
    }
}

int is_expr_start(TokenType type) {
    switch (type) {
        case TOKEN_IDENTIFIER: case TOKEN_NUMBER: case TOKEN_DECIMAL: case TOKEN_STRING:
        case TOKEN_TRUE: case TOKEN_FALSE: case TOKEN_MINUS: case TOKEN_LPAREN:
        case TOKEN_ARRAY: case TOKEN_DICTIONARY: case TOKEN_TIME: return 1;
        default: return 0;
    }
}

typedef enum { PREC_NONE, PREC_ASSIGN, PREC_OR, PREC_AND, PREC_EQUALITY, PREC_COMPARISON, PREC_TERM, PREC_FACTOR, PREC_UNARY, PREC_CALL, PREC_PRIMARY } Precedence;

Expr* parse_expr(Precedence prec); Stmt* parse_statement();

Expr* make_error_expr() { Expr* e = make_expr(EXPR_LITERAL, parser_curr.line); e->as.literal = make_null(); return e; }
Stmt* make_error_stmt() { Stmt* s = make_stmt(STMT_EXPR, parser_curr.line); s->as.expr = make_error_expr(); return s; }

Expr* parse_literal() {
    Expr* e = make_expr(EXPR_LITERAL, parser_prev.line);
    if (parser_prev.type == TOKEN_NUMBER) e->as.literal = make_int(atoll(parser_prev.text));
    else if (parser_prev.type == TOKEN_DECIMAL) e->as.literal = make_float(atof(parser_prev.text));
    else if (parser_prev.type == TOKEN_STRING) { e->as.literal = make_string(parser_prev.text); e->as.literal.as.obj->is_constant = 1; }
    else if (parser_prev.type == TOKEN_TRUE) e->as.literal = make_bool(1);
    else if (parser_prev.type == TOKEN_FALSE) e->as.literal = make_bool(0);
    return e;
}

Expr* parse_variable() {
    Expr* e = make_expr(EXPR_VAR, parser_prev.line);
    e->as.name = ast_strdup(parser_prev.text);
    return e;
}

Expr* parse_grouping() { Expr* e = parse_expr(PREC_ASSIGN); consume(TOKEN_RPAREN, "Expected ')' after expression"); return e; }
Expr* parse_unary() {
    TokenType op = parser_prev.type; Expr* e = make_expr(EXPR_UNARY, parser_prev.line);
    e->as.unary.op = op; e->as.unary.right = parse_expr(PREC_UNARY); return e;
}

Expr* parse_binary(Expr* left) {
    TokenType op = parser_prev.type; Precedence prec = PREC_NONE;
    if (op == TOKEN_PLUS || op == TOKEN_MINUS) prec = PREC_TERM; else if (op == TOKEN_STAR || op == TOKEN_SLASH) prec = PREC_FACTOR;
    else if (op == TOKEN_EQEQ || op == TOKEN_BANGEQ) prec = PREC_EQUALITY;
    else if (op == TOKEN_LESS || op == TOKEN_LESSEQ || op == TOKEN_GREATER || op == TOKEN_GREATEREQ) prec = PREC_COMPARISON;
    Expr* right = parse_expr((Precedence)(prec + 1)); Expr* e = make_expr(EXPR_BINOP, parser_prev.line);
    e->as.bin.left = left; e->as.bin.op = op; e->as.bin.right = right; return e;
}

Expr* parse_dot(Expr* left) {
    consume(TOKEN_IDENTIFIER, "Expected property name"); Expr* e = make_expr(EXPR_MEMBER, parser_prev.line);
    e->as.member.object = left; e->as.member.prop = ast_strdup(parser_prev.text); return e;
}

Expr* parse_array_get() {
    int line = parser_prev.line; consume(TOKEN_GET, "Expected 'get' after array");
    if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected array name"); return make_error_expr(); }
    char* name = ast_strdup(parser_curr.text); advance_parser(); Expr* index = parse_expr(PREC_ASSIGN);
    Expr* e = make_expr(EXPR_ARRAY_GET, line); e->as.array_get.name = name; e->as.array_get.index = index; return e;
}

Expr* parse_dict_get() {
    int line = parser_prev.line; consume(TOKEN_GET, "Expected 'get' after dictionary");
    if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected dictionary name"); return make_error_expr(); }
    char* name = ast_strdup(parser_curr.text); advance_parser();
    if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected key name"); return make_error_expr(); }
    char* key = ast_strdup(parser_curr.text); advance_parser();
    Expr* e = make_expr(EXPR_DICT_GET, line); e->as.dict_get.name = name; e->as.dict_get.key = key; return e;
}

Expr* parse_time() {
    int line = parser_prev.line; 
    if (match_token(TOKEN_GET)) {
        return make_expr(EXPR_TIME_GET, line);
    } else if (match_token(TOKEN_SLEEP)) {
        Expr* e = make_expr(EXPR_TIME_SLEEP, line);
        e->as.time_sleep.ms = parse_expr(PREC_ASSIGN);
        return e;
    } else {
        error_at(&parser_curr, "Expected 'get' or 'sleep' after time");
        return make_error_expr();
    }
}

typedef struct { Expr* (*prefix)(); Expr* (*infix)(Expr*); Precedence prec; } ParseRule;
ParseRule* get_rule(TokenType type);

Expr* parse_expr(Precedence prec) {
    advance_parser();
    ParseRule* prefixRule = get_rule(parser_prev.type);
    if (prefixRule->prefix == NULL) { error_at(&parser_prev, "Expected expression"); return make_error_expr(); }
    Expr* left = prefixRule->prefix();
    
    while (1) {
        ParseRule* infixRule = get_rule(parser_curr.type);
        
        // Treat space-separated list of expressions as function call arguments
        // If the token has no infix behavior but starts a new expression, it must be an argument!
        if (prec <= PREC_CALL && infixRule->infix == NULL && is_expr_start(parser_curr.type)) {
            Expr* call = make_expr(EXPR_CALL, parser_prev.line);
            call->as.call.callee = left;
            call->as.call.args = NULL;
            call->as.call.count = 0;
            do {
                if (parser_curr.type == TOKEN_NEWLINE || parser_curr.type == TOKEN_EOF || parser_curr.type == TOKEN_RBRACKET) break;
                call->as.call.args = AST_REALLOC_ARRAY(call->as.call.args, Expr*, call->as.call.count, call->as.call.count + 1);
                call->as.call.args[call->as.call.count++] = parse_expr(PREC_ASSIGN);
            } while (match_token(TOKEN_COMMA));
            left = call;
            continue;
        }
        
        if (prec <= infixRule->prec && infixRule->prec != PREC_NONE) {
            advance_parser();
            left = infixRule->infix(left);
            continue;
        }
        
        break;
    }
    return left;
}

ParseRule rules[] = {
    [TOKEN_LPAREN]    = {parse_grouping, NULL, PREC_CALL}, [TOKEN_DOT]       = {NULL, parse_dot, PREC_CALL},
    [TOKEN_MINUS]     = {parse_unary, parse_binary, PREC_TERM}, [TOKEN_PLUS]      = {NULL, parse_binary, PREC_TERM},
    [TOKEN_SLASH]     = {NULL, parse_binary, PREC_FACTOR}, [TOKEN_STAR]      = {NULL, parse_binary, PREC_FACTOR},
    [TOKEN_EQEQ]      = {NULL, parse_binary, PREC_EQUALITY}, [TOKEN_BANGEQ]    = {NULL, parse_binary, PREC_EQUALITY},
    [TOKEN_LESS]      = {NULL, parse_binary, PREC_COMPARISON}, [TOKEN_LESSEQ]    = {NULL, parse_binary, PREC_COMPARISON},
    [TOKEN_GREATER]   = {NULL, parse_binary, PREC_COMPARISON}, [TOKEN_GREATEREQ] = {NULL, parse_binary, PREC_COMPARISON},
    [TOKEN_NUMBER]    = {parse_literal, NULL, PREC_NONE}, [TOKEN_DECIMAL]   = {parse_literal, NULL, PREC_NONE},
    [TOKEN_STRING]    = {parse_literal, NULL, PREC_NONE}, [TOKEN_TRUE]      = {parse_literal, NULL, PREC_NONE},
    [TOKEN_FALSE]     = {parse_literal, NULL, PREC_NONE}, [TOKEN_IDENTIFIER]= {parse_variable, NULL, PREC_NONE},
    [TOKEN_ARRAY]     = {parse_array_get, NULL, PREC_NONE}, [TOKEN_DICTIONARY]= {parse_dict_get, NULL, PREC_NONE},
    [TOKEN_TIME]      = {parse_time, NULL, PREC_NONE},
};
ParseRule* get_rule(TokenType type) {
    if (type >= sizeof(rules) / sizeof(rules[0])) { static ParseRule empty = {NULL, NULL, PREC_NONE}; return &empty; }
    return &rules[type];
}

Stmt** parse_block(int* count) {
    consume(TOKEN_LBRACKET, "Expected '[' to begin block"); skip_newlines();
    Stmt** stmts = NULL; *count = 0;
    while (parser_curr.type != TOKEN_RBRACKET && parser_curr.type != TOKEN_EOF) {
        stmts = AST_REALLOC_ARRAY(stmts, Stmt*, *count, *count + 1);
        stmts[(*count)++] = parse_statement();
        if (had_error) synchronize();
        skip_newlines();
    }
    consume(TOKEN_RBRACKET, "Expected ']' to end block"); return stmts;
}

Stmt* parse_statement() {
    int line = parser_curr.line;
    if (match_token(TOKEN_SAY)) { Stmt* s = make_stmt(STMT_SAY, line); s->as.expr = parse_expr(PREC_ASSIGN); return s; }
    if (match_token(TOKEN_VAR)) {
        if (parser_curr.type == TOKEN_TEXT || parser_curr.type == TOKEN_NUMBER_KW || parser_curr.type == TOKEN_DECIMAL_KW || parser_curr.type == TOKEN_BOOLEAN_KW) advance_parser();
        if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected variable name"); return make_error_stmt(); }
        char* name = ast_strdup(parser_curr.text); advance_parser();
        
        if (match_token(TOKEN_EQ)) {
            error_at(&parser_prev, "Variable declarations do not use '='. Use 'var <optional type> <name> <value>'");
            return make_error_stmt();
        }

        Stmt* s = make_stmt(STMT_VAR, line); s->as.var_decl.name = name;
        if (match_token(TOKEN_GET)) { s->as.var_decl.is_get = 1; s->as.var_decl.initializer = NULL; }
        else { s->as.var_decl.is_get = 0; s->as.var_decl.initializer = parse_expr(PREC_ASSIGN); }
        return s;
    }
    if (parser_curr.type == TOKEN_ARRAY) {
        Token next = peek_next_token_parser();
        if (next.type == TOKEN_SET) {
            advance_parser(); advance_parser();
            if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected array name"); return make_error_stmt(); }
            char* name = ast_strdup(parser_curr.text); advance_parser();
            Expr* index = parse_expr(PREC_ASSIGN); Expr* value = parse_expr(PREC_ASSIGN);
            Stmt* s = make_stmt(STMT_ARRAY_SET, line); s->as.array_set.name = name; s->as.array_set.index = index; s->as.array_set.value = value;
            return s;
        } else if (next.type != TOKEN_GET) {
            advance_parser();
            if (parser_curr.type == TOKEN_TEXT || parser_curr.type == TOKEN_NUMBER_KW || parser_curr.type == TOKEN_DECIMAL_KW || parser_curr.type == TOKEN_BOOLEAN_KW) advance_parser();
            if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected array name"); return make_error_stmt(); }
            Stmt* s = make_stmt(STMT_ARRAY, line); s->as.arr_decl.name = ast_strdup(parser_curr.text); advance_parser();
            s->as.arr_decl.elements = NULL; s->as.arr_decl.count = 0; skip_newlines();
            if (parser_curr.type != TOKEN_EOF && parser_curr.type != TOKEN_RBRACKET && parser_curr.type != TOKEN_NEWLINE) {
                do {
                    skip_newlines();
                    if (parser_curr.type == TOKEN_NEWLINE || parser_curr.type == TOKEN_EOF || parser_curr.type == TOKEN_RBRACKET) break;
                    s->as.arr_decl.elements = AST_REALLOC_ARRAY(s->as.arr_decl.elements, Expr*, s->as.arr_decl.count, s->as.arr_decl.count + 1);
                    s->as.arr_decl.elements[s->as.arr_decl.count++] = parse_expr(PREC_ASSIGN);
                    skip_newlines();
                } while (match_token(TOKEN_COMMA));
            }
            return s;
        }
    }
    if (parser_curr.type == TOKEN_DICTIONARY) {
        Token next = peek_next_token_parser();
        if (next.type == TOKEN_SET) {
            advance_parser(); advance_parser();
            if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected dict name"); return make_error_stmt(); }
            char* name = ast_strdup(parser_curr.text); advance_parser();
            if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected key name"); return make_error_stmt(); }
            char* key = ast_strdup(parser_curr.text); advance_parser();
            Expr* value = parse_expr(PREC_ASSIGN);
            Stmt* s = make_stmt(STMT_DICT_SET, line); s->as.dict_set.name = name; s->as.dict_set.key = key; s->as.dict_set.value = value;
            return s;
        } else if (next.type != TOKEN_GET) {
            advance_parser();
            if (parser_curr.type == TOKEN_TEXT || parser_curr.type == TOKEN_NUMBER_KW || parser_curr.type == TOKEN_DECIMAL_KW || parser_curr.type == TOKEN_BOOLEAN_KW) advance_parser();
            if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected dict name"); return make_error_stmt(); }
            Stmt* s = make_stmt(STMT_DICT, line); s->as.dict_decl.name = ast_strdup(parser_curr.text); advance_parser();
            s->as.dict_decl.keys = NULL; s->as.dict_decl.values = NULL; s->as.dict_decl.count = 0; skip_newlines();
            if (parser_curr.type != TOKEN_EOF && parser_curr.type != TOKEN_RBRACKET && parser_curr.type != TOKEN_NEWLINE) {
                do {
                    skip_newlines();
                    if (parser_curr.type == TOKEN_NEWLINE || parser_curr.type == TOKEN_EOF || parser_curr.type == TOKEN_RBRACKET) break;
                    if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected key name"); break; }
                    char* key_name = ast_strdup(parser_curr.text); advance_parser(); consume(TOKEN_COLON, "Expected ':'");
                    s->as.dict_decl.keys = AST_REALLOC_ARRAY(s->as.dict_decl.keys, char*, s->as.dict_decl.count, s->as.dict_decl.count + 1);
                    s->as.dict_decl.values = AST_REALLOC_ARRAY(s->as.dict_decl.values, Expr*, s->as.dict_decl.count, s->as.dict_decl.count + 1);
                    s->as.dict_decl.keys[s->as.dict_decl.count] = key_name; s->as.dict_decl.values[s->as.dict_decl.count] = parse_expr(PREC_ASSIGN);
                    s->as.dict_decl.count++; skip_newlines();
                } while (match_token(TOKEN_COMMA));
            }
            return s;
        }
    }
    if (match_token(TOKEN_JOB)) {
        if (parser_curr.type != TOKEN_IDENTIFIER) { error_at(&parser_curr, "Expected job name"); return make_error_stmt(); }
        Stmt* s = make_stmt(STMT_JOB, line); s->as.job_decl.name = ast_strdup(parser_curr.text); advance_parser();
        s->as.job_decl.params = NULL; s->as.job_decl.param_count = 0; skip_newlines();
        while (parser_curr.type == TOKEN_IDENTIFIER) {
            s->as.job_decl.params = AST_REALLOC_ARRAY(s->as.job_decl.params, char*, s->as.job_decl.param_count, s->as.job_decl.param_count + 1);
            s->as.job_decl.params[s->as.job_decl.param_count++] = ast_strdup(parser_curr.text); advance_parser();
            skip_newlines(); if (!match_token(TOKEN_COMMA)) break; skip_newlines();
        }
        skip_newlines(); s->as.job_decl.body = parse_block(&s->as.job_decl.body_count); return s;
    }
    if (match_token(TOKEN_IF)) {
        Stmt* s = make_stmt(STMT_IF, line); s->as.if_stmt.cond = parse_expr(PREC_ASSIGN); skip_newlines();
        s->as.if_stmt.then_b = parse_block(&s->as.if_stmt.then_c); s->as.if_stmt.else_b = NULL; s->as.if_stmt.else_c = 0; skip_newlines();
        if (match_token(TOKEN_ELSE)) { skip_newlines(); s->as.if_stmt.else_b = parse_block(&s->as.if_stmt.else_c); } return s;
    }
    if (match_token(TOKEN_REPEAT)) {
        Stmt* s = make_stmt(STMT_REPEAT, line);
        if (match_token(TOKEN_FOREVER)) s->as.repeat_stmt.forever = 1; else { s->as.repeat_stmt.forever = 0; s->as.repeat_stmt.count = parse_expr(PREC_ASSIGN); }
        skip_newlines(); s->as.repeat_stmt.body = parse_block(&s->as.repeat_stmt.body_count); return s;
    }
    if (match_token(TOKEN_OUT)) {
        Stmt* s = make_stmt(STMT_OUT, line); s->as.expr = (parser_curr.type != TOKEN_NEWLINE && parser_curr.type != TOKEN_RBRACKET && parser_curr.type != TOKEN_EOF) ? parse_expr(PREC_ASSIGN) : NULL; return s;
    }
    if (match_token(TOKEN_FILE)) {
        Stmt* s = make_stmt(STMT_FILE, line);
        if (match_token(TOKEN_CREATE)) s->as.file_stmt.action = "create"; else if (match_token(TOKEN_UPDATE)) s->as.file_stmt.action = "update";
        else if (match_token(TOKEN_DELETE)) s->as.file_stmt.action = "delete"; else { error_at(&parser_curr, "Expected create/update/delete"); return make_error_stmt(); }
        s->as.file_stmt.file = parse_expr(PREC_ASSIGN);
        if (strcmp(s->as.file_stmt.action, "delete") != 0) {
            skip_newlines();
            if (match_token(TOKEN_LBRACKET)) { skip_newlines(); s->as.file_stmt.content = parse_expr(PREC_ASSIGN); skip_newlines(); consume(TOKEN_RBRACKET, "Expected ']'"); }
            else s->as.file_stmt.content = parse_expr(PREC_ASSIGN);
        } else s->as.file_stmt.content = NULL;
        return s;
    }
    if (match_token(TOKEN_IMPORT)) {
        consume(TOKEN_STRING, "Expected string path after import"); Stmt* s = make_stmt(STMT_IMPORT, line);
        s->as.import_stmt.path = ast_strdup(parser_prev.text);
        if (match_token(TOKEN_AS)) { consume(TOKEN_IDENTIFIER, "Expected module alias"); s->as.import_stmt.alias = ast_strdup(parser_prev.text); }
        else s->as.import_stmt.alias = NULL;
        return s;
    }
    
    Expr* expr = parse_expr(PREC_ASSIGN);
    if (match_token(TOKEN_EQ)) {
        if (expr->type == EXPR_VAR) {
            Stmt* s = make_stmt(STMT_ASSIGN, line); s->as.assign_stmt.name = expr->as.name; s->as.assign_stmt.value = parse_expr(PREC_ASSIGN); return s;
        } else { error_at(&parser_curr, "Invalid assignment target"); return make_error_stmt(); }
    }
    Stmt* s = make_stmt(STMT_EXPR, line); s->as.expr = expr; return s;
}

// ============================================================================
// EXECUTION ENGINE
// ============================================================================
typedef struct CallFrame { char* name; int line; struct CallFrame* prev; } CallFrame;
CallFrame* current_frame = NULL;
int had_runtime_error = 0;

void runtime_error(const char* format, ...) {
    if (had_runtime_error) return;
    had_runtime_error = 1;
    fprintf(stderr, "Runtime Error:\n");
    va_list args; va_start(args, format); vfprintf(stderr, format, args); va_end(args);
    fprintf(stderr, "\n\n");
    CallFrame* frame = current_frame;
    while (frame) {
        if (strcmp(frame->name, "main") == 0) fprintf(stderr, "at %s line %d\n", frame->name, frame->line);
        else fprintf(stderr, "at %s() line %d\n", frame->name, frame->line);
        frame = frame->prev;
    }
}

typedef enum { EXEC_NORMAL, EXEC_RETURN, EXEC_BREAK } ExecType;
typedef struct { ExecType type; Value val; } ExecResult;

char* value_to_string(Value val) {
    char buffer[512];
    if (val.type == VAL_NULL) strcpy(buffer, "null");
    else if (val.type == VAL_BOOL) strcpy(buffer, val.as.boolean ? "true" : "false");
    else if (val.type == VAL_INT) snprintf(buffer, sizeof(buffer), "%lld", val.as.integer);
    else if (val.type == VAL_FLOAT) snprintf(buffer, sizeof(buffer), "%g", val.as.floating);
    else if (val.type == VAL_OBJ) {
        if (val.as.obj->type == OBJ_STRING) return safe_strdup(((ObjString*)val.as.obj)->chars);
        else strcpy(buffer, "<Object>");
    }
    return safe_strdup(buffer);
}

int is_truthy(Value val) {
    if (val.type == VAL_NULL) return 0;
    if (val.type == VAL_BOOL) return val.as.boolean;
    if (val.type == VAL_INT) return val.as.integer != 0;
    if (val.type == VAL_FLOAT) return val.as.floating != 0.0;
    return 1;
}

int values_equal(Value a, Value b) {
    if (a.type != b.type) return 0; if (a.type == VAL_NULL) return 1;
    if (a.type == VAL_BOOL) return a.as.boolean == b.as.boolean;
    if (a.type == VAL_INT) return a.as.integer == b.as.integer;
    if (a.type == VAL_FLOAT) return a.as.floating == b.as.floating;
    if (a.type == VAL_OBJ) {
        if (a.as.obj->type == OBJ_STRING && b.as.obj->type == OBJ_STRING) return strcmp(((ObjString*)a.as.obj)->chars, ((ObjString*)b.as.obj)->chars) == 0;
        return a.as.obj == b.as.obj;
    }
    return 0;
}

ExecResult exec(Stmt* stmt, Env* env);

Value eval(Expr* expr, Env* env) {
    if (had_runtime_error) return make_null();
    if (current_frame) current_frame->line = expr->line;
    if (expr->type == EXPR_LITERAL) return expr->as.literal;
    if (expr->type == EXPR_VAR) {
        Value val = env_get(env, expr->as.name);
        if (val.type == VAL_NULL) runtime_error("Undefined variable '%s'.", expr->as.name);
        return val;
    }
    if (expr->type == EXPR_UNARY) {
        Value right = eval(expr->as.unary.right, env);
        if (expr->as.unary.op == TOKEN_MINUS) {
            if (right.type == VAL_INT) return make_int(-right.as.integer);
            if (right.type == VAL_FLOAT) return make_float(-right.as.floating);
        }
        return make_null();
    }
    if (expr->type == EXPR_BINOP) {
        Value left = eval(expr->as.bin.left, env); Value right = eval(expr->as.bin.right, env);
        TokenType op = expr->as.bin.op;
        if (op == TOKEN_EQEQ) return make_bool(values_equal(left, right));
        if (op == TOKEN_BANGEQ) return make_bool(!values_equal(left, right));
        
        if (left.type == VAL_OBJ && left.as.obj->type == OBJ_STRING && op == TOKEN_PLUS) {
            char* s1 = ((ObjString*)left.as.obj)->chars; char* s2 = value_to_string(right);
            char* res = (char*)safe_alloc(strlen(s1) + strlen(s2) + 1);
            strcpy(res, s1); strcat(res, s2); safe_free(s2);
            Value ret = make_string(res); safe_free(res); return ret;
        }
        
        double l = left.type == VAL_FLOAT ? left.as.floating : (double)left.as.integer;
        double r = right.type == VAL_FLOAT ? right.as.floating : (double)right.as.integer;
        int is_int = (left.type == VAL_INT && right.type == VAL_INT);
        
        if (op == TOKEN_PLUS) return is_int ? make_int(left.as.integer + right.as.integer) : make_float(l + r);
        if (op == TOKEN_MINUS) return is_int ? make_int(left.as.integer - right.as.integer) : make_float(l - r);
        if (op == TOKEN_STAR) return is_int ? make_int(left.as.integer * right.as.integer) : make_float(l * r);
        if (op == TOKEN_SLASH) {
            if (r == 0) { runtime_error("Division by zero."); return make_null(); }
            return is_int ? make_int(left.as.integer / right.as.integer) : make_float(l / r);
        }
        if (op == TOKEN_LESS) return make_bool(l < r); if (op == TOKEN_LESSEQ) return make_bool(l <= r);
        if (op == TOKEN_GREATER) return make_bool(l > r); if (op == TOKEN_GREATEREQ) return make_bool(l >= r);
    }
    if (expr->type == EXPR_CALL) {
        Value callee = eval(expr->as.call.callee, env);
        if (callee.type != VAL_OBJ || callee.as.obj->type != OBJ_JOB) { runtime_error("Can only call jobs."); return make_null(); }
        ObjJob* job = (ObjJob*)callee.as.obj;
        if (expr->as.call.count != job->param_count) { runtime_error("Expected %d arguments but got %d.", job->param_count, expr->as.call.count); return make_null(); }
        
        Env* local = create_env(job->closure);
        for (int i = 0; i < job->param_count; i++) env_define(local, job->params[i], eval(expr->as.call.args[i], env));
        
        CallFrame frame; frame.name = job->name; frame.line = expr->line; frame.prev = current_frame; current_frame = &frame;
        ExecResult res = {EXEC_NORMAL, make_null()};
        for (int i = 0; i < job->body_count; i++) {
            res = exec(job->body[i], local);
            if (res.type != EXEC_NORMAL || had_runtime_error) break;
        }
        current_frame = frame.prev; pop_env(); return res.val;
    }
    if (expr->type == EXPR_ARRAY_GET) {
        Value obj = env_get(env, expr->as.array_get.name); Value idx = eval(expr->as.array_get.index, env);
        if (obj.type == VAL_OBJ && obj.as.obj->type == OBJ_ARRAY) {
            ObjArray* arr = (ObjArray*)obj.as.obj;
            if (idx.type != VAL_INT) { runtime_error("Array index must be an integer."); return make_null(); }
            if (idx.as.integer < 0 || idx.as.integer >= arr->count) { runtime_error("Index out of bounds."); return make_null(); }
            return arr->items[idx.as.integer];
        }
        runtime_error("Cannot get index from non-array."); return make_null();
    }
    if (expr->type == EXPR_DICT_GET) {
        Value obj = env_get(env, expr->as.dict_get.name);
        if (obj.type == VAL_OBJ && obj.as.obj->type == OBJ_DICT) {
            ObjDict* dict = (ObjDict*)obj.as.obj;
            for (int i = 0; i < dict->count; i++) {
                if (strcmp(dict->entries[i].key, expr->as.dict_get.key) == 0) return dict->entries[i].val;
            }
            return make_null();
        }
        runtime_error("Cannot get key from non-dictionary."); return make_null();
    }
    if (expr->type == EXPR_MEMBER) {
        Value obj = eval(expr->as.member.object, env);
        if (obj.type == VAL_OBJ && obj.as.obj->type == OBJ_MODULE) return env_get(((ObjModule*)obj.as.obj)->env, expr->as.member.prop);
        runtime_error("Cannot access property of non-module."); return make_null();
    }
    if (expr->type == EXPR_TIME_GET) {
        return make_int(get_time_ms());
    }
    if (expr->type == EXPR_TIME_SLEEP) {
        Value ms_val = eval(expr->as.time_sleep.ms, env);
        if (ms_val.type != VAL_INT && ms_val.type != VAL_FLOAT) {
            runtime_error("Sleep duration must be a number.");
            return make_null();
        }
        long long ms = (ms_val.type == VAL_INT) ? ms_val.as.integer : (long long)ms_val.as.floating;
        if (ms > 0) sleep_ms(ms);
        return make_null();
    }
    return make_null();
}

void run_file(const char* path, Env* env);

ExecResult exec(Stmt* stmt, Env* env) {
    if (had_runtime_error) return (ExecResult){EXEC_NORMAL, make_null()};
    if (current_frame) current_frame->line = stmt->line;
    if (bytes_allocated > vm.next_gc) gc_collect();
    ExecResult res = {EXEC_NORMAL, make_null()};

    if (stmt->type == STMT_EXPR) eval(stmt->as.expr, env);
    else if (stmt->type == STMT_SAY) {
        Value val = eval(stmt->as.expr, env);
        if (!had_runtime_error) {
            char* s = value_to_string(val);
            printf("%s\n", s); safe_free(s);
        }
    } else if (stmt->type == STMT_VAR) {
        Value val = make_null();
        if (stmt->as.var_decl.is_get) {
            char buf[512];
            if (fgets(buf, sizeof(buf), stdin)) { buf[strcspn(buf, "\r\n")] = '\0'; val = make_string(buf); }
        } else val = eval(stmt->as.var_decl.initializer, env);
        env_define(env, stmt->as.var_decl.name, val);
    } else if (stmt->type == STMT_ARRAY) {
        Value arr_val = make_array(); ObjArray* arr = (ObjArray*)arr_val.as.obj;
        for (int i = 0; i < stmt->as.arr_decl.count; i++) {
            if (arr->count >= arr->capacity) {
                arr->capacity = arr->capacity < 4 ? 8 : arr->capacity * 2;
                arr->items = safe_realloc(arr->items, sizeof(Value) * arr->capacity);
            }
            arr->items[arr->count++] = eval(stmt->as.arr_decl.elements[i], env);
        }
        env_define(env, stmt->as.arr_decl.name, arr_val);
    } else if (stmt->type == STMT_DICT) {
        Value dict_val = make_dict(); ObjDict* dict = (ObjDict*)dict_val.as.obj;
        for (int i = 0; i < stmt->as.dict_decl.count; i++) {
            Value v = eval(stmt->as.dict_decl.values[i], env);
            if (dict->count >= dict->capacity) {
                dict->capacity = dict->capacity < 4 ? 8 : dict->capacity * 2;
                dict->entries = safe_realloc(dict->entries, sizeof(DictEntry) * dict->capacity);
            }
            dict->entries[dict->count].key = safe_strdup(stmt->as.dict_decl.keys[i]);
            dict->entries[dict->count].val = v; dict->count++;
        }
        env_define(env, stmt->as.dict_decl.name, dict_val);
    } else if (stmt->type == STMT_JOB) {
        ObjJob* job = (ObjJob*)allocate_object(sizeof(ObjJob), OBJ_JOB);
        job->name = stmt->as.job_decl.name; job->params = stmt->as.job_decl.params;
        job->param_count = stmt->as.job_decl.param_count; job->body = stmt->as.job_decl.body;
        job->body_count = stmt->as.job_decl.body_count; job->closure = env;
        Value job_val; job_val.type = VAL_OBJ; job_val.as.obj = (Object*)job;
        env_define(env, job->name, job_val);
    } else if (stmt->type == STMT_IF) {
        if (is_truthy(eval(stmt->as.if_stmt.cond, env))) {
            for (int i = 0; i < stmt->as.if_stmt.then_c; i++) {
                res = exec(stmt->as.if_stmt.then_b[i], env);
                if (res.type != EXEC_NORMAL || had_runtime_error) return res;
            }
        } else if (stmt->as.if_stmt.else_b) {
            for (int i = 0; i < stmt->as.if_stmt.else_c; i++) {
                res = exec(stmt->as.if_stmt.else_b[i], env);
                if (res.type != EXEC_NORMAL || had_runtime_error) return res;
            }
        }
    } else if (stmt->type == STMT_REPEAT) {
        long long count = stmt->as.repeat_stmt.forever ? -1 : eval(stmt->as.repeat_stmt.count, env).as.integer;
        while (count == -1 || count > 0) {
            for (int i = 0; i < stmt->as.repeat_stmt.body_count; i++) {
                res = exec(stmt->as.repeat_stmt.body[i], env);
                if (had_runtime_error) return res;
                if (res.type == EXEC_RETURN) return res;
                if (res.type == EXEC_BREAK) { res.type = EXEC_NORMAL; return res; }
            }
            if (count > 0) count--;
        }
    } else if (stmt->type == STMT_OUT) {
        res.type = EXEC_RETURN; res.val = stmt->as.expr ? eval(stmt->as.expr, env) : make_null(); return res;
    } else if (stmt->type == STMT_FILE) {
        Value fval = eval(stmt->as.file_stmt.file, env); char* fname = value_to_string(fval);
        if (strcmp(stmt->as.file_stmt.action, "create") == 0 || strcmp(stmt->as.file_stmt.action, "update") == 0) {
            FILE* f = fopen(fname, strcmp(stmt->as.file_stmt.action, "create") == 0 ? "w" : "a");
            if (f) {
                Value content = eval(stmt->as.file_stmt.content, env); char* cstr = value_to_string(content);
                fprintf(f, "%s", cstr); safe_free(cstr); fclose(f);
            } else runtime_error("Could not open file %s.", fname);
        } else if (strcmp(stmt->as.file_stmt.action, "delete") == 0) remove(fname);
        safe_free(fname);
    } else if (stmt->type == STMT_ASSIGN) {
        Value val = eval(stmt->as.assign_stmt.value, env);
        if (!env_set(env, stmt->as.assign_stmt.name, val)) runtime_error("Undefined variable '%s'.", stmt->as.assign_stmt.name);
    } else if (stmt->type == STMT_ARRAY_SET) {
        Value arr_val = env_get(env, stmt->as.array_set.name); Value idx = eval(stmt->as.array_set.index, env); Value val = eval(stmt->as.array_set.value, env);
        if (arr_val.type == VAL_OBJ && arr_val.as.obj->type == OBJ_ARRAY) {
            ObjArray* arr = (ObjArray*)arr_val.as.obj;
            if (idx.type != VAL_INT) { runtime_error("Array index must be integer."); return res; }
            if (idx.as.integer < 0 || idx.as.integer >= arr->count) { runtime_error("Array index out of bounds."); return res; }
            arr->items[idx.as.integer] = val;
        } else runtime_error("Cannot set index on non-array.");
    } else if (stmt->type == STMT_DICT_SET) {
        Value dict_val = env_get(env, stmt->as.dict_set.name); Value val = eval(stmt->as.dict_set.value, env);
        if (dict_val.type == VAL_OBJ && dict_val.as.obj->type == OBJ_DICT) {
            ObjDict* dict = (ObjDict*)dict_val.as.obj; int found = 0;
            for (int i = 0; i < dict->count; i++) {
                if (strcmp(dict->entries[i].key, stmt->as.dict_set.key) == 0) { dict->entries[i].val = val; found = 1; break; }
            }
            if (!found) {
                if (dict->count >= dict->capacity) {
                    dict->capacity = dict->capacity < 4 ? 8 : dict->capacity * 2;
                    dict->entries = safe_realloc(dict->entries, sizeof(DictEntry) * dict->capacity);
                }
                dict->entries[dict->count].key = safe_strdup(stmt->as.dict_set.key);
                dict->entries[dict->count].val = val; dict->count++;
            }
        } else runtime_error("Cannot set key on non-dictionary.");
    } else if (stmt->type == STMT_IMPORT) {
        Env* mod_env = create_env(NULL); run_file(stmt->as.import_stmt.path, mod_env);
        ObjModule* mod = (ObjModule*)allocate_object(sizeof(ObjModule), OBJ_MODULE); mod->env = mod_env;
        Value mod_val; mod_val.type = VAL_OBJ; mod_val.as.obj = (Object*)mod;
        char* alias = stmt->as.import_stmt.alias;
        if (!alias) {
            char* last = strrchr(stmt->as.import_stmt.path, '/'); char* base = last ? last + 1 : stmt->as.import_stmt.path;
            char* dot = strchr(base, '.');
            if (dot) { int len = dot - base; char* a = safe_alloc(len + 1); strncpy(a, base, len); env_define(env, a, mod_val); safe_free(a); }
            else env_define(env, base, mod_val);
        } else env_define(env, alias, mod_val);
    }
    return res;
}

// ============================================================================
// RUNNER LOGIC
// ============================================================================
void run_script(const char* source, Env* env) {
    init_lexer(source);
    advance_parser();
    
    Stmt** stmts = NULL; int stmt_count = 0;
    skip_newlines();
    while (parser_curr.type != TOKEN_EOF) {
        stmts = AST_REALLOC_ARRAY(stmts, Stmt*, stmt_count, stmt_count + 1);
        stmts[stmt_count++] = parse_statement();
        if (had_error) synchronize();
        skip_newlines();
    }
    
    CallFrame main_frame = {"main", 1, NULL};
    current_frame = &main_frame;
    for (int i = 0; i < stmt_count; i++) {
        if (had_error || had_runtime_error) break;
        exec(stmts[i], env);
    }
    current_frame = NULL;
}

void run_file(const char* path, Env* env) {
    FILE* f = fopen(path, "rb");
    if (!f) { fprintf(stderr, "Error: Could not open file %s\n", path); exit(1); }
    fseek(f, 0, SEEK_END); long size = ftell(f); fseek(f, 0, SEEK_SET);
    
    char* source = safe_alloc(size + 1);
    fread(source, 1, size, f); source[size] = '\0'; fclose(f);
    
    Lexer old_lexer = lexer; Token old_curr = parser_curr; Token old_prev = parser_prev;
    run_script(source, env);
    lexer = old_lexer; parser_curr = old_curr; parser_prev = old_prev;
    
    safe_free(source);
}

int main(int argc, char** argv) {
    if (argc < 2) { printf("Usage: %s <filename>\n", argv[0]); return 1; }
    
    init_vm();
    Env* global_env = create_env(NULL);
    
    run_file(argv[1], global_env);
    
    pop_env();
    vm.gc_paused = 0; vm.next_gc = 0;
    
    Object* curr = vm.objects;
    while(curr) { curr->is_constant = 0; curr = curr->next; }
    gc_collect();
    safe_free(vm.env_stack);
    
    free_ast();
    
    return 0;
}