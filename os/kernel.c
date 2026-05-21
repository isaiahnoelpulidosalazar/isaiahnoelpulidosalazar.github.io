#define NULL ((void*)0)

typedef unsigned int size_t;
typedef unsigned char uint8_t;
typedef unsigned short uint16_t;
typedef unsigned int uint32_t;
typedef signed char int8_t;
typedef signed short int16_t;
typedef signed int int32_t;
typedef long long int64_t;
typedef unsigned long long uint64_t;

typedef _Bool bool;
#define true 1
#define false 0

// ---------------------- FREESTANDING MEMORY ALLOCATOR ----------------------

#define HEAP_SIZE (2 * 1024 * 1024) 
static uint8_t heap[HEAP_SIZE];
static size_t heap_index = 0;

typedef struct {
    size_t size;
} AllocHeader;

void* memcpy(void* dest, const void* src, size_t n) {
    char* d = dest;
    const char* s = src;
    while (n--) *d++ = *s++;
    return dest;
}

void* kmalloc(size_t size) {
    size = (size + 3) & ~3; 
    if (heap_index + sizeof(AllocHeader) + size > HEAP_SIZE) {
        return NULL; 
    }
    AllocHeader* h = (AllocHeader*)&heap[heap_index];
    h->size = size;
    heap_index += sizeof(AllocHeader) + size;
    return (void*)(h + 1);
}

void* krealloc(void* ptr, size_t new_size) {
    if (!ptr) return kmalloc(new_size);
    AllocHeader* h = (AllocHeader*)ptr - 1;
    if (h->size >= new_size) return ptr; 
    void* new_ptr = kmalloc(new_size);
    if (!new_ptr) return NULL;
    memcpy(new_ptr, ptr, h->size);
    return new_ptr;
}

void kfree(void* ptr) {
    (void)ptr; 
}

void* calloc(size_t num, size_t size) {
    size_t total = num * size;
    void* ptr = kmalloc(total);
    if (ptr) {
        char* p = (char*)ptr;
        for (size_t i = 0; i < total; i++) {
            p[i] = 0;
        }
    }
    return ptr;
}

#define malloc kmalloc
#define realloc krealloc
#define free kfree

// ---------------------- HARDWARE INLINE ASSEMBLY IO PORT OPERATIONS ----------------------

static inline uint8_t inb(uint16_t port) {
    uint8_t ret;
    __asm__ __volatile__("inb %1, %0" : "=a"(ret) : "Nd"(port));
    return ret;
}

static inline void outb(uint16_t port, uint8_t val) {
    __asm__ __volatile__("outb %0, %1" : : "a"(val), "Nd"(port));
}

static inline void outw(uint16_t port, uint16_t val) {
    __asm__ __volatile__("outw %0, %1" : : "a"(val), "Nd"(port));
}

static inline uint16_t inw(uint16_t port) {
    uint16_t ret;
    __asm__ __volatile__("inw %1, %0" : "=a"(ret) : "Nd"(port));
    return ret;
}

// ---------------------- PHYSICAL ATA PIO HARD DISK DRIVER ----------------------

void printf(const char* format, ...);

void ata_wait_bsy() {
    while (inb(0x1F7) & 0x80); 
}

void ata_wait_drq() {
    while (!(inb(0x1F7) & 0x08)); 
}

bool ata_present() {
    uint8_t status = inb(0x1F7);
    if (status == 0xFF) return false; 
    return true;
}

void ata_read_sector(uint32_t lba, uint16_t* buffer) {
    ata_wait_bsy();
    outb(0x1F6, 0xE0 | ((lba >> 24) & 0x0F)); 
    outb(0x1F2, 1);                           
    outb(0x1F3, (uint8_t)lba);                
    outb(0x1F4, (uint8_t)(lba >> 8));
    outb(0x1F5, (uint8_t)(lba >> 16));
    outb(0x1F7, 0x20);                        

    ata_wait_bsy();
    ata_wait_drq();

    for (int i = 0; i < 256; i++) {
        buffer[i] = inw(0x1F0);               
    }
}

void ata_write_sector(uint32_t lba, uint16_t* buffer) {
    ata_wait_bsy();
    outb(0x1F6, 0xE0 | ((lba >> 24) & 0x0F));
    outb(0x1F2, 1);                           
    outb(0x1F3, (uint8_t)lba);
    outb(0x1F4, (uint8_t)(lba >> 8));
    outb(0x1F5, (uint8_t)(lba >> 16));
    outb(0x1F7, 0x30);                        

    ata_wait_bsy();
    ata_wait_drq();

    for (int i = 0; i < 256; i++) {
        outw(0x1F0, buffer[i]);               
    }
    
    outb(0x1F7, 0xE7);                        
    ata_wait_bsy();
}

// ---------------------- HARDWARE SHUTDOWN & RESTART ACTIONS ----------------------

void vfs_save_to_disk(void);

void sys_shutdown(void) {
    vfs_save_to_disk(); // Auto-save triggered dynamically before poweroff

    outw(0xB004, 0x2000); 
    outw(0x604, 0x2000);  
    outw(0x4004, 0x3400); 

    printf("\nPoweroff complete. It is now safe to turn off your PC.\n");
    while (1) {
        __asm__ __volatile__("cli; hlt");
    }
}

void sys_restart(void) {
    vfs_save_to_disk(); // Auto-save triggered dynamically before reboot

    outb(0x64, 0xFE);

    struct {
        uint16_t limit;
        uint32_t base;
    } __attribute__((packed)) idt = {0, 0};
    __asm__ __volatile__("lidt %0; int $3" : : "m"(idt));

    while (1) {
        __asm__ __volatile__("cli; hlt");
    }
}

// ---------------------- FREESTANDING 64-BIT MATH HELPERS ----------------------

unsigned long long __udivmoddi4(unsigned long long num, unsigned long long den, unsigned long long *rem) {
    unsigned long long quot = 0, qbit = 1;
    if (den == 0) {
        if (rem) *rem = 0;
        return 0;
    }
    while ((den << 1) > den && (den << 1) < num) {
        den <<= 1;
        qbit <<= 1;
    }
    while (qbit > 0) {
        if (num >= den) {
            num -= den;
            quot |= qbit;
        }
        den >>= 1;
        qbit >>= 1;
    }
    if (rem) *rem = num;
    return quot;
}

unsigned long long __udivdi3(unsigned long long a, unsigned long long b) {
    unsigned long long r;
    return __udivmoddi4(a, b, &r);
}

unsigned long long __umoddi3(unsigned long long a, unsigned long long b) {
    unsigned long long r;
    __udivmoddi4(a, b, &r);
    return r;
}

long long __moddi3(long long a, long long b) {
    long long s_b = b >> 63;
    b = (b ^ s_b) - s_b; 
    long long s_a = a >> 63;
    a = (a ^ s_a) - s_a; 
    unsigned long long r;
    __udivmoddi4(a, b, &r);
    return ((long long)r ^ s_a) - s_a;
}

long long __divdi3(long long a, long long b) {
    long long s_b = b >> 63;
    b = (b ^ s_b) - s_b;
    long long s_a = a >> 63;
    a = (a ^ s_a) - s_a;
    unsigned long long r;
    unsigned long long q = __udivmoddi4(a, b, &r);
    long long s_q = s_a ^ s_b;
    return ((long long)q ^ s_q) - s_q;
}

// ---------------------- STRING LIBRARY HELPERS ----------------------

size_t strlen(const char* s) {
    size_t len = 0;
    while (s[len]) len++;
    return len;
}

char* strcpy(char* dest, const char* src) {
    char* d = dest;
    while ((*d++ = *src++));
    return dest;
}

char* strcat(char* dest, const char* src) {
    char* d = dest;
    while (*d) d++;
    while ((*d++ = *src++));
    return dest;
}

char* strncpy(char* dest, const char* src, size_t n) {
    char* d = dest;
    while (n > 0 && *src) {
        *d++ = *src++;
        n--;
    }
    while (n > 0) {
        *d++ = '\0';
        n--;
    }
    return dest;
}

int strcmp(const char* s1, const char* s2) {
    while (*s1 && (*s1 == *s2)) {
        s1++;
        s2++;
    }
    return *(const unsigned char*)s1 - *(const unsigned char*)s2;
}

int tolower(int c) {
    if (c >= 'A' && c <= 'Z') return c + 32;
    return c;
}

int strcasecmp(const char* s1, const char* s2) {
    while (*s1 && (tolower(*s1) == tolower(*s2))) {
        s1++;
        s2++;
    }
    return tolower(*(const unsigned char*)s1) - tolower(*(const unsigned char*)s2);
}

size_t strcspn(const char* s, const char* reject) {
    size_t count = 0;
    while (s[count]) {
        const char* r = reject;
        while (*r) {
            if (s[count] == *r) return count;
            r++;
        }
        count++;
    }
    return count;
}

void* memmove(void* dest, const void* src, size_t n) {
    char* d = dest;
    const char* s = src;
    if (d < s) {
        while (n--) *d++ = *s++;
    } else {
        d += n;
        s += n;
        while (n--) *--d = *--s;
    }
    return dest;
}

int isspace(int c) {
    return (c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == '\v' || c == '\f');
}

