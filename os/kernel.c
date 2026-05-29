#include "freestanding.h"

// Define stream mocks
FILE* stderr = (FILE*)1;
FILE* stdin  = (FILE*)2;
FILE* stdout = (FILE*)3;

// Heap configuration
uint8_t kheap[2 * 1024 * 1024]; 
size_t heap_offset = 0;

void* kmalloc(size_t size) {
    size = (size + 3) & ~3;
    if (heap_offset + size > sizeof(kheap)) return NULL;
    void* ptr = &kheap[heap_offset];
    heap_offset += size;
    return ptr;
}

void* krealloc(void* ptr, size_t size) {
    if (!ptr) return kmalloc(size);
    void* new_ptr = kmalloc(size);
    if (new_ptr) memcpy(new_ptr, ptr, size);
    return new_ptr;
}

void* malloc(size_t size) { return kmalloc(size); }
void free(void* ptr) { (void)ptr; }
void* realloc(void* ptr, size_t size) { return krealloc(ptr, size); }

void* memset(void* dest, int val, size_t len) {
    uint8_t* p = dest;
    while (len--) *p++ = val;
    return dest;
}

void* memcpy(void* dest, const void* src, size_t len) {
    uint8_t* d = dest;
    const uint8_t* s = src;
    while (len--) *d++ = *s++;
    return dest;
}

int memcmp(const void* s1, const void* s2, size_t n) {
    const unsigned char* p1 = s1;
    const unsigned char* p2 = s2;
    while (n--) {
        if (*p1 != *p2) return *p1 - *p2;
        p1++;
        p2++;
    }
    return 0;
}

size_t strlen(const char* s) {
    size_t len = 0;
    while (s[len]) len++;
    return len;
}

int strcmp(const char* s1, const char* s2) {
    while (*s1 && (*s1 == *s2)) { s1++; s2++; }
    return *(unsigned char*)s1 - *(unsigned char*)s2;
}

int strncmp(const char* s1, const char* s2, size_t n) {
    while (n && *s1 && (*s1 == *s2)) { s1++; s2++; n--; }
    if (n == 0) return 0;
    return *(unsigned char*)s1 - *(unsigned char*)s2;
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
    while (n && (*d++ = *src++)) n--;
    while (n--) *d++ = '\0';
    return dest;
}

size_t strcspn(const char* s, const char* reject) {
    size_t count = 0;
    while (*s) {
        const char* r = reject;
        while (*r) {
            if (*s == *r) return count;
            r++;
        }
        s++;
        count++;
    }
    return count;
}

char* strchr(const char* s, int c) {
    while (*s) {
        if (*s == (char)c) return (char*)s;
        s++;
    }
    return NULL;
}

char* strrchr(const char* s, int c) {
    char* last = NULL;
    while (*s) {
        if (*s == (char)c) last = (char*)s;
        s++;
    }
    return last;
}

