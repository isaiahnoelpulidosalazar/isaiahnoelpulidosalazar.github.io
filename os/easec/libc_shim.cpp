#include "libc_shim.h"

extern void* kmalloc(size_t size);
extern void kfree(void* ptr);
extern void* krealloc(void* ptr, size_t size);
extern void kprint(const char* str);
extern void kprint_char(char c);

extern "C" {
    void* malloc(size_t size) { return kmalloc(size); }
    void free(void* ptr) { kfree(ptr); }
    void* realloc(void* ptr, size_t size) { return krealloc(ptr, size); }

    void* memset(void* dest, int val, size_t len) {
        unsigned char* ptr = (unsigned char*)dest;
        while (len-- > 0) *ptr++ = (unsigned char)val;
        return dest;
    }

    void* memcpy(void* dest, const void* src, size_t len) {
        char* d = (char*)dest;
        const char* s = (const char*)src;
        while (len-- > 0) *d++ = *s++;
        return dest;
    }

    int memcmp(const void* s1, const void* s2, size_t n) {
        const unsigned char* p1 = (const unsigned char*)s1;
        const unsigned char* p2 = (const unsigned char*)s2;
        while (n-- > 0) {
            if (*p1 != *p2) return *p1 - *p2;
            p1++; p2++;
        }
        return 0;
    }

    int strcmp(const char* s1, const char* s2) {
        while (*s1 && (*s1 == *s2)) {
            s1++; s2++;
        }
        return *(const unsigned char*)s1 - *(const unsigned char*)s2;
    }

    int strncmp(const char* s1, const char* s2, size_t n) {
        while (n > 0 && *s1 && (*s1 == *s2)) {
            s1++; s2++; n--;
        }
        if (n == 0) return 0;
        return *(const unsigned char*)s1 - *(const unsigned char*)s2;
    }

    size_t strlen(const char* s) {
        size_t len = 0;
        while (*s++) len++;
        return len;
    }

    int atoi(const char* str) {
        int res = 0;
        for (int i = 0; str[i] != '\0'; ++i) res = res * 10 + str[i] - '0';
        return res;
    }

    long long atoll(const char* str) {
        long long res = 0;
        for (int i = 0; str[i] != '\0'; ++i) res = res * 10 + str[i] - '0';
        return res;
    }

    double atof(const char* str) {
        // Simple base-10 parsing fallback
        double res = 0.0;
        double factor = 1.0;
        bool decimal = false;
        for (int i = 0; str[i] != '\0'; ++i) {
            if (str[i] == '.') {
                decimal = true;
                continue;
            }
            if (decimal) factor /= 10.0f;
            int val = str[i] - '0';
            if (decimal) res += val * factor;
            else res = res * 10.0f + val;
        }
        return res;
    }

    char* strchr(const char* s, int c) {
        while (*s) {
            if (*s == (char)c) return (char*)s;
            s++;
        }
        return nullptr;
    }

    char* strrchr(const char* s, int c) {
        char* last = nullptr;
        while (*s) {
            if (*s == (char)c) last = (char*)s;
            s++;
        }
        return last;
    }

    size_t strcspn(const char* s1, const char* s2) {
        size_t len = 0;
        while (*s1) {
            const char* p = s2;
            while (*p) {
                if (*s1 == *p) return len;
                p++;
            }
            s1++; len++;
        }
        return len;
    }

    char* strncpy(char* dest, const char* src, size_t n) {
        size_t i;
        for (i = 0; i < n && src[i] != '\0'; i++) dest[i] = src[i];
        for (; i < n; i++) dest[i] = '\0';
        return dest;
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

    int isalpha(int c) {
        return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
    }

    int isdigit(int c) {
        return (c >= '0' && c <= '9');
    }

    // Bare-metal print mapping implementations
    int printf(const char* format, ...) {
        // Direct print mapping bypassing dynamic variadic arguments evaluation
        kprint(format);
        return 0;
    }

    int fprintf(void* stream, const char* format, ...) {
        (void)stream; // Silence unused parameter warning
        kprint(format);
        return 0;
    }
}