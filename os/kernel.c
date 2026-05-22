#include "kernel.h"

// --- SETJMP / LONGJMP ---
__attribute__((naked)) int setjmp(jmp_buf buf) {
    asm volatile ("mov 4(%esp), %eax\n mov %ebx, 0(%eax)\n mov %esi, 4(%eax)\n mov %edi, 8(%eax)\n mov %ebp, 12(%eax)\n mov %esp, 16(%eax)\n mov 0(%esp), %edx\n mov %edx, 20(%eax)\n xor %eax, %eax\n ret\n");
}
__attribute__((naked)) void longjmp(jmp_buf buf, int val) {
    asm volatile ("mov 4(%esp), %edx\n mov 8(%esp), %eax\n test %eax, %eax\n jnz 1f\n inc %eax\n 1:\n mov 0(%edx), %ebx\n mov 4(%edx), %esi\n mov 8(%edx), %edi\n mov 12(%edx), %ebp\n mov 16(%edx), %esp\n mov 20(%edx), %ecx\n jmp *%ecx\n");
}

// --- MEMORY ALLOCATOR ---
extern char end; uint32_t heap_ptr = 0;
void* malloc(size_t size) { if (!heap_ptr) heap_ptr = (uint32_t)&end; if (size % 4) size += 4 - (size % 4); uint32_t tmp = heap_ptr; *(size_t*)tmp = size; heap_ptr += size + sizeof(size_t); return (void*)(tmp + sizeof(size_t)); }
void free(void* p) { (void)p; } 
void* calloc(size_t n, size_t size) { void* p = malloc(n * size); memset(p, 0, n * size); return p; }
void* realloc(void* p, size_t size) { if(!p) return malloc(size); if(size == 0) return NULL; size_t old_size = *((size_t*)p - 1); if (old_size >= size) return p; void* newp = malloc(size); memcpy(newp, p, old_size); return newp; }

// --- LIBC STRING & CTYPE ---
size_t strlen(const char* s) { size_t i=0; while(s[i]) i++; return i; }
char* strcpy(char* d, const char* s) { int i=0; while((d[i]=s[i])) i++; return d; }
char* strncpy(char* d, const char* s, size_t n) { size_t i=0; while(i<n && s[i]) {d[i]=s[i]; i++;} while(i<n) d[i++]='\0'; return d; }
int strcmp(const char* s1, const char* s2) { while(*s1 && (*s1==*s2)) { s1++; s2++; } return *(const unsigned char*)s1 - *(const unsigned char*)s2; }
int strncmp(const char* s1, const char* s2, size_t n) { while(n--) { if(*s1!=*s2) return *(const unsigned char*)s1 - *(const unsigned char*)s2; s1++; s2++; } return 0; }
int strcasecmp(const char* s1, const char* s2) { while(*s1) { char c1 = (*s1>='A'&&*s1<='Z')?*s1+32:*s1; char c2 = (*s2>='A'&&*s2<='Z')?*s2+32:*s2; if (c1 != c2) return c1 - c2; s1++; s2++; } return *s2 == 0 ? 0 : -1; }
char* strrchr(const char* s, int c) { const char* last = NULL; while(*s) { if (*s == (char)c) last = s; s++; } return (char*)last; }
void* memset(void* s, int c, size_t n) { unsigned char* p=s; while(n--) *p++=(unsigned char)c; return s; }
void* memcpy(void* d, const void* s, size_t n) { unsigned char* pd=d; const unsigned char* ps=s; while(n--) *pd++=*ps++; return d; }
void* memmove(void* d, const void* s, size_t n) { unsigned char* pd=d; const unsigned char* ps=s; if(pd<ps) while(n--) *pd++=*ps++; else { pd+=n; ps+=n; while(n--) *--pd=*--ps; } return d; }
size_t strcspn(const char* s, const char* rej) { size_t c=0; while(*s) { const char* r=rej; while(*r) { if(*s==*r) return c; r++; } s++; c++; } return c; }
char* strcat(char* d, const char* s) { strcpy(d + strlen(d), s); return d; }
int isspace(int c) { return c==' '||c=='\t'||c=='\n'||c=='\r'||c=='\f'||c=='\v'; }
int isdigit(int c) { return c>='0'&&c<='9'; }
int isalpha(int c) { return (c>='a'&&c<='z')||(c>='A'&&c<='Z'); }
int isalnum(int c) { return isalpha(c)||isdigit(c); }