int isdigit(int c) { return c >= '0' && c <= '9'; }
int isalpha(int c) { return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_'; }

long long atoll(const char* s) {
    long long res = 0;
    int sign = 1;
    if (*s == '-') { sign = -1; s++; }
    while (*s >= '0' && *s <= '9') {
        res = res * 10 + (*s - '0');
        s++;
    }
    return res * sign;
}

double atof(const char* s) {
    double res = 0.0;
    double factor = 1.0;
    int dec_seen = 0;
    int sign = 1;
    if (*s == '-') { sign = -1; s++; }
    while (*s) {
        if (*s == '.') { dec_seen = 1; s++; continue; }
        if (*s < '0' || *s > '9') break;
        if (dec_seen) factor *= 0.1;
        res = res * 10.0 + (*s - '0');
        s++;
    }
    return res * factor * sign;
}

/* -------------------------------------------------------------
   Standard Formatting and Stream Function Mocks
   ------------------------------------------------------------- */

FILE* fopen(const char* filename, const char* mode) {
    (void)filename; (void)mode;
    return NULL;
}

int fclose(FILE* stream) {
    (void)stream;
    return 0;
}

size_t fread(void* ptr, size_t size, size_t nmemb, FILE* stream) {
    (void)ptr; (void)size; (void)nmemb; (void)stream;
    return 0;
}

size_t fwrite(const void* ptr, size_t size, size_t nmemb, FILE* stream) {
    (void)ptr; (void)size; (void)nmemb; (void)stream;
    return 0;
}

int fseek(FILE* stream, long offset, int whence) {
    (void)stream; (void)offset; (void)whence;
    return 0;
}

long ftell(FILE* stream) {
    (void)stream;
    return 0;
}

int remove(const char* filename) {
    (void)filename;
    return 0;
}

/* -------------------------------------------------------------
   VGA Output and Formatting Drivers
   ------------------------------------------------------------- */

#define VGA_WIDTH 80
#define VGA_HEIGHT 25
uint16_t* const vga_buffer = (uint16_t*)0xB8000;
int cursor_x = 0;
int cursor_y = 0;

void clear_screen() {
    for (int y = 0; y < VGA_HEIGHT; y++) {
        for (int x = 0; x < VGA_WIDTH; x++) {
            vga_buffer[y * VGA_WIDTH + x] = (uint16_t)' ' | (0x0F << 8);
        }
    }
    cursor_x = 0;
    cursor_y = 0;
}

void print_char(char c) {
    if (c == '\n') {
        cursor_x = 0;
        cursor_y++;
    } else {
        vga_buffer[cursor_y * VGA_WIDTH + cursor_x] = (uint16_t)c | (0x0F << 8);
        cursor_x++;
    }
    if (cursor_x >= VGA_WIDTH) {
        cursor_x = 0;
        cursor_y++;
    }
    if (cursor_y >= VGA_HEIGHT) {
        for (int y = 1; y < VGA_HEIGHT; y++) {
            memcpy(vga_buffer + (y - 1) * VGA_WIDTH, vga_buffer + y * VGA_WIDTH, VGA_WIDTH * 2);
        }
        for (int x = 0; x < VGA_WIDTH; x++) {
            vga_buffer[(VGA_HEIGHT - 1) * VGA_WIDTH + x] = (uint16_t)' ' | (0x0F << 8);
        }
        cursor_y = VGA_HEIGHT - 1;
    }
}

void print_string(const char* str) {
    while (*str) print_char(*str++);
}

static void itoa(long long value, char* str, int base) {
    char temp[32];
    int i = 0;
    int is_negative = 0;
    if (value < 0 && base == 10) {
        is_negative = 1;
        value = -value;
    }
    do {
        int rem = value % base;
        temp[i++] = (rem > 9) ? (rem - 10) + 'a' : rem + '0';
        value /= base;
    } while (value > 0);
    if (is_negative) temp[i++] = '-';
    int len = i;
    for (int j = 0; j < len; j++) str[j] = temp[len - j - 1];
    str[len] = '\0';
}

int vsnprintf(char* str, size_t size, const char* format, va_list args) {
    size_t written = 0;
    while (*format && written < size - 1) {
        if (*format == '%') {
            format++;
            if (*format == 's') {
                char* val = va_arg(args, char*);
                while (*val && written < size - 1) str[written++] = *val++;
            } else if (*format == 'd' || *format == 'g') {
                int val = va_arg(args, int);
                char num_buf[32];
                itoa(val, num_buf, 10);
                char* n = num_buf;
                while (*n && written < size - 1) str[written++] = *n++;
            } else if (*format == 'l') {
                format++;
                if (*format == 'l') format++;
                if (*format == 'd') {
                    long long val = va_arg(args, long long);
                    char num_buf[64];
                    itoa(val, num_buf, 10);
                    char* n = num_buf;
                    while (*n && written < size - 1) str[written++] = *n++;
                }
            } else {
                str[written++] = '%';
                if (written < size - 1) str[written++] = *format;
            }
        } else {
            str[written++] = *format;
        }
        format++;
    }
    str[written] = '\0';
    return written;
}

int snprintf(char* str, size_t size, const char* format, ...) {
    va_list args;
    va_start(args, format);
    int ret = vsnprintf(str, size, format, args);
    va_end(args);
    return ret;
}

int sprintf(char* str, const char* format, ...) {
    va_list args;
    va_start(args, format);
    int ret = vsnprintf(str, 4096, format, args);
    va_end(args);
    return ret;
}

int printf(const char* format, ...) {
    char buf[1024];
    va_list args;
    va_start(args, format);
    int ret = vsnprintf(buf, sizeof(buf), format, args);
    va_end(args);
    print_string(buf);
    return ret;
}

int fprintf(FILE* stream, const char* format, ...) {
    (void)stream;
    char buf[1024];
    va_list args;
    va_start(args, format);
    int ret = vsnprintf(buf, sizeof(buf), format, args);
    va_end(args);
    print_string(buf);
    return ret;
}

int vfprintf(FILE* stream, const char* format, va_list args) {
    (void)stream;
    char buf[1024];
    int ret = vsnprintf(buf, sizeof(buf), format, args);
    print_string(buf);
    return ret;
}

int fputs(const char* str, FILE* stream) {
    (void)stream;
    print_string(str);
    return 0;
}

/* -------------------------------------------------------------
   Keyboard and System Control Register Interfaces
   ------------------------------------------------------------- */

uint8_t inb(uint16_t port) {
    uint8_t ret;
    __asm__ volatile("inb %1, %0" : "=a"(ret) : "Nd"(port));
    return ret;
}

void outb(uint16_t port, uint8_t val) {
    __asm__ volatile("outb %0, %1" : : "a"(val), "Nd"(port));
}

void outw(uint16_t port, uint16_t val) {
    __asm__ volatile("outw %0, %1" : : "a"(val), "Nd"(port));
}

char keyboard_get_char() {
    static const char scan_to_ascii[] = {
        0, 0, '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', 0,
        0, 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\n',
        0, 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'', '`', 0,
        '\\', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/', 0, '*', 0, ' '
    };
    while (1) {
        if (inb(0x64) & 1) {
            uint8_t scancode = inb(0x60);
            if (scancode < sizeof(scan_to_ascii) && scan_to_ascii[scancode] != 0) {
                return scan_to_ascii[scancode];
            }
        }
    }
}

char* fgets_freestanding(char* str, int num) {
    int i = 0;
    while (i < num - 1) {
        char c = keyboard_get_char();
        if (c == '\n') {
            print_char('\n');
            str[i++] = '\0';
            break;
        } else {
            print_char(c);
            str[i++] = c;
        }
    }
    return str;
}

char* fgets(char* str, int num, FILE* stream) {
    (void)stream;
    return fgets_freestanding(str, num);
}

void sys_reboot() { outb(0x64, 0xFE); }
void sys_shutdown() { outw(0x604, 0x2000); } // 16-bit write resolves warning

/* -------------------------------------------------------------
   AHCI SATA Native Storage Driver Implementation
   ------------------------------------------------------------- */

#define SATA_SIG_ATA    0x00000101
#define AHCI_DEV_BUSY   0x80
#define AHCI_DEV_DRQ    0x08

typedef struct tagHBAPort {
    uint32_t clb;
    uint32_t clbu;
    uint32_t fb;
    uint32_t fbu;
    uint32_t is;
    uint32_t ie;
    uint32_t cmd;
    uint32_t rsvd0;
    uint32_t tfd;
    uint32_t sig;
    uint32_t ssts;
    uint32_t sctl;
    uint32_t serr;
    uint32_t sact;
    uint32_t ci;
    uint32_t sntf;
    uint32_t fbs;
    uint32_t rsvd1[11];
    uint32_t vendor[4];
} HBAPort;

typedef struct tagHBAMem {
    uint32_t cap;
    uint32_t ghc;
    uint32_t is;
    uint32_t pi;
    uint32_t vs;
    uint32_t ccc_ctl;
    uint32_t ccc_pts;
    uint32_t em_loc;
    uint32_t em_ctl;
    uint32_t cap2;
    uint32_t bohc;
    uint8_t  rsvd[116];
    uint8_t  vendor[96];
    HBAPort  ports[32];
} HBAMem;

typedef struct tagHBAPRDTEntry {
    uint32_t dba;
    uint32_t dbau;
    uint32_t rsvd0;
    uint32_t dbc:22;
    uint32_t rsvd1:9;
    uint32_t i:1;
} HBAPRDTEntry;

typedef struct tagHBACommandTable {
    uint8_t cfis[64];
    uint8_t acmd[16];
    uint8_t rsvd[48];
    HBAPRDTEntry prdt_entry[1];
} HBACommandTable;

typedef struct tagHBACommandHeader {
    uint8_t cfl:5;
    uint8_t a:1;
    uint8_t w:1;
    uint8_t p:1;
    uint8_t r:1;
    uint8_t b:1;
    uint8_t c:1;
    uint8_t rsvd0:1;
    uint8_t pmp:4;
    uint16_t prdtl;
    uint32_t prdbc;
    uint32_t ctba;
    uint32_t ctbau;
    uint32_t rsvd1[4];
} HBACommandHeader;

int ahci_read(HBAPort *port, uint32_t startl, uint32_t starth, uint32_t count, uint16_t *buf) {
    port->is = (uint32_t)-1;
    int spin = 0;
    int slot = 0;

    HBACommandHeader *cmdheader = (HBACommandHeader*)(uintptr_t)(port->clb);
    cmdheader[slot].cfl = 5;
    cmdheader[slot].w = 0; 
    cmdheader[slot].prdtl = 1;

    HBACommandTable *cmdtable = (HBACommandTable*)(uintptr_t)(cmdheader[slot].ctba);
    memset(cmdtable, 0, sizeof(HBACommandTable));

    cmdtable->prdt_entry[0].dba = (uint32_t)(uintptr_t)buf;
    cmdtable->prdt_entry[0].dbc = (count << 9) - 1;
    cmdtable->prdt_entry[0].i = 1;

    uint8_t *cmdfis = cmdtable->cfis;
    cmdfis[0] = 0x27; 
    cmdfis[1] = 1 << 7;
    cmdfis[2] = 0x25; 

    cmdfis[4] = (uint8_t)startl;
    cmdfis[5] = (uint8_t)(startl >> 8);
    cmdfis[6] = (uint8_t)(startl >> 16);
    cmdfis[7] = 1 << 6;

    cmdfis[8] = (uint8_t)(startl >> 24);
    cmdfis[9] = (uint8_t)starth;
    cmdfis[10] = (uint8_t)(starth >> 8);

    cmdfis[12] = count & 0xFF;
    cmdfis[13] = (count >> 8) & 0xFF;

    while ((port->tfd & (AHCI_DEV_BUSY | AHCI_DEV_DRQ)) && spin < 1000000) {
        spin++;
    }
    if (spin == 1000000) return 0;

    port->ci = 1 << slot;

    while (1) {
        if ((port->ci & (1 << slot)) == 0) break;
        if (port->is & (1 << 30)) return 0;
    }
    return 1;
}

int ahci_write(HBAPort *port, uint32_t startl, uint32_t starth, uint32_t count, uint16_t *buf) {
    port->is = (uint32_t)-1;
    int spin = 0;
    int slot = 0;

    HBACommandHeader *cmdheader = (HBACommandHeader*)(uintptr_t)(port->clb);
    cmdheader[slot].cfl = 5;
    cmdheader[slot].w = 1; 
    cmdheader[slot].prdtl = 1;

    HBACommandTable *cmdtable = (HBACommandTable*)(uintptr_t)(cmdheader[slot].ctba);
    memset(cmdtable, 0, sizeof(HBACommandTable));

    cmdtable->prdt_entry[0].dba = (uint32_t)(uintptr_t)buf;
    cmdtable->prdt_entry[0].dbc = (count << 9) - 1;
    cmdtable->prdt_entry[0].i = 1;

    uint8_t *cmdfis = cmdtable->cfis;
    cmdfis[0] = 0x27;
    cmdfis[1] = 1 << 7;
    cmdfis[2] = 0x35; 

    cmdfis[4] = (uint8_t)startl;
    cmdfis[5] = (uint8_t)(startl >> 8);
    cmdfis[6] = (uint8_t)(startl >> 16);
    cmdfis[7] = 1 << 6;

    cmdfis[8] = (uint8_t)(startl >> 24);
    cmdfis[9] = (uint8_t)starth;
    cmdfis[10] = (uint8_t)(starth >> 8);

    cmdfis[12] = count & 0xFF;
    cmdfis[13] = (count >> 8) & 0xFF;

    while ((port->tfd & (AHCI_DEV_BUSY | AHCI_DEV_DRQ)) && spin < 1000000) {
        spin++;
    }
    if (spin == 1000000) return 0;

    port->ci = 1 << slot;

    while (1) {
        if ((port->ci & (1 << slot)) == 0) break;
        if (port->is & (1 << 30)) return 0;
    }
    return 1;
}

/* -------------------------------------------------------------
   Timing Fallback Utilities
   ------------------------------------------------------------- */

long long get_time_ms() {
    static long long mock_time = 0;
    return mock_time++;
}

void sleep_ms(long long ms) {
    for (volatile long long i = 0; i < ms * 10000; i++);
}

void exit(int status) {
    (void)status;
    print_string("\nKernel exited. System Halted.\n");
    while (1) {
        __asm__ volatile("cli; hlt");
    }
}

/* -------------------------------------------------------------
   Main Shell Entry
   ------------------------------------------------------------- */

void kernel_main(uint32_t magic, uint32_t addr) {
    (void)magic; (void)addr;
    clear_screen();
    print_string("=========================================\n");
    print_string("          Welcome to inpsos              \n");
    print_string("=========================================\n");

    while (1) {
        print_string("inpsos> ");
        char command_buf[64];
        fgets_freestanding(command_buf, sizeof(command_buf));

        if (strcmp(command_buf, "clear") == 0) {
            clear_screen();
        } else if (strcmp(command_buf, "restart") == 0) {
            sys_reboot();
        } else if (strcmp(command_buf, "shutdown") == 0) {
            sys_shutdown();
        } else {
            print_string("Executing requested easec module...\n");
        }
    }
}