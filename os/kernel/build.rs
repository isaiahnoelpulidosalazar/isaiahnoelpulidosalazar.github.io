use std::env;
use std::fs;
use std::path::PathBuf;

fn main() {
    let out_dir = PathBuf::from(env::var("OUT_DIR").unwrap());
    let include_dir = out_dir.join("include");
    fs::create_dir_all(&include_dir).unwrap();
    
    // Dynamic generation of bare-metal C mapping interfaces
    fs::write(include_dir.join("stdio.h"), "
        #pragma once
        #include <stdarg.h>
        #include <stddef.h>
        typedef struct FILE FILE;
        extern FILE* stdin; extern FILE* stdout; extern FILE* stderr;
        int printf(const char *format, ...);
        int fprintf(FILE *stream, const char *format, ...);
        int snprintf(char *str, size_t size, const char *format, ...);
        FILE *fopen(const char *pathname, const char *mode);
        int fclose(FILE *stream);
        size_t fread(void *ptr, size_t size, size_t nmemb, FILE *stream);
        int fputs(const char *s, FILE *stream);
        int fseek(FILE *stream, long offset, int whence);
        long ftell(FILE *stream);
        int remove(const char *pathname);
        char *fgets(char *s, int size, FILE *stream);
        #define SEEK_END 2
        #define SEEK_SET 0
    ").unwrap();

    fs::write(include_dir.join("stdlib.h"), "
        #pragma once
        #include <stddef.h>
        void *malloc(size_t size); void free(void *ptr); void *realloc(void *ptr, size_t size);
        void exit(int status); double atof(const char *nptr); long long atoll(const char *nptr);
    ").unwrap();

    fs::write(include_dir.join("string.h"), "
        #pragma once
        #include <stddef.h>
        void *memcpy(void *dest, const void *src, size_t n);
        void *memset(void *s, int c, size_t n);
        size_t strlen(const char *s);
        char *strcpy(char *dest, const char *src);
        char *strncpy(char *dest, const char *src, size_t n);
        int strcmp(const char *s1, const char *s2);
        int strncmp(const char *s1, const char *s2, size_t n);
        char *strcat(char *dest, const char *src);
        char *strchr(const char *s, int c);
        char *strrchr(const char *s, int c);
        int memcmp(const void *s1, const void *s2, size_t n);
        size_t strcspn(const char *s, const char *reject);
    ").unwrap();
    
    fs::write(include_dir.join("ctype.h"), "#pragma once\nint isalpha(int c); int isdigit(int c);\n").unwrap();
    
    fs::create_dir_all(include_dir.join("sys")).unwrap();
    fs::write(include_dir.join("sys/time.h"), "#pragma once\nstruct timeval { long tv_sec; long tv_usec; }; int gettimeofday(struct timeval *tv, void *tz);\n").unwrap();
    fs::write(include_dir.join("strings.h"), "#pragma once\n").unwrap();
    fs::write(include_dir.join("time.h"), "#pragma once\nstruct timespec { long tv_sec; long tv_nsec; }; int nanosleep(const struct timespec *req, struct timespec *rem);\n").unwrap();

    cc::Build::new()
        .file("easec.c")
        .include(&include_dir)
        .flag("-ffreestanding")
        .flag("-fno-stack-protector")
        .flag("-mno-red-zone")
        .compile("easec");
        
    println!("cargo:rerun-if-changed=easec.c");
}