long long atoll(const char* str) { long long res=0; int sign=1; while(isspace(*str)) str++; if(*str=='-') { sign=-1; str++; } else if(*str=='+') str++; while(isdigit(*str)) res = res*10 + (*str++ - '0'); return res*sign; }
double atof(const char* str) { double res=0, frac=1; int sign=1; while(isspace(*str)) str++; if(*str=='-') { sign=-1; str++; } else if(*str=='+') str++; while(isdigit(*str)) res = res*10 + (*str++ - '0'); if(*str=='.') { str++; while(isdigit(*str)) { res = res*10 + (*str++ - '0'); frac*=10; } } return sign * (res/frac); }
long long strtoll(const char* str, char** endptr, int base) { (void)base; if(endptr) *endptr = (char*)str + strlen(str); return atoll(str); }
double strtod(const char* str, char** endptr) { if(endptr) *endptr = (char*)str + strlen(str); return atof(str); }

void itoa(long long num, char* str) { int i = 0; bool is_neg = false; if (num == 0) { str[i++] = '0'; str[i] = '\0'; return; } if (num < 0) { is_neg = true; num = -num; } while(num != 0) { str[i++] = (num % 10) + '0'; num /= 10; } if (is_neg) str[i++] = '-'; str[i] = '\0'; for(int j=0; j<i/2; j++) { char t=str[j]; str[j]=str[i-1-j]; str[i-1-j]=t; } }
void ftoa(double n, char* res) { long long ipart = (long long)n; double fpart = n - (double)ipart; if(n < 0 && ipart == 0) { *res++ = '-'; fpart = -fpart; } else if (n < 0) fpart = -fpart; itoa(ipart, res); int len = strlen(res); res[len] = '.'; long long frac = (long long)(fpart * 1000000); itoa(frac, res + len + 1); }
int os_sprintf(char* buf, const char* fmt, ...) { va_list args; va_start(args, fmt); int i = 0; while(*fmt) { if (*fmt == '%') { fmt++; if (*fmt == 'l' && *(fmt+1) == 'l' && *(fmt+2) == 'd') { char tmp[64]; itoa(va_arg(args, long long), tmp); strcpy(&buf[i], tmp); i += strlen(tmp); fmt += 2; } else if (*fmt == 'g' || *fmt == 'f') { char tmp[64]; ftoa(va_arg(args, double), tmp); strcpy(&buf[i], tmp); i += strlen(tmp); } else if (*fmt == 's') { char* str = va_arg(args, char*); strcpy(&buf[i], str); i += strlen(str); } else if (*fmt == 'c') { buf[i++] = (char)va_arg(args, int); } } else { buf[i++] = *fmt; } fmt++; } buf[i] = '\0'; va_end(args); return i; }

