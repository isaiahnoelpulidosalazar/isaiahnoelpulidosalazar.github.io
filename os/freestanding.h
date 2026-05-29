#ifndef FREESTANDING_H
#define FREESTANDING_H

#define NULL ((void*)0)
#define EOF (-1)

typedef unsigned char uint8_t;
typedef unsigned short uint16_t;
typedef unsigned int uint32_t;
typedef unsigned long long uint64_t;
typedef int int32_t;
typedef long long int64_t;
typedef unsigned int size_t;
typedef unsigned int uintptr_t;

// Variable Arguments Support
typedef __builtin_va_list va_list;
#define va_start(v,l) __builtin_va_start(v,l)
#define va_end(v)     __builtin_va_end(v)
#define va_arg(v,l)   __builtin_va_arg(v,l)

// C library string definitions
size_t strlen(const char* s);
int strcmp(const char* s1, const char* s2);
int strncmp(const char* s1, const char* s2, size_t n);
char* strcpy(char* dest, const char* src);
char* strcat(char* dest, const char* src);
char* strncpy(char* dest, const char* src, size_t n);
size_t strcspn(const char* s, const char* reject);
char* strchr(const char* s, int c);
char* strrchr(const char* s, int c);
int isdigit(int c);
int isalpha(int c);
long long atoll(const char* s);
double atof(const char* s);

// Memory controls
void* memset(void* dest, int val, size_t len);
void* memcpy(void* dest, const void* src, size_t len);
int memcmp(const void* s1, const void* s2, size_t n);
void* malloc(size_t size);
void free(void* ptr);
void* realloc(void* ptr, size_t size);

// Standard simulated interfaces
typedef void FILE;
extern FILE* stderr;
extern FILE* stdin;
extern FILE* stdout;

int printf(const char* format, ...);
int snprintf(char* str, size_t size, const char* format, ...);
int sprintf(char* str, const char* format, ...);
int fputs(const char* str, FILE* stream);
void exit(int status);

long long get_time_ms();
void sleep_ms(long long ms);

#endif