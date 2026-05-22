#include "kernel.h"

// --- SETJMP / LONGJMP (Exception handling for easec parser) ---
__attribute__((naked)) int setjmp(jmp_buf buf) {
    asm volatile (
        "mov 4(%esp), %eax\n"
        "mov %ebx, 0(%eax)\n" "mov %esi, 4(%eax)\n"
        "mov %edi, 8(%eax)\n" "mov %ebp, 12(%eax)\n"
        "mov %esp, 16(%eax)\n"
        "mov 0(%esp), %edx\n" "mov %edx, 20(%eax)\n"
        "xor %eax, %eax\n" "ret\n"
    );
}

__attribute__((naked)) void longjmp(jmp_buf buf, int val) {
    asm volatile (
        "mov 4(%esp), %edx\n" "mov 8(%esp), %eax\n"
        "test %eax, %eax\n" "jnz 1f\n" "inc %eax\n" "1:\n"
        "mov 0(%edx), %ebx\n" "mov 4(%edx), %esi\n"
        "mov 8(%edx), %edi\n" "mov 12(%edx), %ebp\n"
        "mov 16(%edx), %esp\n" "mov 20(%edx), %ecx\n"
        "jmp *%ecx\n"
    );
}

// --- MEMORY ALLOCATOR ---
extern char end; // defined in linker script
uint32_t heap_ptr = 0;

void* malloc(size_t size) {
    if (!heap_ptr) heap_ptr = (uint32_t)&end;
    if (size % 4) size += 4 - (size % 4); // 4-byte alignment
    uint32_t tmp = heap_ptr;
    *(size_t*)tmp = size;
    heap_ptr += size + sizeof(size_t);
    return (void*)(tmp + sizeof(size_t));
}

void free(void* p) { (void)p; } // Bump allocator ignores free

void* calloc(size_t n, size_t size) {
    void* p = malloc(n * size);
    memset(p, 0, n * size);
    return p;
}

void* realloc(void* p, size_t size) {
    if(!p) return malloc(size);
    if(size == 0) { return NULL; }
    size_t old_size = *((size_t*)p - 1);
    if (old_size >= size) return p; 
    void* newp = malloc(size);
    memcpy(newp, p, old_size);
    return newp;
}

// --- LIBC STRING & CTYPE ---
size_t strlen(const char* s) { size_t i=0; while(s[i]) i++; return i; }
char* strcpy(char* d, const char* s) { int i=0; while((d[i]=s[i])) i++; return d; }
char* strncpy(char* d, const char* s, size_t n) { size_t i=0; while(i<n && s[i]) {d[i]=s[i]; i++;} while(i<n) d[i++]='\0'; return d; }
int strcmp(const char* s1, const char* s2) { while(*s1 && (*s1==*s2)) { s1++; s2++; } return *(const unsigned char*)s1 - *(const unsigned char*)s2; }
int strncmp(const char* s1, const char* s2, size_t n) { while(n--) { if(*s1!=*s2) return *(const unsigned char*)s1 - *(const unsigned char*)s2; s1++; s2++; } return 0; }
int strcasecmp(const char* s1, const char* s2) {
    while(*s1) {
        char c1 = (*s1>='A'&&*s1<='Z')?*s1+32:*s1; char c2 = (*s2>='A'&&*s2<='Z')?*s2+32:*s2;
        if (c1 != c2) return c1 - c2; s1++; s2++;
    } return *s2 == 0 ? 0 : -1;
}
void* memset(void* s, int c, size_t n) { unsigned char* p=s; while(n--) *p++=(unsigned char)c; return s; }
void* memcpy(void* d, const void* s, size_t n) { unsigned char* pd=d; const unsigned char* ps=s; while(n--) *pd++=*ps++; return d; }
void* memmove(void* d, const void* s, size_t n) { 
    unsigned char* pd=d; const unsigned char* ps=s;
    if(pd<ps) while(n--) *pd++=*ps++; else { pd+=n; ps+=n; while(n--) *--pd=*--ps; } return d;
}
size_t strcspn(const char* s, const char* rej) { 
    size_t c=0; while(*s) { const char* r=rej; while(*r) { if(*s==*r) return c; r++; } s++; c++; } return c; 
}
char* strcat(char* d, const char* s) { strcpy(d + strlen(d), s); return d; }

int isspace(int c) { return c==' '||c=='\t'||c=='\n'||c=='\r'||c=='\f'||c=='\v'; }
int isdigit(int c) { return c>='0'&&c<='9'; }
int isalpha(int c) { return (c>='a'&&c<='z')||(c>='A'&&c<='Z'); }
int isalnum(int c) { return isalpha(c)||isdigit(c); }

