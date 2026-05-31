#include "libc_shim.h"
#include <stdarg.h>

extern void* kmalloc(size_t size);
extern void kfree(void* ptr);
extern void* krealloc(void* ptr, size_t size);
extern void kprint(const char* str);
extern void kprint_char(char c);
extern void kget_string(char* buffer, int max_len);

extern bool create_file(const char* name, const char* content, size_t size);
extern bool read_file(const char* name, char* output_buffer, size_t max_size);
extern bool delete_file_fs(const char* name);

// Setup structural mock mapping context for freestanding Files
struct FILE {
    char filename[32];
    char mode[4];
    char* buffer;
    size_t size;
    size_t position;
    bool is_write;
};

// Global standard streams
FILE* stderr_stream = (FILE*)1;
FILE* stdin_stream = (FILE*)2;

// Standard library locale lookup table setup
static unsigned short ctype_table[384] = {0};

void init_ctype_table() {
    for (int i = 0; i < 256; i++) {
        unsigned short mask = 0;
        if (i >= 'a' && i <= 'z') mask |= 0x200 | 0x400 | 0x8 | 0x4000 | 0x8000;
        if (i >= 'A' && i <= 'Z') mask |= 0x100 | 0x400 | 0x8 | 0x4000 | 0x8000;
        if (i >= '0' && i <= '9') mask |= 0x800 | 0x8 | 0x4000 | 0x8000;
        if (i == ' ' || i == '\t' || i == '\r' || i == '\n' || i == '\v' || i == '\f') mask |= 0x2000;
        ctype_table[128 + i] = mask;
    }
}

extern "C" {
    FILE* stderr = stderr_stream;
    FILE* stdin = stdin_stream;

    const unsigned short int **__ctype_b_loc(void) {
        static const unsigned short int *ctype_ptr = &ctype_table[128];
        return &ctype_ptr;
    }

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

    long long strtoll(const char* nptr, char** endptr, int base) {
        (void)base;
        if (endptr) *endptr = (char*)nptr + strlen(nptr);
        return atoll(nptr);
    }

    double strtod(const char* nptr, char** endptr) {
        if (endptr) *endptr = (char*)nptr + strlen(nptr);
        return atof(nptr);
    }

    int isalpha(int c) {
        return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
    }

    int isdigit(int c) {
        return (c >= '0' && c <= '9');
    }

    int printf(const char* format, ...) {
        kprint(format);
        return 0;
    }

    int fprintf(FILE* stream, const char* format, ...) {
        (void)stream;
        kprint(format);
        return 0;
    }

    int vfprintf(FILE* stream, const char* format, va_list arg) {
        (void)stream; (void)arg;
        kprint(format);
        return 0;
    }

    int fputs(const char* s, FILE* stream) {
        (void)stream;
        kprint(s);
        return 0;
    }

    void exit(int status) {
        (void)status;
        kprint("\nSystem halted.\n");
        while (1) { asm volatile ("hlt"); }
    }

    // Bare-metal File I/O mappings to the direct SATA simple filesystem interface
    FILE* fopen(const char* filename, const char* mode) {
        FILE* f = (FILE*)kmalloc(sizeof(FILE));
        memset(f, 0, sizeof(FILE));
        strncpy(f->filename, filename, 31);
        strncpy(f->mode, mode, 3);

        if (mode[0] == 'r') {
            char* temp = (char*)kmalloc(16384); // Allocates a maximum read limit space
            if (read_file(filename, temp, 16384)) {
                f->size = strlen(temp);
                f->buffer = (char*)kmalloc(f->size + 1);
                memcpy(f->buffer, temp, f->size);
                f->buffer[f->size] = '\0';
                f->position = 0;
                f->is_write = false;
                kfree(temp);
                return f;
            }
            kfree(temp);
            kfree(f);
            return nullptr;
        } else if (mode[0] == 'w' || mode[0] == 'a') {
            f->is_write = true;
            f->buffer = (char*)kmalloc(4096);
            f->buffer[0] = '\0';
            f->size = 0;
            f->position = 0;
            return f;
        }
        kfree(f);
        return nullptr;
    }

    int fclose(FILE* stream) {
        if (stream == stderr_stream || stream == stdin_stream || !stream) return 0;
        if (stream->is_write) {
            create_file(stream->filename, stream->buffer, strlen(stream->buffer));
        }
        if (stream->buffer) kfree(stream->buffer);
        kfree(stream);
        return 0;
    }

    size_t fread(void* ptr, size_t size, size_t nmemb, FILE* stream) {
        if (!stream || stream == stderr_stream || stream == stdin_stream) return 0;
        size_t total_bytes = size * nmemb;
        size_t available = stream->size - stream->position;
        size_t to_read = total_bytes < available ? total_bytes : available;
        memcpy(ptr, stream->buffer + stream->position, to_read);
        stream->position += to_read;
        return to_read / size;
    }

    int fseek(FILE* stream, long int offset, int whence) {
        if (!stream || stream == stderr_stream || stream == stdin_stream) return -1;
        if (whence == 0) { // SEEK_SET
            stream->position = offset;
        } else if (whence == 1) { // SEEK_CUR
            stream->position += offset;
        } else if (whence == 2) { // SEEK_END
            stream->position = stream->size + offset;
        }
        return 0;
    }

    long int ftell(FILE* stream) {
        if (!stream || stream == stderr_stream || stream == stdin_stream) return -1;
        return stream->position;
    }

    int remove(const char* filename) {
        return delete_file_fs(filename) ? 0 : -1;
    }

    char* fgets(char* s, int size, FILE* stream) {
        if (stream == stdin_stream) {
            kget_string(s, size);
            return s;
        }
        return nullptr;
    }

    // Bare-metal timer setups without system interrupt hooks
    static long long mock_time_ms = 1000000;

    int gettimeofday(void* tv, void* tz) {
        (void)tz;
        struct tv_struct {
            long tv_sec;
            long tv_usec;
        } *t = (struct tv_struct*)tv;

        if (t) {
            mock_time_ms += 10; // Virtual forward clock ticks
            t->tv_sec = mock_time_ms / 1000;
            t->tv_usec = (mock_time_ms % 1000) * 1000;
        }
        return 0;
    }

    int nanosleep(const void* req, void* rem) {
        (void)req; (void)rem;
        for (volatile int i = 0; i < 2000000; i++); // Busy spin loop to emulate timed wait
        return 0;
    }
}