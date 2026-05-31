#pragma once
#include <stddef.h>
#include <stdint.h>

extern "C" {
    void init_ctype_table();
    void* malloc(size_t size);
    void free(void* ptr);
    void* realloc(void* ptr, size_t size);
    void* memset(void* dest, int val, size_t len);
    void* memcpy(void* dest, const void* src, size_t len);
    int memcmp(const void* s1, const void* s2, size_t n);
    int strcmp(const char* s1, const char* s2);
    int strncmp(const char* s1, const char* s2, size_t n);
    size_t strlen(const char* s);
    int sprintf(char* buf, const char* format, ...);
    int snprintf(char* buf, size_t size, const char* format, ...);
}