long long atoll(const char* str) {
    long long res=0; int sign=1; while(isspace(*str)) str++;
    if(*str=='-') { sign=-1; str++; } else if(*str=='+') str++;
    while(isdigit(*str)) res = res*10 + (*str++ - '0'); return res*sign;
}
double atof(const char* str) {
    double res=0, frac=1; int sign=1; while(isspace(*str)) str++;
    if(*str=='-') { sign=-1; str++; } else if(*str=='+') str++;
    while(isdigit(*str)) res = res*10 + (*str++ - '0');
    if(*str=='.') { str++; while(isdigit(*str)) { res = res*10 + (*str++ - '0'); frac*=10; } }
    return sign * (res/frac);
}
long long strtoll(const char* str, char** endptr, int base) { (void)base; if(endptr) *endptr = (char*)str + strlen(str); return atoll(str); }
double strtod(const char* str, char** endptr) { if(endptr) *endptr = (char*)str + strlen(str); return atof(str); }

// --- PRINTF / I/O FORMATTING ---
void itoa(long long num, char* str) {
    int i = 0; bool is_neg = false;
    if (num == 0) { str[i++] = '0'; str[i] = '\0'; return; }
    if (num < 0) { is_neg = true; num = -num; }
    while(num != 0) { str[i++] = (num % 10) + '0'; num /= 10; }
    if (is_neg) str[i++] = '-';
    str[i] = '\0';
    for(int j=0; j<i/2; j++) { char t=str[j]; str[j]=str[i-1-j]; str[i-1-j]=t; }
}

void ftoa(double n, char* res) {
    long long ipart = (long long)n; double fpart = n - (double)ipart;
    if(n < 0 && ipart == 0) { *res++ = '-'; fpart = -fpart; } else if (n < 0) fpart = -fpart;
    itoa(ipart, res); int len = strlen(res); res[len] = '.';
    long long frac = (long long)(fpart * 1000000); // 6 precision points
    itoa(frac, res + len + 1);
}

int os_sprintf(char* buf, const char* fmt, ...) {
    va_list args; va_start(args, fmt); int i = 0;
    while(*fmt) {
        if (*fmt == '%') {
            fmt++;
            if (*fmt == 'l' && *(fmt+1) == 'l' && *(fmt+2) == 'd') {
                char tmp[64]; itoa(va_arg(args, long long), tmp);
                strcpy(&buf[i], tmp); i += strlen(tmp); fmt += 2;
            } else if (*fmt == 'g' || *fmt == 'f') {
                char tmp[64]; ftoa(va_arg(args, double), tmp);
                strcpy(&buf[i], tmp); i += strlen(tmp);
            } else if (*fmt == 's') {
                char* str = va_arg(args, char*); strcpy(&buf[i], str); i += strlen(str);
            } else if (*fmt == 'c') { buf[i++] = (char)va_arg(args, int); }
        } else { buf[i++] = *fmt; } fmt++;
    }
    buf[i] = '\0'; va_end(args); return i;
}

// --- VGA DRIVER ---
uint16_t* vga = (uint16_t*)0xB8000; int cx = 0, cy = 0;

void vga_scroll() {
    if (cy >= 25) {
        for(int i=0; i<24*80; i++) vga[i] = vga[i + 80];
        for(int i=24*80; i<25*80; i++) vga[i] = 0x0720;
        cy = 24;
    }
}

void vga_putchar(char c) {
    if (c == '\n') { cx = 0; cy++; }
    else if (c == '\b') {
        if (cx > 0) cx--; else if (cy > 0) { cy--; cx = 79; }
        vga[cy * 80 + cx] = 0x0720;
    }
    else { vga[cy * 80 + cx] = (uint16_t)c | 0x0700; cx++; if (cx >= 80) { cx = 0; cy++; } }
    vga_scroll();
    uint16_t pos = cy * 80 + cx;
    outb(0x3D4, 0x0F); outb(0x3D5, (uint8_t)(pos & 0xFF));
    outb(0x3D4, 0x0E); outb(0x3D5, (uint8_t)((pos >> 8) & 0xFF));
}

void vga_clear() {
    for(int i=0; i<25*80; i++) vga[i] = 0x0720;
    cx = 0; cy = 0;
}