int isdigit(int c) {
    return (c >= '0' && c <= '9');
}

int isalpha(int c) {
    return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
}

int isalnum(int c) {
    return (isalpha(c) || isdigit(c));
}

long long atoll(const char* s) {
    long long res = 0;
    int sign = 1;
    while (isspace(*s)) s++;
    if (*s == '-') { sign = -1; s++; }
    else if (*s == '+') s++;
    while (isdigit(*s)) {
        res = res * 10 + (*s - '0');
        s++;
    }
    return sign * res;
}

double atof(const char* s) {
    double res = 0.0;
    double factor = 1.0;
    int sign = 1;
    while (isspace(*s)) s++;
    if (*s == '-') { sign = -1; s++; }
    else if (*s == '+') s++;
    while (isdigit(*s)) {
        res = res * 10.0 + (*s - '0');
        s++;
    }
    if (*s == '.') {
        s++;
        while (isdigit(*s)) {
            factor *= 0.1;
            res += (*s - '0') * factor;
            s++;
        }
    }
    return sign * res;
}

// ---------------------- FORMATTED CONSOLE OUTPUT ----------------------

#define VGA_WIDTH 80
#define VGA_HEIGHT 25
static uint16_t* const vga_buffer = (uint16_t*)0xB8000;
static int terminal_row = 0;
static int terminal_column = 0;
static uint8_t terminal_color = 0x07;

void terminal_clear(void) {
    for (int y = 0; y < VGA_HEIGHT; y++) {
        for (int x = 0; x < VGA_WIDTH; x++) {
            vga_buffer[y * VGA_WIDTH + x] = (uint16_t)' ' | (uint16_t)terminal_color << 8;
        }
    }
    terminal_row = 0;
    terminal_column = 0;
}

void terminal_scroll(void) {
    for (int y = 1; y < VGA_HEIGHT; y++) {
        for (int x = 0; x < VGA_WIDTH; x++) {
            vga_buffer[(y - 1) * VGA_WIDTH + x] = vga_buffer[y * VGA_WIDTH + x];
        }
    }
    for (int x = 0; x < VGA_WIDTH; x++) {
        vga_buffer[(VGA_HEIGHT - 1) * VGA_WIDTH + x] = (uint16_t)' ' | (uint16_t)terminal_color << 8;
    }
    terminal_row = VGA_HEIGHT - 1;
}

void terminal_putc(char c) {
    if (c == '\n') {
        terminal_column = 0;
        terminal_row++;
        if (terminal_row >= VGA_HEIGHT) {
            terminal_scroll();
        }
        return;
    }
    if (c == '\r') {
        terminal_column = 0;
        return;
    }
    vga_buffer[terminal_row * VGA_WIDTH + terminal_column] = (uint16_t)c | (uint16_t)terminal_color << 8;
    terminal_column++;
    if (terminal_column >= VGA_WIDTH) {
        terminal_column = 0;
        terminal_row++;
        if (terminal_row >= VGA_HEIGHT) {
            terminal_scroll();
        }
    }
}

void terminal_writestring(const char* data) {
    for (size_t i = 0; data[i] != '\0'; i++) {
        terminal_putc(data[i]);
    }
}

void llong_to_str(long long val, char* buf) {
    char tmp[32];
    int i = 0;
    int sign = (val < 0) ? 1 : 0;
    if (val < 0) val = -val;
    do {
        tmp[i++] = (val % 10) + '0';
        val /= 10;
    } while (val > 0);
    if (sign) tmp[i++] = '-';
    int j = 0;
    while (i > 0) {
        buf[j++] = tmp[--i];
    }
    buf[j] = '\0';
}

void double_to_str(double val, char* buf) {
    if (val < 0) {
        *buf++ = '-';
        val = -val;
    }
    long long ipart = (long long)val;
    llong_to_str(ipart, buf);
    while (*buf) buf++;
    *buf++ = '.';
    double fpart = val - (double)ipart;
    long long fpart_int = (long long)(fpart * 10000.0 + 0.5);
    llong_to_str(fpart_int, buf);
}

int sprintf(char* buf, const char* format, ...) {
    char* p_arg = (char*)&format + sizeof(format);
    const char* f = format;
    char* dest = buf;
    while (*f) {
        if (*f == '%') {
            f++;
            if (*f == 'l' && *(f+1) == 'l' && *(f+2) == 'd') {
                long long val = *(long long*)p_arg;
                p_arg += sizeof(long long);
                llong_to_str(val, dest);
                while (*dest) dest++;
                f += 3;
            } else if (*f == 'g') {
                double val = *(double*)p_arg;
                p_arg += sizeof(double);
                double_to_str(val, dest);
                while (*dest) dest++;
                f++;
            } else if (*f == 'd') {
                int val = *(int*)p_arg;
                p_arg += sizeof(int);
                llong_to_str(val, dest);
                while (*dest) dest++;
                f++;
            } else if (*f == 's') {
                char* s = *(char**)p_arg;
                p_arg += sizeof(char*);
                while (*s) *dest++ = *s++;
                f++;
            } else {
                *dest++ = *f++;
            }
        } else {
            *dest++ = *f++;
        }
    }
    *dest = '\0';
    return dest - buf;
}

void printf(const char* format, ...) {
    char buf[1024];
    char* p_arg = (char*)&format + sizeof(format);
    const char* f = format;
    char* dest = buf;
    while (*f) {
        if (*f == '%') {
            f++;
            if (*f == 'l' && *(f+1) == 'l' && *(f+2) == 'd') {
                long long val = *(long long*)p_arg;
                p_arg += sizeof(long long);
                llong_to_str(val, dest);
                while (*dest) dest++;
                f += 3;
            } else if (*f == 'g') {
                double val = *(double*)p_arg;
                p_arg += sizeof(double);
                double_to_str(val, dest);
                while (*dest) dest++;
                f++;
            } else if (*f == 'd') {
                int val = *(int*)p_arg;
                p_arg += sizeof(int);
                llong_to_str(val, dest);
                while (*dest) dest++;
                f++;
            } else if (*f == 's') {
                char* s = *(char**)p_arg;
                p_arg += sizeof(char*);
                if (!s) s = "(null)";
                while (*s) *dest++ = *s++;
                f++;
            } else if (*f == 'c') {
                char c = (char)*(int*)p_arg;
                p_arg += sizeof(int);
                *dest++ = c;
                f++;
            } else {
                *dest++ = *f++;
            }
        } else {
            *dest++ = *f++;
        }
    }
    *dest = '\0';
    terminal_writestring(buf);
}

void exit(int status) {
    printf("\nSystem halted with exit code %d\n", status);
    while (1) {
        __asm__ __volatile__("hlt");
    }
}

// ---------------------- HARDWARE KEYBOARD INPUT ----------------------

char kbd_get_scancode(void) {
    while ((inb(0x64) & 1) == 0); 
    return inb(0x60);
}

