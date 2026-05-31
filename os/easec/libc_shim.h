#pragma once
#include <stddef.h>
#include <stdint.h>

// Override dynamic memory macros in the ported compiler to target kernel mapping APIs
extern "C" {
    inline void* malloc(size_t size);
    inline void free(void* ptr);
    inline void* realloc(void* ptr, size_t size);
    inline void* memset(void* dest, int val, size_t len);
    inline void* memcpy(void* dest, const void* src, size_t len);
    inline int memcmp(const void* s1, const void* s2, size_t n);
    inline int strcmp(const char* s1, const char* s2);
    inline int strncmp(const char* s1, const char* s2, size_t n);
    inline size_t strlen(const char* s);
    inline int sprintf(char* buf, const char* format, ...);
    inline int snprintf(char* buf, size_t size, const char* format, ...);
}