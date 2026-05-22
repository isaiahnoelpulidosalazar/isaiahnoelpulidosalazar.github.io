#ifndef KERNEL_H
#define KERNEL_H

#define NULL ((void*)0)
typedef unsigned int size_t;
typedef unsigned char uint8_t;
typedef unsigned short uint16_t;
typedef unsigned int uint32_t;
typedef unsigned long long uint64_t;
typedef _Bool bool;
#define true 1
#define false 0

// Var args
typedef __builtin_va_list va_list;
#define va_start(v,l) __builtin_va_start(v,l)
#define va_end(v) __builtin_va_end(v)
#define va_arg(v,l) __builtin_va_arg(v,l)

// Exception Handling
typedef struct { uint32_t regs[6]; } jmp_buf[1];
int setjmp(jmp_buf buf);
void longjmp(jmp_buf buf, int val);

// Hardware I/O
static inline void outb(uint16_t port, uint8_t val) { asm volatile("outb %0, %1" : : "a"(val), "Nd"(port)); }
static inline void outw(uint16_t port, uint16_t val) { asm volatile("outw %0, %1" : : "a"(val), "Nd"(port)); }
static inline uint8_t inb(uint16_t port) { uint8_t ret; asm volatile("inb %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }
static inline uint16_t inw(uint16_t port) { uint16_t ret; asm volatile("inw %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }

// Memory Subsystem
void* malloc(size_t size);
void free(void* p);
void* calloc(size_t n, size_t size);
void* realloc(void* p, size_t size);

// String Subsystem
size_t strlen(const char* s);
char* strcpy(char* dest, const char* src);
char* strncpy(char* dest, const char* src, size_t n);
int strcmp(const char* s1, const char* s2);
int strncmp(const char* s1, const char* s2, size_t n);
int strcasecmp(const char* s1, const char* s2);
char* strrchr(const char* s, int c);
void* memcpy(void* dest, const void* src, size_t n);
void* memset(void* s, int c, size_t n);
void* memmove(void* dest, const void* src, size_t n);
size_t strcspn(const char* s, const char* reject);
char* strcat(char* dest, const char* src);

// Ctype & Parsing
int isspace(int c); int isdigit(int c); int isalpha(int c); int isalnum(int c);
long long atoll(const char* str);
long long strtoll(const char* str, char** endptr, int base);
double atof(const char* str);
double strtod(const char* str, char** endptr);

// OS Standard Library Interfaces
void os_printf(const char* fmt, ...);
int os_sprintf(char* buf, const char* fmt, ...);
void os_getline(char* buf, int max);
void vga_clear();

// Physical Hard Disk (ATA PIO)
void sys_install_os();
void try_load_os();

// Virtual Filesystem & Directories
extern char current_dir[128];
void vfs_init();
void vfs_resolve(const char* path, char* out);
char* vfs_read(const char* fname);
void vfs_write(const char* fname, const char* content);
void vfs_append(const char* fname, const char* content);
int vfs_delete(const char* fname);
void vfs_mkdir(const char* name);
void vfs_rmdir(const char* name);
void vfs_cd(const char* name);
void vfs_ls(const char* path);

// Easec Main Entry
extern jmp_buf easec_env;
void run_easec(const char* code, const char* arg);

#endif