const char kbd_us_map[128] = {
    0,  27, '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', '\b',
    '\t', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\n',
    0, 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'', '`',
    0, '\\', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/', 0,
    '*', 0, ' ', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '-',
    0, 0, 0, '+', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

const char kbd_us_shift_map[128] = {
    0,  27, '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '\b',
    '\t', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '{', '}', '\n',
    0, 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ':', '"', '~',
    0, '|', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '<', '>', '?', 0,
    '*', 0, ' ', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '-',
    0, 0, 0, '+', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

bool shift_active = false;

char kbd_get_scancode(void) {
    while ((inb(0x64) & 1) == 0); 
    return inb(0x60);
}

char kbd_getchar(void) {
    while (1) {
        char code = kbd_get_scancode();
        if (code == 0x2A || code == 0x36) { 
            shift_active = true;
            continue;
        }
        if (code == (char)(0x2A + 0x80) || code == (char)(0x36 + 0x80)) { 
            shift_active = false;
            continue;
        }
        if (code & 0x80) continue; 
        
        char ascii = shift_active ? kbd_us_shift_map[(int)code] : kbd_us_map[(int)code];
        if (ascii != 0) return ascii;
    }
}

void kbd_gets(char* buf, size_t max_len) {
    size_t idx = 0;
    while (idx < max_len - 1) {
        char c = kbd_getchar();
        if (c == '\n') {
            terminal_putc('\n');
            break;
        }
        if (c == '\b') {
            if (idx > 0) {
                idx--;
                if (terminal_column > 0) {
                    terminal_column--;
                } else if (terminal_row > 0) {
                    terminal_row--;
                    terminal_column = VGA_WIDTH - 1;
                }
                vga_buffer[terminal_row * VGA_WIDTH + terminal_column] = (uint16_t)' ' | (uint16_t)terminal_color << 8;
            }
            continue;
        }
        terminal_putc(c);
        buf[idx++] = c;
    }
    buf[idx] = '\0';
}

// ---------------------- VIRTUAL RAM FILE SYSTEM ----------------------

#define MAX_MOCK_FILES 16
#define MAX_FILE_NAME 32
#define MAX_FILE_CONTENT 2048
#define VFS_SIGNATURE "INPSOS_VFS_V01"

typedef struct {
    char name[MAX_FILE_NAME];
    char content[MAX_FILE_CONTENT];
    bool active;
} MockFile;

typedef struct {
    char signature[16];
    MockFile files[MAX_MOCK_FILES];
} VfsStorage;

static VfsStorage vfs_data;
#define vfs vfs_data.files

void mock_file_create(const char* name, const char* content) {
    for (int i = 0; i < MAX_MOCK_FILES; i++) {
        if (vfs[i].active && strcmp(vfs[i].name, name) == 0) {
            strncpy(vfs[i].content, content, MAX_FILE_CONTENT - 1);
            vfs[i].content[MAX_FILE_CONTENT - 1] = '\0';
            return;
        }
    }
    for (int i = 0; i < MAX_MOCK_FILES; i++) {
        if (!vfs[i].active) {
            strncpy(vfs[i].name, name, MAX_FILE_NAME - 1);
            vfs[i].name[MAX_FILE_NAME - 1] = '\0';
            strncpy(vfs[i].content, content, MAX_FILE_CONTENT - 1);
            vfs[i].content[MAX_FILE_CONTENT - 1] = '\0';
            vfs[i].active = true;
            return;
        }
    }
    printf("VFS Error: Virtual disk storage full.\n");
}

void mock_file_update(const char* name, const char* content) {
    for (int i = 0; i < MAX_MOCK_FILES; i++) {
        if (vfs[i].active && strcmp(vfs[i].name, name) == 0) {
            size_t cur_len = strlen(vfs[i].content);
            if (cur_len < MAX_FILE_CONTENT - 1) {
                strncpy(vfs[i].content + cur_len, content, MAX_FILE_CONTENT - cur_len - 1);
                vfs[i].content[MAX_FILE_CONTENT - 1] = '\0';
            }
            return;
        }
    }
    mock_file_create(name, content);
}

char* mock_file_read(const char* name) {
    for (int i = 0; i < MAX_MOCK_FILES; i++) {
        if (vfs[i].active && strcmp(vfs[i].name, name) == 0) {
            return vfs[i].content;
        }
    }
    return NULL;
}

void mock_file_delete(const char* name) {
    for (int i = 0; i < MAX_MOCK_FILES; i++) {
        if (vfs[i].active && strcmp(vfs[i].name, name) == 0) {
            vfs[i].active = false;
            return;
        }
    }
    printf("VFS Error: Mock file '%s' not found.\n", name);
}

// ---------------------- ENUMS & TYPES ----------------------

typedef enum {
    TOKEN_VAR, TOKEN_SAY, TOKEN_GET, TOKEN_OUT, TOKEN_JOB, TOKEN_IF, TOKEN_ELSE,
    TOKEN_REPEAT, TOKEN_INCREMENT, TOKEN_DECREMENT, TOKEN_ARRAY, TOKEN_DICTIONARY,
    TOKEN_BOOLEAN_TYPE, TOKEN_NUMBER_TYPE, TOKEN_DECIMAL_TYPE, TOKEN_STRING_TYPE,
    TOKEN_TRUE, TOKEN_FALSE, TOKEN_IDENTIFIER, TOKEN_STRING_VALUE, TOKEN_NUMBER_VALUE,
    TOKEN_DECIMAL_VALUE, TOKEN_PLUS, TOKEN_MINUS, TOKEN_MULTIPLY, TOKEN_DIVIDE,
    TOKEN_MODULO, TOKEN_GREATER, TOKEN_LESS, TOKEN_GREATER_EQUAL, TOKEN_LESS_EQUAL,
    TOKEN_EQUAL, TOKEN_NOT_EQUAL, TOKEN_LBRACKET, TOKEN_RBRACKET, TOKEN_COMMA,
    TOKEN_COLON, TOKEN_EOF, TOKEN_SET, TOKEN_FILE, TOKEN_CREATE, TOKEN_READ,
    TOKEN_UPDATE, TOKEN_DELETE, TOKEN_RAW, TOKEN_COMPILE, TOKEN_RUN, 
    TOKEN_SHUTDOWN, TOKEN_RESTART, TOKEN_NONE
} TokenType;

typedef struct {
    TokenType type;
    char* text;
} Token;

typedef enum {
    VAL_NULL, VAL_INT, VAL_FLOAT, VAL_BOOL, VAL_STRING, VAL_ARRAY, VAL_DICT, VAL_JOB
} ValueType;

struct ASTNode;

typedef struct {
    ValueType type;
    union {
        long long i;
        double f;
        bool b;
        char* s;
        struct ValueList* arr;
        struct ValueDict* dict;
        struct ASTNode* job;
    } as;
} Value;

typedef struct ValueList {
    Value* items;
    int count;
    int cap;
} ValueList;

typedef struct {
    char* key;
    Value value;
} KeyValuePair;

typedef struct ValueDict {
    KeyValuePair* pairs;
    int count;
    int cap;
} ValueDict;

typedef enum {
    NODE_PROG, NODE_VAR_STMT, NODE_SAY, NODE_OUT, NODE_JOB, NODE_EXPR_STMT,
    NODE_INCDEC, NODE_REPEAT, NODE_IF, NODE_ARRAY_DECL, NODE_DICT_DECL,
    NODE_ARRAY_SET, NODE_DICT_SET, NODE_FILE_CREATE, NODE_FILE_UPDATE, NODE_FILE_DELETE,
    NODE_RAW, NODE_COMPILE, NODE_BIN_EXPR, NODE_LITERAL, NODE_VAR_EXPR, NODE_GET,
    NODE_CALL, NODE_ARRAY_GET, NODE_ARRAY_LEN, NODE_DICT_GET, NODE_DICT_LEN, NODE_FILE_READ, 
    NODE_RUN, NODE_SHUTDOWN, NODE_RESTART
} NodeType;

typedef struct DictPair {
    char* key;
    struct ASTNode* val;
} DictPair;

typedef struct ASTNode {
    NodeType type;
    char* name;
    struct ASTNode* left;
    struct ASTNode* right;
    struct ASTNode* cond;
    struct ASTNode** body;
    int body_count;
    struct ASTNode** false_body;
    int false_body_count;
    Value literal_val;
    TokenType op_type;
    TokenType var_type;
    bool is_increment;
    bool is_forever;
    bool is_key;
    struct ASTNode** args;
    int arg_count;
    char** param_names;
    int param_count;
    DictPair* dict_pairs;
    int pair_count;
    char* c_code;
} ASTNode;

typedef struct EnvNode {
    char* name;
    Value value;
    struct EnvNode* next;
} EnvNode;

typedef struct Env {
    EnvNode* head;
    struct Env* parent;
} Env;

// ---------------------- GLOBALS & UTILS ----------------------

Token* tokens = NULL;
int token_count = 0, token_cap = 0, current_token = 0;
bool is_break = false, is_return = false;
Value return_value;

char* my_strdup(const char* s) {
    char* d = kmalloc(strlen(s) + 1);
    if(d) strcpy(d, s);
    return d;
}

char* my_strndup(const char* s, size_t n) {
    char* d = kmalloc(n + 1);
    if(d) { strncpy(d, s, n); d[n] = '\0'; }
    return d;
}

void trim_inplace(char* str) {
    char* start = str;
    while(isspace((unsigned char)*start)) start++;
    char* end = start + strlen(start) - 1;
    while(end > start && isspace((unsigned char)*end)) end--;
    *(end + 1) = '\0';
    memmove(str, start, end - start + 2);
}

// ---------------------- MEMORY & DATA STRUCTURES ----------------------

Value val_null(void) { Value v; v.type = VAL_NULL; return v; }
Value val_int(long long i) { Value v; v.type = VAL_INT; v.as.i = i; return v; }
Value val_float(double f) { Value v; v.type = VAL_FLOAT; v.as.f = f; return v; }
Value val_bool(bool b) { Value v; v.type = VAL_BOOL; v.as.b = b; return v; }
Value val_string(const char* s) { Value v; v.type = VAL_STRING; v.as.s = my_strdup(s); return v; }
Value val_job(ASTNode* n) { Value v; v.type = VAL_JOB; v.as.job = n; return v; }

ValueList* new_list(void) {
    ValueList* l = malloc(sizeof(ValueList));
    l->count = 0; l->cap = 4;
    l->items = malloc(sizeof(Value) * l->cap);
    return l;
}

void list_add(ValueList* l, Value v) {
    if (l->count >= l->cap) { l->cap *= 2; l->items = realloc(l->items, sizeof(Value)*l->cap); }
    l->items[l->count++] = v;
}

ValueDict* new_dict(void) {
    ValueDict* d = malloc(sizeof(ValueDict));
    d->count = 0; d->cap = 4;
    d->pairs = malloc(sizeof(KeyValuePair) * d->cap);
    return d;
}

void dict_set(ValueDict* d, const char* key, Value v) {
    for(int i=0; i<d->count; i++) {
        if(!strcmp(d->pairs[i].key, key)) { d->pairs[i].value = v; return; }
    }
    if (d->count >= d->cap) { d->cap *= 2; d->pairs = realloc(d->pairs, sizeof(KeyValuePair)*d->cap); }
    d->pairs[d->count].key = my_strdup(key);
    d->pairs[d->count].value = v;
    d->count++;
}

ASTNode* new_node(NodeType type) {
    ASTNode* n = calloc(1, sizeof(ASTNode));
    if (n) n->type = type;
    return n;
}

Env* new_env(Env* parent) {
    Env* e = malloc(sizeof(Env));
    e->head = NULL; e->parent = parent;
    return e;
}

Env* find_env(Env* e, const char* name) {
    while (e) {
        EnvNode* n = e->head;
        while(n) { if(!strcmp(n->name, name)) return e; n = n->next; }
        e = e->parent;
    }
    return NULL;
}

void env_define(Env* e, const char* name, Value val) {
    EnvNode* n = malloc(sizeof(EnvNode));
    n->name = my_strdup(name); n->value = val; n->next = e->head; e->head = n;
}

void env_set(Env* e, const char* name, Value val) {
    Env* target = find_env(e, name);
    if (!target) { env_define(e, name, val); return; }
    EnvNode* n = target->head;
    while(n) { if(!strcmp(n->name, name)) { n->value = val; return; } n = n->next; }
}

Value env_get(Env* e, const char* name) {
    Env* target = find_env(e, name);
    if (target) {
        EnvNode* n = target->head;
        while(n) { if(!strcmp(n->name, name)) return n->value; n = n->next; }
    }
    printf("Runtime Error: Variable '%s' not found.\n", name);
    exit(1);
    return val_null();
}

// ---------------------- LEXER ----------------------

void add_token(TokenType type, const char* text) {
    if(token_count >= token_cap) {
        token_cap = token_cap == 0 ? 128 : token_cap * 2;
        tokens = realloc(tokens, token_cap * sizeof(Token));
    }
    tokens[token_count].type = type;
    tokens[token_count].text = text ? my_strdup(text) : my_strdup("");
    token_count++;
}

TokenType get_keyword_type(const char* id) {
    if (!strcmp(id, "var")) return TOKEN_VAR;
    if (!strcmp(id, "say")) return TOKEN_SAY;
    if (!strcmp(id, "get")) return TOKEN_GET;
    if (!strcmp(id, "out")) return TOKEN_OUT;
    if (!strcmp(id, "job")) return TOKEN_JOB;
    if (!strcmp(id, "if")) return TOKEN_IF;
    if (!strcmp(id, "else")) return TOKEN_ELSE;
    if (!strcmp(id, "repeat")) return TOKEN_REPEAT;
    if (!strcmp(id, "increment")) return TOKEN_INCREMENT;
    if (!strcmp(id, "decrement")) return TOKEN_DECREMENT;
    if (!strcmp(id, "array")) return TOKEN_ARRAY;
    if (!strcmp(id, "dictionary")) return TOKEN_DICTIONARY;
    if (!strcmp(id, "boolean")) return TOKEN_BOOLEAN_TYPE;
    if (!strcmp(id, "number")) return TOKEN_NUMBER_TYPE;
    if (!strcmp(id, "decimal")) return TOKEN_DECIMAL_TYPE;
    if (!strcmp(id, "string")) return TOKEN_STRING_TYPE;
    if (!strcmp(id, "true")) return TOKEN_TRUE;
    if (!strcmp(id, "false")) return TOKEN_FALSE;
    if (!strcmp(id, "set")) return TOKEN_SET;
    if (!strcmp(id, "file")) return TOKEN_FILE;
    if (!strcmp(id, "create")) return TOKEN_CREATE;
    if (!strcmp(id, "read")) return TOKEN_READ;
    if (!strcmp(id, "update")) return TOKEN_UPDATE;
    if (!strcmp(id, "delete")) return TOKEN_DELETE;
    if (!strcmp(id, "run")) return TOKEN_RUN;
    if (!strcmp(id, "shutdown")) return TOKEN_SHUTDOWN;
    if (!strcmp(id, "restart")) return TOKEN_RESTART;
    return TOKEN_IDENTIFIER;
}

void tokenize(const char* code) {
    int pos = 0, len = strlen(code);
    while (pos < len) {
        char c = code[pos];
        if (isspace((unsigned char)c)) { pos++; continue; }
        
        if (c == '"') {
            pos++; int start = pos;
            while(pos < len && code[pos] != '"') pos++;
            char* str = my_strndup(&code[start], pos - start);
            if(pos < len) pos++;
            add_token(TOKEN_STRING_VALUE, str);
            free(str); continue;
        }
        
        if (isdigit((unsigned char)c)) {
            int start = pos; bool is_dec = false;
            while(pos < len && (isdigit((unsigned char)code[pos]) || code[pos] == '.')) {
                if (code[pos] == '.') {
                    is_dec = true;
                }
                pos++;
            }
            char* num = my_strndup(&code[start], pos - start);
            add_token(is_dec ? TOKEN_DECIMAL_VALUE : TOKEN_NUMBER_VALUE, num);
            free(num); continue;
        }
        
        if (isalpha((unsigned char)c)) {
            int start = pos;
            while(pos < len && isalnum((unsigned char)code[pos])) pos++;
            char* id = my_strndup(&code[start], pos - start);
            
            if (!strcmp(id, "note")) {
                int peek_idx = pos; while(peek_idx < len && isspace((unsigned char)code[peek_idx])) peek_idx++;
                if (peek_idx < len && code[peek_idx] == '[') {
                    pos = peek_idx + 1;
                    while(pos < len && code[pos] != ']') pos++;
                    if(pos < len) pos++;
                } else {
                    while(pos < len && code[pos] != '\n' && code[pos] != '\r') pos++;
                }
                free(id); continue;
            }
            
            if (!strcmp(id, "raw") || !strcmp(id, "compile")) {
                int peek_idx = pos; while(peek_idx < len && isspace((unsigned char)code[peek_idx])) peek_idx++;
                if (peek_idx < len && code[peek_idx] == '[') {
                    peek_idx++;
                    int depth = 1; bool in_str = false; int r_start = peek_idx;
                    while(peek_idx < len && depth > 0) {
                        char cc = code[peek_idx];
                        if (cc == '"') in_str = !in_str;
                        if (!in_str) { 
                            if (cc == '[') {
                                depth++; 
                            } else if (cc == ']') {
                                depth--; 
                            }
                        }
                        if (depth > 0) peek_idx++;
                    }
                    char* raw_c_code = my_strndup(&code[r_start], peek_idx - r_start);
                    trim_inplace(raw_c_code);
                    add_token(!strcmp(id, "raw") ? TOKEN_RAW : TOKEN_COMPILE, raw_c_code);
                    free(raw_c_code); pos = peek_idx + 1; free(id); continue;
                }
            }
            add_token(get_keyword_type(id), id); free(id); continue;
        }
        
        if (c == '>') {
            if (pos+1 < len && code[pos+1] == '=') { add_token(TOKEN_GREATER_EQUAL, ">="); pos+=2; continue; }
            add_token(TOKEN_GREATER, ">"); pos++; continue;
        }
        if (c == '<') {
            if (pos+1 < len && code[pos+1] == '=') { add_token(TOKEN_LESS_EQUAL, "<="); pos+=2; continue; }
            add_token(TOKEN_LESS, "<"); pos++; continue;
        }
        if (c == '=') {
            if (pos+1 < len && code[pos+1] == '=') { add_token(TOKEN_EQUAL, "=="); pos+=2; continue; }
            printf("Syntax Error: Unknown character '='\n"); exit(1);
        }
        if (c == '!') {
            if (pos+1 < len && code[pos+1] == '=') { add_token(TOKEN_NOT_EQUAL, "!="); pos+=2; continue; }
            printf("Syntax Error: Unknown character '!'\n"); exit(1);
        }
        if (c == ':') { add_token(TOKEN_COLON, ":"); pos++; continue; }
        
        switch(c) {
            case '+': add_token(TOKEN_PLUS, "+"); break; case '-': add_token(TOKEN_MINUS, "-"); break;
            case '*': add_token(TOKEN_MULTIPLY, "*"); break; case '/': add_token(TOKEN_DIVIDE, "/"); break;
            case '%': add_token(TOKEN_MODULO, "%"); break; case '[': add_token(TOKEN_LBRACKET, "["); break;
            case ']': add_token(TOKEN_RBRACKET, "]"); break; case ',': add_token(TOKEN_COMMA, ","); break;
            default: printf("Syntax Error: Unknown character '%c'\n", c); exit(1);
        }
        pos++;
    }
    add_token(TOKEN_EOF, "");
}

// Forward declarations for parsing
ASTNode* parse_expr(void);
ASTNode* parse_stmt(void);

// ---------------------- TOKEN GETTERS & PARSERS ----------------------

Token peek(void) { 
    return tokens[current_token]; 
}

Token peek_next(void) { 
    return current_token + 1 < token_count ? tokens[current_token+1] : tokens[token_count-1]; 
}

Token consume(TokenType type) {
    if (peek().type == type) return tokens[current_token++];
    printf("Parse Error: Expected %d but got %d at '%s'\n", type, peek().type, peek().text);
    exit(1);
    Token dummy = {TOKEN_EOF, NULL}; 
    return dummy;
}

Token consume_type(void) {
    TokenType t = peek().type;
    if (t==TOKEN_BOOLEAN_TYPE || t==TOKEN_NUMBER_TYPE || t==TOKEN_DECIMAL_TYPE || t==TOKEN_STRING_TYPE)
        return consume(t);
    printf("Parse Error: Expected type but got %d\n", t); 
    exit(1);
    Token dummy = {TOKEN_EOF, NULL}; 
    return dummy;
}

bool is_expr_start(TokenType type) {
    return type == TOKEN_IDENTIFIER || type == TOKEN_NUMBER_VALUE ||
           type == TOKEN_DECIMAL_VALUE || type == TOKEN_STRING_VALUE ||
           type == TOKEN_GET || type == TOKEN_TRUE || type == TOKEN_FALSE ||
           type == TOKEN_ARRAY || type == TOKEN_DICTIONARY || type == TOKEN_FILE;
}

// ---------------------- PARSER IMPLEMENTATIONS ----------------------

ASTNode* parse_primary(void) {
    Token t = peek();
    ASTNode* n;
    if (t.type == TOKEN_NUMBER_VALUE) {
        n = new_node(NODE_LITERAL); n->literal_val = val_int(atoll(consume(TOKEN_NUMBER_VALUE).text)); return n;
    }
    if (t.type == TOKEN_DECIMAL_VALUE) {
        n = new_node(NODE_LITERAL); n->literal_val = val_float(atof(consume(TOKEN_DECIMAL_VALUE).text)); return n;
    }
    if (t.type == TOKEN_STRING_VALUE) {
        n = new_node(NODE_LITERAL); n->literal_val = val_string(consume(TOKEN_STRING_VALUE).text); return n;
    }
    if (t.type == TOKEN_TRUE) { consume(TOKEN_TRUE); n = new_node(NODE_LITERAL); n->literal_val = val_bool(true); return n; }
    if (t.type == TOKEN_FALSE) { consume(TOKEN_FALSE); n = new_node(NODE_LITERAL); n->literal_val = val_bool(false); return n; }
    if (t.type == TOKEN_GET) { consume(TOKEN_GET); return new_node(NODE_GET); }
    if (t.type == TOKEN_FILE) {
        consume(TOKEN_FILE); consume(TOKEN_READ);
        n = new_node(NODE_FILE_READ); n->left = parse_expr(); return n;
    }
    if (t.type == TOKEN_ARRAY) {
        consume(TOKEN_ARRAY);
        if (peek().type == TOKEN_GET) {
            consume(TOKEN_GET); n = new_node(NODE_ARRAY_GET); n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
            if (peek().type == TOKEN_COMMA) {
                consume(TOKEN_COMMA);
            }
            n->left = parse_expr(); return n;
        }
        if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "length")) {
            consume(TOKEN_IDENTIFIER); n = new_node(NODE_ARRAY_LEN); n->name = my_strdup(consume(TOKEN_IDENTIFIER).text); return n;
        }
        printf("Invalid array expr\n"); exit(1);
    }
    if (t.type == TOKEN_DICTIONARY) {
        consume(TOKEN_DICTIONARY);
        if (peek().type == TOKEN_GET) {
            consume(TOKEN_GET); n = new_node(NODE_DICT_GET); n->is_key = false;
            if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "key")) { consume(TOKEN_IDENTIFIER); n->is_key = true; }
            else if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "index")) { consume(TOKEN_IDENTIFIER); }
            else { printf("Expected key or index\n"); exit(1); }
            n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
            if (peek().type == TOKEN_COMMA) {
                consume(TOKEN_COMMA);
            }
            n->left = parse_expr(); return n;
        }
        if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "length")) {
            consume(TOKEN_IDENTIFIER); n = new_node(NODE_DICT_LEN); n->name = my_strdup(consume(TOKEN_IDENTIFIER).text); return n;
        }
        printf("Invalid dict expr\n"); exit(1);
    }
    if (t.type == TOKEN_IDENTIFIER) {
        char* name = consume(TOKEN_IDENTIFIER).text;
        if (is_expr_start(peek().type)) {
            n = new_node(NODE_CALL); n->name = my_strdup(name);
            n->args = malloc(sizeof(ASTNode*)*32); n->arg_count = 0;
            n->args[n->arg_count++] = parse_expr();
            while(peek().type == TOKEN_COMMA) { consume(TOKEN_COMMA); n->args[n->arg_count++] = parse_expr(); }
            return n;
        }
        n = new_node(NODE_VAR_EXPR); n->name = my_strdup(name); return n;
    }
    printf("Parse Error: Unexpected token %d at '%s'\n", peek().type, peek().text); exit(1);
    return NULL;
}