// --- VGA DRIVER ---
uint16_t* vga = (uint16_t*)0xB8000; int cx = 0, cy = 0;
void vga_scroll() { if (cy >= 25) { for(int i=0; i<24*80; i++) vga[i] = vga[i + 80]; for(int i=24*80; i<25*80; i++) vga[i] = 0x0720; cy = 24; } }
void vga_putchar(char c) { if (c == '\n') { cx = 0; cy++; } else if (c == '\b') { if (cx > 0) cx--; else if (cy > 0) { cy--; cx = 79; } vga[cy * 80 + cx] = 0x0720; } else { vga[cy * 80 + cx] = (uint16_t)c | 0x0700; cx++; if (cx >= 80) { cx = 0; cy++; } } vga_scroll(); uint16_t pos = cy * 80 + cx; outb(0x3D4, 0x0F); outb(0x3D5, (uint8_t)(pos & 0xFF)); outb(0x3D4, 0x0E); outb(0x3D5, (uint8_t)((pos >> 8) & 0xFF)); }
void vga_clear() { for(int i=0; i<25*80; i++) vga[i] = 0x0720; cx = 0; cy = 0; }
void os_printf(const char* fmt, ...) { va_list args; va_start(args, fmt); while(*fmt) { if (*fmt == '%') { fmt++; if (*fmt == 'l' && *(fmt+1) == 'l' && *(fmt+2) == 'd') { char tmp[64]; itoa(va_arg(args, long long), tmp); for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]); fmt += 2; } else if (*fmt == 'g' || *fmt == 'f') { char tmp[64]; ftoa(va_arg(args, double), tmp); for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]); } else if (*fmt == 's') { char* str = va_arg(args, char*); for(int k=0; str[k]; k++) vga_putchar(str[k]); } else if (*fmt == 'c') { vga_putchar((char)va_arg(args, int)); } } else { vga_putchar(*fmt); } fmt++; } va_end(args); }

// --- KEYBOARD DRIVER ---
const char kbd_US[128] = { 0,27,'1','2','3','4','5','6','7','8','9','0','-','=','\b','\t','q','w','e','r','t','y','u','i','o','p','[',']','\n', 0,'a','s','d','f','g','h','j','k','l',';','\'','`',0,'\\','z','x','c','v','b','n','m',',','.','/',0,'*',0,' ',0 };
const char kbd_US_shift[128] = { 0,27,'!','@','#','$','%','^','&','*','(',')','_','+','\b','\t','Q','W','E','R','T','Y','U','I','O','P','{','}','\n', 0,'A','S','D','F','G','H','J','K','L',':','"','~',0,'|','Z','X','C','V','B','N','M','<','>','?',0,'*',0,' ',0 };
char os_getchar() { static bool shift = false; while(1) { if (inb(0x64) & 1) { uint8_t sc = inb(0x60); if (sc == 0x2A || sc == 0x36) shift = true; else if (sc == 0xAA || sc == 0xB6) shift = false; else if (!(sc & 0x80)) { char c = shift ? kbd_US_shift[sc] : kbd_US[sc]; if (c) return c; } } } }
void os_getline(char* buf, int max) { int i = 0; while(i < max - 1) { char c = os_getchar(); if (c == '\b') { if (i > 0) { i--; vga_putchar('\b'); vga_putchar(' '); vga_putchar('\b'); } } else if (c == '\n') { os_printf("\n"); buf[i] = '\0'; return; } else { os_printf("%c", c); buf[i++] = c; } } buf[i] = '\0'; }

// --- HARD DISK (ATA PIO) DRIVER ---
void ata_wait_bsy() { while(inb(0x1F7) & 0x80); }
void ata_wait_drq() { while(!(inb(0x1F7) & 0x08)); }
void ata_write_sector(uint32_t lba, uint8_t* buffer) { ata_wait_bsy(); outb(0x1F6, 0xE0 | ((lba >> 24) & 0x0F)); outb(0x1F2, 1); outb(0x1F3, (uint8_t) lba); outb(0x1F4, (uint8_t)(lba >> 8)); outb(0x1F5, (uint8_t)(lba >> 16)); outb(0x1F7, 0x30); ata_wait_bsy(); ata_wait_drq(); for(int i=0; i<256; i++) outw(0x1F0, ((uint16_t*)buffer)[i]); }
void ata_read_sector(uint32_t lba, uint8_t* buffer) { ata_wait_bsy(); outb(0x1F6, 0xE0 | ((lba >> 24) & 0x0F)); outb(0x1F2, 1); outb(0x1F3, (uint8_t) lba); outb(0x1F4, (uint8_t)(lba >> 8)); outb(0x1F5, (uint8_t)(lba >> 16)); outb(0x1F7, 0x20); ata_wait_bsy(); ata_wait_drq(); for(int i=0; i<256; i++) ((uint16_t*)buffer)[i] = inw(0x1F0); }