void os_printf(const char* fmt, ...) {
    char buf[1024]; va_list args; va_start(args, fmt);
    // Reuse custom string formatter
    int i=0;
    while(*fmt) {
        if (*fmt == '%') {
            fmt++;
            if (*fmt == 'l' && *(fmt+1) == 'l' && *(fmt+2) == 'd') {
                char tmp[64]; itoa(va_arg(args, long long), tmp);
                for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]); fmt += 2;
            } else if (*fmt == 'g' || *fmt == 'f') {
                char tmp[64]; ftoa(va_arg(args, double), tmp);
                for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]);
            } else if (*fmt == 's') {
                char* str = va_arg(args, char*);
                for(int k=0; str[k]; k++) vga_putchar(str[k]);
            } else if (*fmt == 'c') { vga_putchar((char)va_arg(args, int)); }
        } else { vga_putchar(*fmt); } fmt++;
    }
    va_end(args);
}

// --- KEYBOARD DRIVER ---
const char kbd_US[128] = {
    0,27,'1','2','3','4','5','6','7','8','9','0','-','=','\b','\t','q','w','e','r','t','y','u','i','o','p','[',']','\n',
    0,'a','s','d','f','g','h','j','k','l',';','\'','`',0,'\\','z','x','c','v','b','n','m',',','.','/',0,'*',0,' ',0
};
const char kbd_US_shift[128] = {
    0,27,'!','@','#','$','%','^','&','*','(',')','_','+','\b','\t','Q','W','E','R','T','Y','U','I','O','P','{','}','\n',
    0,'A','S','D','F','G','H','J','K','L',':','"','~',0,'|','Z','X','C','V','B','N','M','<','>','?',0,'*',0,' ',0
};

char os_getchar() {
    static bool shift = false;
    while(1) {
        if (inb(0x64) & 1) {
            uint8_t sc = inb(0x60);
            if (sc == 0x2A || sc == 0x36) shift = true;
            else if (sc == 0xAA || sc == 0xB6) shift = false;
            else if (!(sc & 0x80)) {
                char c = shift ? kbd_US_shift[sc] : kbd_US[sc];
                if (c) return c;
            }
        }
    }
}

void os_getline(char* buf, int max) {
    int i = 0;
    while(i < max - 1) {
        char c = os_getchar();
        if (c == '\b') {
            if (i > 0) { i--; vga_putchar('\b'); vga_putchar(' '); vga_putchar('\b'); }
        } else if (c == '\n') { os_printf("\n"); buf[i] = '\0'; return; }
        else { os_printf("%c", c); buf[i++] = c; }
    }
    buf[i] = '\0';
}

// --- VIRTUAL FILESYSTEM ---
#define MAX_FILES 64
struct { char name[64]; char content[4096]; bool used; } fs[MAX_FILES];

void vfs_init() { for(int i=0; i<MAX_FILES; i++) fs[i].used = false; }
char* vfs_read(const char* fname) { for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, fname)) return fs[i].content; return NULL; }
void vfs_write(const char* fname, const char* content) {
    for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, fname)) { strncpy(fs[i].content, content, 4095); return; }
    for(int i=0; i<MAX_FILES; i++) if(!fs[i].used) { fs[i].used = true; strcpy(fs[i].name, fname); strncpy(fs[i].content, content, 4095); return; }
}
void vfs_append(const char* fname, const char* content) {
    for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, fname)) { strcat(fs[i].content, content); return; }
}
int vfs_delete(const char* fname) {
    for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, fname)) { fs[i].used = false; return 0; }
    return -1;
}

// --- MAIN OS ENTRY ---
jmp_buf easec_env;

void kernel_main(void) {
    vga_clear();
    vfs_init();
    
    // Add Requested OS Script Commands
    vfs_write("/easec/clear.easec", "var tmp = sys_clear");
    vfs_write("/easec/shutdown.easec", "var tmp = sys_shutdown");
    vfs_write("/easec/restart.easec", "var tmp = sys_restart");
    vfs_write("/easec/install.easec", "var tmp = sys_install");
    vfs_write("/easec/create_file.easec", "say \"Enter filename:\"\nvar fname = get\nsay \"Enter content:\"\nvar fcontent = get\nfile create fname [ fcontent ]");
    vfs_write("/easec/delete_file.easec", "say \"Enter filename to delete:\"\nvar fname = get\nfile delete fname");

    os_printf("Welcome to inpsos powered by Easec OS Shell!\n");
    os_printf("Available commands: clear, shutdown, restart, install, create_file, delete_file\n");

    char line[256];
    while (1) {
        os_printf("> ");
        os_getline(line, 256);
        if (strlen(line) == 0) continue;
        
        char path[128];
        os_sprintf(path, "/easec/%s.easec", line);
        
        char* code = vfs_read(path);
        if (code) {
            if (setjmp(easec_env) == 0) run_easec(code);
            else os_printf("Easec Error.\n");
        } else {
            os_printf("Error: Command/File not found: %s\n", path);
        }
    }
}