ASTNode* parse_mult(void) {
    ASTNode* left = parse_primary();
    while(peek().type == TOKEN_MULTIPLY || peek().type == TOKEN_DIVIDE || peek().type == TOKEN_MODULO) {
        Token op = consume(peek().type); ASTNode* bin = new_node(NODE_BIN_EXPR);
        bin->left = left; bin->op_type = op.type; bin->right = parse_primary(); left = bin;
    }
    return left;
}

ASTNode* parse_add(void) {
    ASTNode* left = parse_mult();
    while(peek().type == TOKEN_PLUS || peek().type == TOKEN_MINUS) {
        Token op = consume(peek().type); ASTNode* bin = new_node(NODE_BIN_EXPR);
        bin->left = left; bin->op_type = op.type; bin->right = parse_mult(); left = bin;
    }
    return left;
}

ASTNode* parse_comparison(void) {
    ASTNode* left = parse_add();
    while(peek().type >= TOKEN_GREATER && peek().type <= TOKEN_NOT_EQUAL) {
        Token op = consume(peek().type); ASTNode* bin = new_node(NODE_BIN_EXPR);
        bin->left = left; bin->op_type = op.type; bin->right = parse_add(); left = bin;
    }
    return left;
}

ASTNode* parse_expr(void) { return parse_comparison(); }