// --- VIRTUAL FILESYSTEM ---
#define MAX_FILES 128
typedef struct { char name[128]; char content[4096]; bool used; bool is_dir; } file_t;
file_t fs[MAX_FILES];
char current_dir[128] = "/";

void vfs_init() { for(int i=0; i<MAX_FILES; i++) fs[i].used = false; }
void vfs_resolve(const char* path, char* out) {
    if (path[0] == '/') strcpy(out, path);
    else { strcpy(out, current_dir); if (out[strlen(out)-1] != '/') strcat(out, "/"); strcat(out, path); }
}
char* vfs_read(const char* fname) { char res[256]; vfs_resolve(fname, res); for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !fs[i].is_dir && !strcmp(fs[i].name, res)) return fs[i].content; return NULL; }
void vfs_write(const char* fname, const char* content) { char res[256]; vfs_resolve(fname, res); for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, res)) { strncpy(fs[i].content, content, 4095); return; } for(int i=0; i<MAX_FILES; i++) if(!fs[i].used) { fs[i].used = true; fs[i].is_dir = false; strcpy(fs[i].name, res); strncpy(fs[i].content, content, 4095); return; } }
void vfs_append(const char* fname, const char* content) { char res[256]; vfs_resolve(fname, res); for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, res)) { strcat(fs[i].content, content); return; } }
int vfs_delete(const char* fname) { char res[256]; vfs_resolve(fname, res); for(int i=0; i<MAX_FILES; i++) if(fs[i].used && !strcmp(fs[i].name, res)) { fs[i].used = false; return 0; } return -1; }
void vfs_mkdir(const char* name) { char res[256]; vfs_resolve(name, res); for(int i=0; i<MAX_FILES; i++) if(fs[i].used && strcmp(fs[i].name, res) == 0) return; for(int i=0; i<MAX_FILES; i++) if(!fs[i].used) { fs[i].used = true; fs[i].is_dir = true; strcpy(fs[i].name, res); fs[i].content[0] = '\0'; return; } }
void vfs_rmdir(const char* name) {
    char res[256]; vfs_resolve(name, res); int len = strlen(res);
    for(int i=0; i<MAX_FILES; i++) { if(fs[i].used && strncmp(fs[i].name, res, len) == 0) { fs[i].used = false; } }
}
void vfs_cd(const char* name) {
    if (!name || strlen(name) == 0) return;
    if (strcmp(name, "..") == 0) { char* last_slash = strrchr(current_dir, '/'); if(last_slash && last_slash != current_dir) *last_slash = '\0'; else strcpy(current_dir, "/"); return; }
    if (strcmp(name, "/") == 0) { strcpy(current_dir, "/"); return; }
    char res[256]; vfs_resolve(name, res); int len = strlen(res); if(len > 1 && res[len-1] == '/') res[len-1] = '\0';
    for(int i=0; i<MAX_FILES; i++) if(fs[i].used && fs[i].is_dir && strcmp(fs[i].name, res) == 0) { strcpy(current_dir, res); return; }
    os_printf("Directory not found.\n");
}
void vfs_ls(const char* path) {
    char target[256]; if(path && strlen(path) > 0) vfs_resolve(path, target); else strcpy(target, current_dir);
    int tlen = strlen(target); if(tlen > 1 && target[tlen-1] != '/') { target[tlen] = '/'; target[tlen+1] = '\0'; tlen++; }
    if(strcmp(target, "/") == 0) tlen = 1;
    for(int i=0; i<MAX_FILES; i++) { if(fs[i].used) { if(strncmp(fs[i].name, target, tlen) == 0 || (strcmp(target, "/") == 0)) os_printf("  %s %s\n", fs[i].is_dir ? "[DIR]" : "[FILE]", fs[i].name); } }
}