ASTNode* parse_stmt(void) {
    Token t = peek();
    ASTNode* n;
    if (t.type == TOKEN_RAW) { n = new_node(NODE_RAW); n->c_code = my_strdup(consume(TOKEN_RAW).text); return n; }
    if (t.type == TOKEN_COMPILE) { n = new_node(NODE_COMPILE); n->c_code = my_strdup(consume(TOKEN_COMPILE).text); return n; }
    if (t.type == TOKEN_VAR) {
        consume(TOKEN_VAR); n = new_node(NODE_VAR_STMT); n->var_type = TOKEN_NONE;
        if (peek().type >= TOKEN_BOOLEAN_TYPE && peek().type <= TOKEN_STRING_TYPE) n->var_type = consume_type().type;
        n->name = my_strdup(consume(TOKEN_IDENTIFIER).text); n->left = parse_expr(); return n;
    }
    if (t.type == TOKEN_SAY) { consume(TOKEN_SAY); n = new_node(NODE_SAY); n->left = parse_expr(); return n; }
    if (t.type == TOKEN_OUT) {
        consume(TOKEN_OUT); n = new_node(NODE_OUT);
        if (peek().type != TOKEN_EOF && peek().type != TOKEN_RBRACKET && peek().type != TOKEN_ELSE && is_expr_start(peek().type))
            n->left = parse_expr();
        return n;
    }
    if (t.type == TOKEN_INCREMENT || t.type == TOKEN_DECREMENT) {
        n = new_node(NODE_INCDEC); n->is_increment = consume(t.type).type == TOKEN_INCREMENT;
        n->name = my_strdup(consume(TOKEN_IDENTIFIER).text); n->left = parse_expr(); return n;
    }
    if (t.type == TOKEN_IF) {
        consume(TOKEN_IF); n = new_node(NODE_IF); n->cond = parse_expr(); consume(TOKEN_LBRACKET);
        n->body = malloc(sizeof(ASTNode*)*128); n->body_count = 0;
        while(peek().type != TOKEN_RBRACKET && peek().type != TOKEN_EOF) n->body[n->body_count++] = parse_stmt();
        consume(TOKEN_RBRACKET);
        if (peek().type == TOKEN_ELSE) {
            consume(TOKEN_ELSE); consume(TOKEN_LBRACKET);
            n->false_body = malloc(sizeof(ASTNode*)*128); n->false_body_count = 0;
            while(peek().type != TOKEN_RBRACKET && peek().type != TOKEN_EOF) n->false_body[n->false_body_count++] = parse_stmt();
            consume(TOKEN_RBRACKET);
        }
        return n;
    }
    if (t.type == TOKEN_REPEAT) {
        consume(TOKEN_REPEAT); n = new_node(NODE_REPEAT); n->is_forever = false;
        if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "forever")) { consume(TOKEN_IDENTIFIER); n->is_forever = true; }
        else n->cond = parse_expr();
        consume(TOKEN_LBRACKET); n->body = malloc(sizeof(ASTNode*)*128); n->body_count = 0;
        while(peek().type != TOKEN_RBRACKET && peek().type != TOKEN_EOF) n->body[n->body_count++] = parse_stmt();
        consume(TOKEN_RBRACKET); return n;
    }
    if (t.type == TOKEN_ARRAY) {
        TokenType next_type = peek_next().type;
        if (next_type == TOKEN_SET) {
            consume(TOKEN_ARRAY); consume(TOKEN_SET); n = new_node(NODE_ARRAY_SET);
            n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
            if (peek().type == TOKEN_COMMA) {
                consume(TOKEN_COMMA);
            }
            n->left = parse_expr();
            if (peek().type == TOKEN_COMMA) {
                consume(TOKEN_COMMA);
            }
            n->right = parse_expr(); return n;
        }
        if (next_type >= TOKEN_BOOLEAN_TYPE && next_type <= TOKEN_STRING_TYPE) {
            consume(TOKEN_ARRAY); n = new_node(NODE_ARRAY_DECL); n->var_type = consume_type().type;
            n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
            n->args = malloc(sizeof(ASTNode*)*128); n->arg_count = 0;
            if (peek().type != TOKEN_EOF && is_expr_start(peek().type)) {
                n->args[n->arg_count++] = parse_expr();
                while(peek().type == TOKEN_COMMA) { consume(TOKEN_COMMA); n->args[n->arg_count++] = parse_expr(); }
            }
            return n;
        }
    }
    if (t.type == TOKEN_DICTIONARY) {
        TokenType next_type = peek_next().type;
        if (next_type == TOKEN_SET) {
            consume(TOKEN_DICTIONARY); consume(TOKEN_SET); n = new_node(NODE_DICT_SET); n->is_key = false;
            if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "key")) { consume(TOKEN_IDENTIFIER); n->is_key = true; }
            else if (peek().type == TOKEN_IDENTIFIER && !strcmp(peek().text, "index")) { consume(TOKEN_IDENTIFIER); }
            else { printf("Expected key or index\n"); exit(1); }
            n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
            if (peek().type == TOKEN_COMMA) {
                consume(TOKEN_COMMA);
            }
            n->left = parse_expr();
            if (peek().type == TOKEN_COMMA) {
                consume(TOKEN_COMMA);
            }
            n->right = parse_expr(); return n;
        }
        if (next_type == TOKEN_IDENTIFIER) {
            consume(TOKEN_DICTIONARY); n = new_node(NODE_DICT_DECL); n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
            n->dict_pairs = malloc(sizeof(DictPair)*128); n->pair_count = 0;
            if (peek().type == TOKEN_IDENTIFIER || peek().type == TOKEN_STRING_VALUE) {
                n->dict_pairs[n->pair_count].key = my_strdup(consume(peek().type).text); consume(TOKEN_COLON);
                n->dict_pairs[n->pair_count++].val = parse_expr();
                while(peek().type == TOKEN_COMMA) {
                    consume(TOKEN_COMMA);
                    n->dict_pairs[n->pair_count].key = my_strdup(consume(peek().type).text); consume(TOKEN_COLON);
                    n->dict_pairs[n->pair_count++].val = parse_expr();
                }
            }
            return n;
        }
    }
    if (t.type == TOKEN_FILE) {
        TokenType next_type = peek_next().type;
        if (next_type == TOKEN_CREATE || next_type == TOKEN_UPDATE || next_type == TOKEN_DELETE) {
            consume(TOKEN_FILE);
            if (peek().type == TOKEN_CREATE) {
                consume(TOKEN_CREATE); n = new_node(NODE_FILE_CREATE); n->left = parse_expr();
                consume(TOKEN_LBRACKET); n->right = parse_expr(); consume(TOKEN_RBRACKET); return n;
            }
            if (peek().type == TOKEN_UPDATE) {
                consume(TOKEN_UPDATE); n = new_node(NODE_FILE_UPDATE); n->left = parse_expr();
                consume(TOKEN_LBRACKET); n->right = parse_expr(); consume(TOKEN_RBRACKET); return n;
            }
            if (peek().type == TOKEN_DELETE) {
                consume(TOKEN_DELETE); n = new_node(NODE_FILE_DELETE); n->left = parse_expr(); return n;
            }
        }
    }
    if (t.type == TOKEN_JOB) {
        consume(TOKEN_JOB); n = new_node(NODE_JOB); n->name = my_strdup(consume(TOKEN_IDENTIFIER).text);
        n->param_names = malloc(sizeof(char*)*32); n->param_count = 0;
        while(peek().type == TOKEN_IDENTIFIER) n->param_names[n->param_count++] = my_strdup(consume(TOKEN_IDENTIFIER).text);
        consume(TOKEN_LBRACKET); n->body = malloc(sizeof(ASTNode*)*128); n->body_count = 0;
        while(peek().type != TOKEN_RBRACKET && peek().type != TOKEN_EOF) n->body[n->body_count++] = parse_stmt();
        consume(TOKEN_RBRACKET); return n;
    }
    if (t.type == TOKEN_RUN) {
        consume(TOKEN_RUN);
        n = new_node(NODE_RUN);
        n->left = parse_expr();
        return n;
    }
    if (t.type == TOKEN_SHUTDOWN) {
        consume(TOKEN_SHUTDOWN);
        return new_node(NODE_SHUTDOWN);
    }
    if (t.type == TOKEN_RESTART) {
        consume(TOKEN_RESTART);
        return new_node(NODE_RESTART);
    }
    n = new_node(NODE_EXPR_STMT); n->left = parse_expr(); return n;
}

ASTNode* parse(void) {
    ASTNode* p = new_node(NODE_PROG); p->body = malloc(sizeof(ASTNode*)*1024); p->body_count = 0;
    while(peek().type != TOKEN_EOF) p->body[p->body_count++] = parse_stmt();
    return p;
}

// ---------------------- EVALUATOR ----------------------

char* value_to_string(Value v) {
    char buf[256];
    if (v.type == VAL_INT) sprintf(buf, "%lld", v.as.i);
    else if (v.type == VAL_FLOAT) sprintf(buf, "%g", v.as.f);
    else if (v.type == VAL_BOOL) strcpy(buf, v.as.b ? "True" : "False");
    else if (v.type == VAL_STRING) return my_strdup(v.as.s);
    else strcpy(buf, "Object");
    return my_strdup(buf);
}

Value cast_to_type(Value val, TokenType t) {
    if (t == TOKEN_NUMBER_TYPE) {
        if (val.type == VAL_FLOAT) return val_int((long long)val.as.f);
        if (val.type == VAL_STRING) return val_int(atoll(val.as.s));
        if (val.type == VAL_BOOL) return val_int(val.as.b ? 1 : 0);
    }
    if (t == TOKEN_DECIMAL_TYPE) {
        if (val.type == VAL_INT) return val_float((double)val.as.i);
        if (val.type == VAL_STRING) return val_float(atof(val.as.s));
        if (val.type == VAL_BOOL) return val_float(val.as.b ? 1.0 : 0.0);
    }
    if (t == TOKEN_BOOLEAN_TYPE) {
        if (val.type == VAL_INT) return val_bool(val.as.i != 0);
        if (val.type == VAL_STRING) return val_bool(strlen(val.as.s) > 0);
    }
    if (t == TOKEN_STRING_TYPE) return val_string(value_to_string(val));
    return val;
}

bool vals_equal(Value a, Value b) {
    if (a.type != b.type) return false;
    if (a.type == VAL_INT) return a.as.i == b.as.i;
    if (a.type == VAL_FLOAT) return a.as.f == b.as.f;
    if (a.type == VAL_BOOL) return a.as.b == b.as.b;
    if (a.type == VAL_STRING) return !strcmp(a.as.s, b.as.s);
    return false;
}

Value eval_expr(ASTNode* expr, Env* env);
void exec_stmt(ASTNode* stmt, Env* env);

void exec_c_code(const char* code, bool build_exe) {
    (void)build_exe;
    if (strcmp(code, "terminal_clear();") == 0) {
        terminal_clear();
    } else {
        printf("[inpsos Bypass]: Dynamic C compilation (GCC execution) is unsupported in freestanding boot mode.\n");
    }
}