void sys_install_os() {
    uint8_t magic[512] = "1NPS_VFS_1.0"; ata_write_sector(1, magic);
    uint8_t* fs_ptr = (uint8_t*)fs; uint32_t num_sectors = sizeof(fs) / 512 + 1;
    for(uint32_t i=0; i<num_sectors; i++) ata_write_sector(2 + i, fs_ptr + (i * 512));
    os_printf("OS Filesystem installed to physical hard disk!\nChanges will now persist across reboots.\n");
}

bool try_load_os() {
    uint8_t magic[512]; ata_read_sector(1, magic);
    if(strncmp((char*)magic, "1NPS_VFS_1.0", 12) == 0) {
        uint8_t* fs_ptr = (uint8_t*)fs; uint32_t num_sectors = sizeof(fs) / 512 + 1;
        for(uint32_t i=0; i<num_sectors; i++) ata_read_sector(2 + i, fs_ptr + (i * 512));
        os_printf("Restored system from hard disk.\n");
        return true;
    }
    return false;
}

// --- MAIN OS ENTRY ---
jmp_buf easec_env;

void kernel_main(void) {
    vga_clear();
    vfs_init();
    
    if(!try_load_os()) {
        // First boot defaults
        vfs_mkdir("/easec");
        vfs_write("/easec/clear.easec", "var tmp = sys_clear");
        vfs_write("/easec/shutdown.easec", "var tmp = sys_shutdown");
        vfs_write("/easec/restart.easec", "var tmp = sys_restart");
        vfs_write("/easec/install.easec", "var tmp = sys_install");
        vfs_write("/easec/create_file.easec", "say \"Filename:\"\nvar fname = get\nsay \"Content:\"\nvar fcontent = get\nfile create fname [ fcontent ]");
        vfs_write("/easec/delete_file.easec", "say \"Delete target:\"\nvar fname = get\nfile delete fname");
        vfs_write("/easec/list.easec", "var tmp = sys_ls");
        vfs_write("/easec/change_directory.easec", "var tmp = sys_cd");
        vfs_write("/easec/create_folder.easec", "var tmp = sys_mkdir");
        vfs_write("/easec/delete_folder.easec", "var tmp = sys_rmdir");
        vfs_write("/easec/demo.easec", "say \"--- EASEC DEMO ---\"\nsay \"Variables:\"\nvar number mynum = 42\nsay mynum\nsay \"Arrays:\"\narray number arr [10, 20]\nsay arr[1]\nsay \"Conditionals:\"\nif mynum == 42 [\nsay \"Number is 42!\"\n] else [\nsay \"No\"\n]\nsay \"Loops:\"\nvar i = 0\nrepeat 3 [\nsay i\nincrement i 1\n]\nsay \"Jobs:\"\njob greet x [\nsay \"Hello \" + x\n]\ngreet \"User!\"\nsay \"Demo Complete.\"");
    }

    os_printf("Welcome to inpsos powered by Easec OS Shell!\n");

    char line[256];
    while (1) {
        os_printf("root@inpsos:%s> ", current_dir);
        os_getline(line, 256);
        if (strlen(line) == 0) continue;
        
        char cmd[64] = {0}; char arg[192] = {0};
        int i=0, j=0;
        while(line[i] && line[i] != ' ') cmd[j++] = line[i++];
        if (line[i] == ' ') {
            i++; while(line[i] == ' ') i++;
            if (line[i] == '"') { i++; int k = 0; while(line[i] && line[i] != '"') arg[k++] = line[i++]; }
            else { int k = 0; while(line[i]) arg[k++] = line[i++]; }
        }
        
        char path[128]; os_sprintf(path, "/easec/%s.easec", cmd);
        char* code = vfs_read(path);
        if (code) { if (setjmp(easec_env) == 0) run_easec(code, arg); else os_printf("Easec Error.\n"); }
        else os_printf("Error: Command not found: %s\n", cmd);
    }
}