Value eval_expr(ASTNode* expr, Env* env) {
    if (!expr) return val_null();
    if (expr->type == NODE_LITERAL) return expr->literal_val;
    if (expr->type == NODE_VAR_EXPR) {
        Value val = env_get(env, expr->name);
        if (val.type == VAL_JOB) {
            Env* local_env = new_env(env);
            for (int i=0; i < val.as.job->body_count; i++) {
                exec_stmt(val.as.job->body[i], local_env);
                if (is_return) { is_return = false; return return_value; }
                if (is_break) { is_break = false; return val_null(); }
            }
            return val_null();
        }
        return val;
    }
    if (expr->type == NODE_GET) {
        char buf[256]; 
        kbd_gets(buf, 256);
        size_t len = strlen(buf);
        if (len > 0 && buf[len - 1] == '\n') buf[len - 1] = '\0';
        
        bool is_float = false;
        bool is_num = true;
        for (size_t i = 0; buf[i] != '\0'; i++) {
            if (buf[i] == '.') is_float = true;
            else if (!isdigit(buf[i]) && buf[i] != '-' && buf[i] != '+') is_num = false;
        }
        if (is_num) {
            if (is_float) return val_float(atof(buf));
            return val_int(atoll(buf));
        }
        if (!strcmp(buf, "true") || !strcmp(buf, "TRUE")) return val_bool(true);
        if (!strcmp(buf, "false") || !strcmp(buf, "FALSE")) return val_bool(false);
        return val_string(buf);
    }
    if (expr->type == NODE_FILE_READ) {
        Value fv = eval_expr(expr->left, env); char* fname = value_to_string(fv);
        char* content = mock_file_read(fname);
        if(content) {
            return val_string(content);
        }
        printf("Runtime Error: Virtual File '%s' not found.\n", fname); exit(1);
    }
    if (expr->type == NODE_ARRAY_GET) {
        Value obj = env_get(env, expr->name);
        if (obj.type == VAL_ARRAY) {
            int idx = (int)eval_expr(expr->left, env).as.i;
            return obj.as.arr->items[idx];
        }
        printf("Runtime Error: Not an array.\n"); exit(1);
    }
    if (expr->type == NODE_ARRAY_LEN) {
        Value obj = env_get(env, expr->name);
        if (obj.type == VAL_ARRAY) return val_int(obj.as.arr->count);
        printf("Runtime Error: Not an array.\n"); exit(1);
    }
    if (expr->type == NODE_DICT_GET) {
        Value obj = env_get(env, expr->name);
        if (obj.type == VAL_DICT) {
            if (expr->is_key) {
                char* key = value_to_string(eval_expr(expr->left, env));
                for(int i=0; i<obj.as.dict->count; i++) {
                    if(!strcmp(obj.as.dict->pairs[i].key, key)) return obj.as.dict->pairs[i].value;
                }
                printf("Runtime Error: Key not found.\n"); exit(1);
            } else {
                int idx = (int)eval_expr(expr->left, env).as.i;
                return obj.as.dict->pairs[idx].value;
            }
        }
        printf("Runtime Error: Not a dictionary.\n"); exit(1);
    }
    if (expr->type == NODE_DICT_LEN) {
        Value obj = env_get(env, expr->name);
        if (obj.type == VAL_DICT) return val_int(obj.as.dict->count);
        printf("Runtime Error: Not a dictionary.\n"); exit(1);
    }
    if (expr->type == NODE_BIN_EXPR) {
        Value l = eval_expr(expr->left, env); Value r = eval_expr(expr->right, env);
        if (expr->op_type == TOKEN_EQUAL) return val_bool(vals_equal(l, r));
        if (expr->op_type == TOKEN_NOT_EQUAL) return val_bool(!vals_equal(l, r));
        if (l.type == VAL_BOOL || r.type == VAL_BOOL) {
            if (expr->op_type == TOKEN_PLUS && (l.type == VAL_STRING || r.type == VAL_STRING)) {
                char *ls = value_to_string(l), *rs = value_to_string(r);
                char *res = malloc(strlen(ls) + strlen(rs) + 1);
                strcpy(res, ls); strcat(res, rs); return val_string(res);
            }
            printf("Runtime Error: Invalid boolean operation.\n"); exit(1);
        }
        bool is_l_int = l.type == VAL_INT, is_r_int = r.type == VAL_INT;
        if ((is_l_int || l.type == VAL_FLOAT) && (is_r_int || r.type == VAL_FLOAT)) {
            double l_val = is_l_int ? (double)l.as.i : l.as.f;
            double r_val = is_r_int ? (double)r.as.i : r.as.f;
            switch(expr->op_type) {
                case TOKEN_PLUS: return (is_l_int && is_r_int) ? val_int(l.as.i + r.as.i) : val_float(l_val + r_val);
                case TOKEN_MINUS: return (is_l_int && is_r_int) ? val_int(l.as.i - r.as.i) : val_float(l_val - r_val);
                case TOKEN_MULTIPLY: return (is_l_int && is_r_int) ? val_int(l.as.i * r.as.i) : val_float(l_val * r_val);
                case TOKEN_DIVIDE: return val_float(l_val / r_val);
                case TOKEN_MODULO: return (is_l_int && is_r_int) ? val_int(l.as.i % r.as.i) : val_float((long long)l_val % (long long)r_val);
                case TOKEN_GREATER: return val_bool(l_val > r_val);
                case TOKEN_LESS: return val_bool(l_val < r_val);
                case TOKEN_GREATER_EQUAL: return val_bool(l_val >= r_val);
                case TOKEN_LESS_EQUAL: return val_bool(l_val <= r_val);
                default: exit(1);
            }
        }
        if (expr->op_type == TOKEN_PLUS) {
            char *ls = value_to_string(l), *rs = value_to_string(r);
            char *res = malloc(strlen(ls) + strlen(rs) + 1);
            strcpy(res, ls); strcat(res, rs); return val_string(res);
        }
        printf("Runtime Error: Invalid binary operation.\n"); exit(1);
    }
    if (expr->type == NODE_CALL) {
        Value f = env_get(env, expr->name);
        if (f.type == VAL_JOB) {
            Env* loc = new_env(env);
            for(int i=0; i < f.as.job->param_count; i++) {
                Value arg = i < expr->arg_count ? eval_expr(expr->args[i], env) : val_null();
                env_define(loc, f.as.job->param_names[i], arg);
            }
            for(int i=0; i < f.as.job->body_count; i++) {
                exec_stmt(f.as.job->body[i], loc);
                if (is_return) { is_return = false; return return_value; }
                if (is_break) { is_break = false; return val_null(); }
            }
            return val_null();
        }
        printf("Runtime Error: Not a function.\n"); exit(1);
    }
    return val_null();
}

void run_easec(const char* code);

void exec_stmt(ASTNode* stmt, Env* env) {
    if (!stmt) return;
    if (stmt->type == NODE_RAW) exec_c_code(stmt->c_code, false);
    else if (stmt->type == NODE_COMPILE) exec_c_code(stmt->c_code, true);
    else if (stmt->type == NODE_VAR_STMT) {
        Value val = eval_expr(stmt->left, env);
        if (stmt->var_type != TOKEN_NONE) val = cast_to_type(val, stmt->var_type);
        env_define(env, stmt->name, val);
    }
    else if (stmt->type == NODE_SAY) printf("%s\n", value_to_string(eval_expr(stmt->left, env)));
    else if (stmt->type == NODE_OUT) {
        if (stmt->left) { return_value = eval_expr(stmt->left, env); is_return = true; }
        else is_break = true;
    }
    else if (stmt->type == NODE_INCDEC) {
        Value cur = env_get(env, stmt->name); Value amt = eval_expr(stmt->left, env);
        double c = cur.type == VAL_INT ? (double)cur.as.i : cur.as.f;
        double a = amt.type == VAL_INT ? (double)amt.as.i : amt.as.f;
        double r = stmt->is_increment ? c + a : c - a;
        env_set(env, stmt->name, cur.type == VAL_INT ? val_int((long long)r) : val_float(r));
    }
    else if (stmt->type == NODE_IF) {
        Value cond = eval_expr(stmt->cond, env);
        bool b = cond.type == VAL_BOOL ? cond.as.b : (cond.type == VAL_INT ? cond.as.i != 0 : false);
        if (b) {
            for(int i=0; i<stmt->body_count; i++) {
                exec_stmt(stmt->body[i], env); if(is_return || is_break) return;
            }
        } else {
            for(int i=0; i<stmt->false_body_count; i++) {
                exec_stmt(stmt->false_body[i], env); if(is_return || is_break) return;
            }
        }
    }
    else if (stmt->type == NODE_REPEAT) {
        if (stmt->is_forever) {
            while(true) {
                for(int i=0; i<stmt->body_count; i++) { exec_stmt(stmt->body[i], env); if (is_return) return; if (is_break) { is_break = false; return; } }
            }
        } else {
            long long c = eval_expr(stmt->cond, env).as.i;
            for(long long x=0; x<c; x++) {
                for(int i=0; i<stmt->body_count; i++) { exec_stmt(stmt->body[i], env); if (is_return) return; if (is_break) { is_break = false; return; } }
            }
        }
    }
    else if (stmt->type == NODE_ARRAY_DECL) {
        ValueList* list = new_list();
        for(int i=0; i<stmt->arg_count; i++) list_add(list, cast_to_type(eval_expr(stmt->args[i], env), stmt->var_type));
        Value v; v.type = VAL_ARRAY; v.as.arr = list; env_define(env, stmt->name, v);
    }
    else if (stmt->type == NODE_ARRAY_SET) {
        Value arr = env_get(env, stmt->name);
        if (arr.type == VAL_ARRAY) {
            int idx = (int)eval_expr(stmt->left, env).as.i; Value val = eval_expr(stmt->right, env);
            if(idx >= 0 && idx < arr.as.arr->count) arr.as.arr->items[idx] = val;
            else { printf("Runtime Error: Index out of bounds.\n"); exit(1); }
        } else { printf("Runtime Error: Not an array.\n"); exit(1); }
    }
    else if (stmt->type == NODE_DICT_DECL) {
        ValueDict* dict = new_dict();
        for(int i=0; i<stmt->pair_count; i++) dict_set(dict, stmt->dict_pairs[i].key, eval_expr(stmt->dict_pairs[i].val, env));
        Value v; v.type = VAL_DICT; v.as.dict = dict; env_define(env, stmt->name, v);
    }
    else if (stmt->type == NODE_DICT_SET) {
        Value d = env_get(env, stmt->name);
        if (d.type == VAL_DICT) {
            Value val = eval_expr(stmt->right, env);
            if (stmt->is_key) {
                char* k = value_to_string(eval_expr(stmt->left, env)); dict_set(d.as.dict, k, val);
            } else {
                int idx = (int)eval_expr(stmt->left, env).as.i;
                if(idx >= 0 && idx < d.as.dict->count) d.as.dict->pairs[idx].value = val;
                else { printf("Runtime Error: Index out of bounds.\n"); exit(1); }
            }
        } else { printf("Runtime Error: Not a dictionary.\n"); exit(1); }
    }
    else if (stmt->type == NODE_FILE_CREATE) {
        char* fname = value_to_string(eval_expr(stmt->left, env)); char* cnt = value_to_string(eval_expr(stmt->right, env));
        mock_file_create(fname, cnt);
    }
    else if (stmt->type == NODE_FILE_UPDATE) {
        char* fname = value_to_string(eval_expr(stmt->left, env)); char* cnt = value_to_string(eval_expr(stmt->right, env));
        mock_file_update(fname, cnt);
    }
    else if (stmt->type == NODE_FILE_DELETE) {
        char* fname = value_to_string(eval_expr(stmt->left, env));
        mock_file_delete(fname);
    }
    else if (stmt->type == NODE_JOB) env_define(env, stmt->name, val_job(stmt));
    else if (stmt->type == NODE_RUN) {
        char* fname = value_to_string(eval_expr(stmt->left, env));
        char* code = mock_file_read(fname);
        if (code) {
            run_easec(code);
        } else {
            printf("Runtime Error: Easec program '%s' not found.\n", fname);
        }
    }
    else if (stmt->type == NODE_SHUTDOWN) {
        sys_shutdown();
    }
    else if (stmt->type == NODE_RESTART) {
        sys_restart();
    }
    else if (stmt->type == NODE_EXPR_STMT) eval_expr(stmt->left, env);
}

// ---------------------- HARD DISK VFS PERSISTENCE ----------------------

void vfs_save_to_disk(void) {
    if (!ata_present()) {
        printf("[VFS] Save Failed: No physical ATA hard disk detected on Primary Master.\n");
        return;
    }
    printf("[VFS] Committing RAM storage state to raw sectors on physical hard drive...\n");
    
    // Set validation signature prior to serializing VFS blocks
    for (int i = 0; i < 15; i++) {
        vfs_data.signature[i] = VFS_SIGNATURE[i];
    }
    vfs_data.signature[15] = '\0';
    
    uint16_t* buffer = (uint16_t*)&vfs_data;
    size_t total_words = (sizeof(vfs_data) + 1) / 2;
    size_t total_sectors = (sizeof(vfs_data) + 511) / 512;
    
    for (size_t s = 0; s < total_sectors; s++) {
        uint16_t sector_buffer[256];
        for (int w = 0; w < 256; w++) {
            size_t idx = s * 256 + w;
            sector_buffer[w] = (idx < total_words) ? buffer[idx] : 0;
        }
        ata_write_sector(100 + s, sector_buffer);
    }
    printf("[VFS] Commit complete. Virtual files written safely to disk sectors 100-%d.\n", 100 + total_sectors - 1);
}

void vfs_load_from_disk(void) {
    if (!ata_present()) {
        printf("[VFS] Load Failed: No physical ATA hard disk detected on Primary Master.\n");
        return;
    }
    
    // Safety check: verify persistent signature inside the initial storage block
    uint16_t validation_sector[256];
    ata_read_sector(100, validation_sector);
    char* drive_signature = (char*)validation_sector;
    
    bool signature_match = true;
    for (int i = 0; i < 14; i++) {
        if (drive_signature[i] != VFS_SIGNATURE[i]) {
            signature_match = false;
            break;
        }
    }
    
    if (!signature_match) {
        printf("[VFS] Storage Bypass: Drive is blank or uninitialized. Keeping system default files.\n");
        return;
    }
    
    printf("[VFS] Restoring storage state from physical sectors on hard drive...\n");
    
    uint16_t* buffer = (uint16_t*)&vfs_data;
    size_t total_sectors = (sizeof(vfs_data) + 511) / 512;
    
    for (size_t s = 0; s < total_sectors; s++) {
        uint16_t sector_buffer[256];
        ata_read_sector(100 + s, sector_buffer);
        for (int w = 0; w < 256; w++) {
            size_t idx = s * 256 + w;
            if (idx < (sizeof(vfs_data) / 2)) {
                buffer[idx] = sector_buffer[w];
            }
        }
    }
    printf("[VFS] Restore complete. State recovered from disk.\n");
}

// ---------------------- RUNTIME EXECUTION GATEWAY ----------------------

void run_easec_with_args(const char* code, const char* arg1, const char* arg2) {
    tokens = NULL;
    token_count = 0;
    token_cap = 0;
    current_token = 0;
    is_break = false;
    is_return = false;
    
    tokenize(code);
    ASTNode* ast = parse();
    Env* global_env = new_env(NULL);
    
    if (arg1) {
        env_define(global_env, "arg1", val_string(arg1));
    } else {
        env_define(global_env, "arg1", val_string(""));
    }
    if (arg2) {
        env_define(global_env, "arg2", val_string(arg2));
    } else {
        env_define(global_env, "arg2", val_string(""));
    }
    
    ValueList* list = new_list();
    for (int i = 0; i < MAX_MOCK_FILES; i++) {
        if (vfs[i].active) {
            list_add(list, val_string(vfs[i].name));
        }
    }
    Value v;
    v.type = VAL_ARRAY;
    v.as.arr = list;
    env_define(global_env, "sys_files", v);
    
    for(int i=0; i<ast->body_count; i++) {
        exec_stmt(ast->body[i], global_env);
    }
}

void run_easec(const char* code) {
    run_easec_with_args(code, NULL, NULL);
}

// ---------------------- COMMAND LINE STRING PARSER ----------------------

void parse_input_line(const char* input, char* cmd, char* arg1, char* arg2) {
    cmd[0] = '\0';
    arg1[0] = '\0';
    arg2[0] = '\0';
    
    size_t len = strlen(input);
    size_t i = 0;
    
    while (i < len && isspace(input[i])) i++;
    if (i >= len) return;
    
    size_t c_idx = 0;
    while (i < len && !isspace(input[i])) {
        cmd[c_idx++] = input[i++];
    }
    cmd[c_idx] = '\0';
    
    while (i < len && isspace(input[i])) i++;
    if (i >= len) return;
    
    size_t a1_idx = 0;
    if (input[i] == '"') {
        i++; 
        while (i < len && input[i] != '"') {
            arg1[a1_idx++] = input[i++];
        }
        if (i < len) i++; 
    } else {
        while (i < len && !isspace(input[i])) {
            arg1[a1_idx++] = input[i++];
        }
    }
    arg1[a1_idx] = '\0';
    
    while (i < len && isspace(input[i])) i++;
    if (i >= len) return;
    
    size_t a2_idx = 0;
    if (input[i] == '"') {
        i++; 
        while (i < len && input[i] != '"') {
            if (input[i] == '\\' && i + 1 < len && input[i+1] == '"') {
                arg2[a2_idx++] = '"';
                i += 2;
            } else {
                arg2[a2_idx++] = input[i++];
            }
        }
    } else {
        while (i < len) {
            arg2[a2_idx++] = input[i++];
        }
    }
    arg2[a2_idx] = '\0';
}

// ---------------------- KERNEL ENTRY POINT ----------------------

#include "embedded_files.h"

char* get_embedded_script(const char* name) {
    for (int i = 0; embedded_files[i].name != NULL; i++) {
        if (strcmp(embedded_files[i].name, name) == 0) {
            return (char*)embedded_files[i].code;
        }
    }
    return NULL;
}

void kernel_main(void) {
    terminal_clear();
    
    printf("====================================================================\n");
    printf("                        WELCOME TO inpsos!                          \n");
    printf("====================================================================\n\n");
    
    printf("[System] Checking primary IDE/SATA channel status...\n");
    if (ata_present()) {
        printf("[System] Hardware Detected: Hard disk found.\n");
        vfs_load_from_disk();
    } else {
        printf("[System] Drive not detected. Defaulting to standard session.\n");
    }

    for (int i = 0; embedded_files[i].name != NULL; i++) {
        char filename_buf[64];
        sprintf(filename_buf, "%s.easec", embedded_files[i].name);
        mock_file_create(filename_buf, embedded_files[i].code);
    }
    
    printf("[System] Core commands loaded successfully into Easec script layer.\n");
    printf("[System] Loading interactive shell subsystem...\n\n");
    
    char input_buf[256];
    char cmd[64];
    char arg1[128];
    char arg2[1024];
    
    while (1) {
        printf("inpsos> ");
        kbd_gets(input_buf, 256);
        
        size_t len = strlen(input_buf);
        while (len > 0 && (input_buf[len - 1] == '\r' || input_buf[len - 1] == '\n' || input_buf[len - 1] == ' ')) {
            input_buf[len - 1] = '\0';
            len--;
        }
        
        if (strlen(input_buf) == 0) continue;
        
        parse_input_line(input_buf, cmd, arg1, arg2);
        
        char cmd_file[128];
        sprintf(cmd_file, "%s.easec", cmd);
        
        char* script_code = mock_file_read(cmd_file);
        if (script_code) {
            run_easec_with_args(script_code, arg1, arg2);
        } else {
            char* direct_code = mock_file_read(cmd);
            if (direct_code) {
                run_easec_with_args(direct_code, arg1, arg2);
            } else {
                printf("Error: Command or script '%s' not found.\n", cmd);
            }
        }
        printf("\n");
    }
}