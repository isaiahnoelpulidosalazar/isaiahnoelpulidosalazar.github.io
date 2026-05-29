#include "freestanding.h"

/* -------------------------------------------------------------
   Hardened Memory Allocation (Tracks Block Sizes)
   ------------------------------------------------------------- */

FILE* stderr = (FILE*)1;
FILE* stdin  = (FILE*)2;
FILE* stdout = (FILE*)3;

// Expanded to 16MB to ensure the Easec compiler has plenty of breathing room
uint8_t kheap[16 * 1024 * 1024]; 
size_t heap_offset = 0;

void* kmalloc(size_t size) {
    size = (size + 3) & ~3; // 4-byte alignment
    // We add sizeof(size_t) to securely store the size of the allocation in a header
    if (heap_offset + sizeof(size_t) + size > sizeof(kheap)) return NULL;
    
    size_t* header = (size_t*)&kheap[heap_offset];
    *header = size; 
    heap_offset += sizeof(size_t) + size;
    
    return (void*)(header + 1); // Return memory just after the size header
}

void* krealloc(void* ptr, size_t new_size) {
    if (!ptr) return kmalloc(new_size);
    if (new_size == 0) return NULL; // No-op for free

    size_t* header = (size_t*)ptr - 1;
    size_t old_size = *header;

    void* new_ptr = kmalloc(new_size);
    if (new_ptr) {
        // Prevent copying out-of-bounds by taking the minimum of old/new size
        memcpy(new_ptr, ptr, old_size < new_size ? old_size : new_size);
    }
    return new_ptr;
}

void* malloc(size_t size) { return kmalloc(size); }
void free(void* ptr) { (void)ptr; } // Bump allocator ignores free
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

FILE* fopen(const char* filename, const char* mode) { (void)filename; (void)mode; return NULL; }
int fclose(FILE* stream) { (void)stream; return 0; }
size_t fread(void* ptr, size_t size, size_t nmemb, FILE* stream) { (void)ptr; (void)size; (void)nmemb; (void)stream; return 0; }
size_t fwrite(const void* ptr, size_t size, size_t nmemb, FILE* stream) { (void)ptr; (void)size; (void)nmemb; (void)stream; return 0; }
int fseek(FILE* stream, long offset, int whence) { (void)stream; (void)offset; (void)whence; return 0; }
long ftell(FILE* stream) { (void)stream; return 0; }
int remove(const char* filename) { (void)filename; return 0; }

/* -------------------------------------------------------------
   VGA Output and Display Formatting Drivers
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
    } else if (c == '\b') {
        if (cursor_x > 0) cursor_x--;
        else if (cursor_y > 0) {
            cursor_y--;
            cursor_x = VGA_WIDTH - 1;
        }
        vga_buffer[cursor_y * VGA_WIDTH + cursor_x] = (uint16_t)' ' | (0x0F << 8);
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
    char temp[32]; int i = 0; int is_negative = 0;
    if (value < 0 && base == 10) { is_negative = 1; value = -value; }
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

/* Prevent variadic stack corruption by accurately typing bytes based on the format flag */
int vsnprintf(char* str, size_t size, const char* format, va_list args) {
    size_t written = 0;
    while (*format && written < size - 1) {
        if (*format == '%') {
            format++;
            if (*format == 's') {
                char* val = va_arg(args, char*);
                if (!val) val = "(null)";
                while (*val && written < size - 1) str[written++] = *val++;
            } else if (*format == 'd' || *format == 'i') {
                int val = va_arg(args, int); 
                char num_buf[32]; itoa(val, num_buf, 10);
                char* n = num_buf; while (*n && written < size - 1) str[written++] = *n++;
            } else if (*format == 'g' || *format == 'f') {
                // VERY IMPORTANT: Consume 8 bytes for double to prevent CPU stack corruption
                double val = va_arg(args, double);
                long long int_part = (long long)val;
                char num_buf[64]; itoa(int_part, num_buf, 10);
                char* n = num_buf; while (*n && written < size - 1) str[written++] = *n++;
            } else if (*format == 'l') {
                format++; if (*format == 'l') format++;
                if (*format == 'd') {
                    // Consume 8 bytes for long long
                    long long val = va_arg(args, long long); 
                    char num_buf[64]; itoa(val, num_buf, 10);
                    char* n = num_buf; while (*n && written < size - 1) str[written++] = *n++;
                }
            } else { str[written++] = '%'; if (written < size - 1) str[written++] = *format; }
        } else { str[written++] = *format; }
        format++;
    }
    str[written] = '\0'; return written;
}

int snprintf(char* str, size_t size, const char* format, ...) { va_list args; va_start(args, format); int ret = vsnprintf(str, size, format, args); va_end(args); return ret; }
int sprintf(char* str, const char* format, ...) { va_list args; va_start(args, format); int ret = vsnprintf(str, 4096, format, args); va_end(args); return ret; }
int printf(const char* format, ...) { char buf[1024]; va_list args; va_start(args, format); int ret = vsnprintf(buf, sizeof(buf), format, args); va_end(args); print_string(buf); return ret; }
int fprintf(FILE* stream, const char* format, ...) { (void)stream; char buf[1024]; va_list args; va_start(args, format); int ret = vsnprintf(buf, sizeof(buf), format, args); va_end(args); print_string(buf); return ret; }
int vfprintf(FILE* stream, const char* format, va_list args) { (void)stream; char buf[1024]; int ret = vsnprintf(buf, sizeof(buf), format, args); print_string(buf); return ret; }
int fputs(const char* str, FILE* stream) { (void)stream; print_string(str); return 0; }

/* -------------------------------------------------------------
   Hardware Port Communications
   ------------------------------------------------------------- */

uint8_t inb(uint16_t port) { uint8_t ret; __asm__ volatile("inb %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }
void outb(uint16_t port, uint8_t val) { __asm__ volatile("outb %0, %1" : : "a"(val), "Nd"(port)); }
void outw(uint16_t port, uint16_t val) { __asm__ volatile("outw %0, %1" : : "a"(val), "Nd"(port)); }
uint32_t inl(uint16_t port) { uint32_t ret; __asm__ volatile("inl %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }
void outl(uint16_t port, uint32_t val) { __asm__ volatile("outl %0, %1" : : "a"(val), "Nd"(port)); }

/* -------------------------------------------------------------
   Keyboard System Configuration
   ------------------------------------------------------------- */

char keyboard_get_char() {
    static const char scan_to_ascii[] = {
        0, 0, '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', '\b',
        '\t', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\n',
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
            break;
        } else if (c == '\b') {
            if (i > 0) {
                i--;
                print_char('\b');
            }
        } else if (c != 0) {
            print_char(c);
            str[i++] = c;
        }
    }
    str[i] = '\0'; // Guaranteed null termination
    return str;
}

char* fgets(char* str, int num, FILE* stream) { (void)stream; return fgets_freestanding(str, num); }

void sys_reboot() { outb(0x64, 0xFE); }
void sys_shutdown() { outw(0x604, 0x2000); }

/* -------------------------------------------------------------
   PCI AHCI Interface System & Storage Definitions
   ------------------------------------------------------------- */

#define SATA_SIG_ATA    0x00000101
#define AHCI_DEV_BUSY   0x80
#define AHCI_DEV_DRQ    0x08

typedef struct tagHBAPort {
    uint32_t clb;  uint32_t clbu; uint32_t fb;   uint32_t fbu;
    uint32_t is;   uint32_t ie;   uint32_t cmd;  uint32_t rsvd0;
    uint32_t tfd;  uint32_t sig;  uint32_t ssts; uint32_t sctl;
    uint32_t serr; uint32_t sact; uint32_t ci;   uint32_t sntf;
    uint32_t fbs;  uint32_t rsvd1[11]; uint32_t vendor[4];
} HBAPort;

typedef struct tagHBAMem {
    uint32_t cap; uint32_t ghc; uint32_t is; uint32_t pi; uint32_t vs;
    uint32_t ccc_ctl; uint32_t ccc_pts; uint32_t em_loc; uint32_t em_ctl;
    uint32_t cap2; uint32_t bohc; uint8_t rsvd[116]; uint8_t vendor[96];
    HBAPort ports[32];
} HBAMem;

typedef struct tagHBAPRDTEntry {
    uint32_t dba; uint32_t dbau; uint32_t rsvd0;
    uint32_t dbc:22; uint32_t rsvd1:9; uint32_t i:1;
} HBAPRDTEntry;

typedef struct tagHBACommandTable {
    uint8_t cfis[64]; uint8_t acmd[16]; uint8_t rsvd[48];
    HBAPRDTEntry prdt_entry[1];
} HBACommandTable;

typedef struct tagHBACommandHeader {
    uint8_t cfl:5; uint8_t a:1; uint8_t w:1; uint8_t p:1; uint8_t r:1; uint8_t b:1;
    uint8_t c:1; uint8_t rsvd0:1; uint8_t pmp:4; uint16_t prdtl; uint32_t prdbc;
    uint32_t ctba; uint32_t ctbau; uint32_t rsvd1[4];
} HBACommandHeader;

int ahci_read(HBAPort *port, uint32_t startl, uint32_t starth, uint32_t count, uint16_t *buf) {
    port->is = (uint32_t)-1; int spin = 0; int slot = 0;
    HBACommandHeader *cmdheader = (HBACommandHeader*)(uintptr_t)(port->clb);
    cmdheader[slot].cfl = 5; cmdheader[slot].w = 0; cmdheader[slot].prdtl = 1;

    HBACommandTable *cmdtable = (HBACommandTable*)(uintptr_t)(cmdheader[slot].ctba);
    memset(cmdtable, 0, sizeof(HBACommandTable));
    cmdtable->prdt_entry[0].dba = (uint32_t)(uintptr_t)buf;
    cmdtable->prdt_entry[0].dbc = (count << 9) - 1; cmdtable->prdt_entry[0].i = 1;

    uint8_t *cmdfis = cmdtable->cfis;
    cmdfis[0] = 0x27; cmdfis[1] = 1 << 7; cmdfis[2] = 0x25; 
    cmdfis[4] = (uint8_t)startl; cmdfis[5] = (uint8_t)(startl >> 8);
    cmdfis[6] = (uint8_t)(startl >> 16); cmdfis[7] = 1 << 6;
    cmdfis[8] = (uint8_t)(startl >> 24); cmdfis[9] = (uint8_t)starth;
    cmdfis[10] = (uint8_t)(starth >> 8);
    cmdfis[12] = count & 0xFF; cmdfis[13] = (count >> 8) & 0xFF;

    while ((port->tfd & (AHCI_DEV_BUSY | AHCI_DEV_DRQ)) && spin < 1000000) spin++;
    if (spin == 1000000) return 0;
    port->ci = 1 << slot;

    int wait = 0;
    while (1) {
        if ((port->ci & (1 << slot)) == 0) break;
        if (port->is & (1 << 30)) return 0;
        if (wait++ > 5000000) return 0;
    }
    return 1;
}

int ahci_write(HBAPort *port, uint32_t startl, uint32_t starth, uint32_t count, uint16_t *buf) {
    port->is = (uint32_t)-1; int spin = 0; int slot = 0;
    HBACommandHeader *cmdheader = (HBACommandHeader*)(uintptr_t)(port->clb);
    cmdheader[slot].cfl = 5; cmdheader[slot].w = 1; cmdheader[slot].prdtl = 1;

    HBACommandTable *cmdtable = (HBACommandTable*)(uintptr_t)(cmdheader[slot].ctba);
    memset(cmdtable, 0, sizeof(HBACommandTable));
    cmdtable->prdt_entry[0].dba = (uint32_t)(uintptr_t)buf;
    cmdtable->prdt_entry[0].dbc = (count << 9) - 1; cmdtable->prdt_entry[0].i = 1;

    uint8_t *cmdfis = cmdtable->cfis;
    cmdfis[0] = 0x27; cmdfis[1] = 1 << 7; cmdfis[2] = 0x35; 
    cmdfis[4] = (uint8_t)startl; cmdfis[5] = (uint8_t)(startl >> 8);
    cmdfis[6] = (uint8_t)(startl >> 16); cmdfis[7] = 1 << 6;
    cmdfis[8] = (uint8_t)(startl >> 24); cmdfis[9] = (uint8_t)starth;
    cmdfis[10] = (uint8_t)(starth >> 8);
    cmdfis[12] = count & 0xFF; cmdfis[13] = (count >> 8) & 0xFF;

    while ((port->tfd & (AHCI_DEV_BUSY | AHCI_DEV_DRQ)) && spin < 1000000) spin++;
    if (spin == 1000000) return 0;

    port->ci = 1 << slot;

    int wait = 0;
    while (1) {
        if ((port->ci & (1 << slot)) == 0) break;
        if (port->is & (1 << 30)) return 0;
        if (wait++ > 5000000) return 0;
    }
    return 1;
}

/* -------------------------------------------------------------
   PCI Bus Scanner for Real Hardware Target Configurations
   ------------------------------------------------------------- */

uint32_t pci_config_read(uint8_t bus, uint8_t slot, uint8_t func, uint8_t offset) {
    uint32_t address = (uint32_t)((bus << 16) | (slot << 11) | (func << 8) | (offset & 0xfc) | ((uint32_t)0x80000000));
    outl(0xCF8, address);
    return inl(0xCFC);
}

void pci_config_write(uint8_t bus, uint8_t slot, uint8_t func, uint8_t offset, uint32_t value) {
    uint32_t address = (uint32_t)((bus << 16) | (slot << 11) | (func << 8) | (offset & 0xfc) | ((uint32_t)0x80000000));
    outl(0xCF8, address);
    outl(0xCFC, value);
}

void* get_ahci_base() {
    for (uint16_t bus = 0; bus < 256; bus++) {
        for (uint8_t slot = 0; slot < 32; slot++) {
            uint32_t vendor = pci_config_read(bus, slot, 0, 0);
            if (vendor == 0xFFFFFFFF) continue;
            
            uint32_t class_sub = pci_config_read(bus, slot, 0, 0x08);
            uint8_t class_code = (class_sub >> 24) & 0xFF;
            uint8_t subclass = (class_sub >> 16) & 0xFF;
            uint8_t prog_if = (class_sub >> 8) & 0xFF;
            
            if (class_code == 0x01 && subclass == 0x06 && prog_if == 0x01) {
                uint32_t bar5 = pci_config_read(bus, slot, 0, 0x24);
                uint32_t ahci_address = bar5 & 0xFFFFFFF0;
                if (ahci_address == 0) {
                    pci_config_write(bus, slot, 0, 0x24, 0xFEB00000);
                    ahci_address = 0xFEB00000;
                }

                uint32_t cmd = pci_config_read(bus, slot, 0, 0x04);
                pci_config_write(bus, slot, 0, 0x04, cmd | 0x02 | 0x04); // Enable Memory Space & Bus Master
                return (void*)ahci_address;
            }
        }
    }
    return NULL;
}

HBAPort* active_port = NULL;

void find_ahci_device() {
    print_string("Probing PCI Bus for AHCI storage controllers...\n");
    HBAMem* hba_mem = (HBAMem*)get_ahci_base(); 
    if (hba_mem) {
        uint32_t pi = hba_mem->pi;
        for (int i = 0; i < 32; i++) {
            if (pi & (1 << i)) {
                HBAPort* port = &hba_mem->ports[i];
                if ((port->ssts & 0x0F) == 3 && port->sig == SATA_SIG_ATA) {
                    active_port = port;
                    
                    port->cmd &= ~(1 << 0);
                    port->cmd &= ~(1 << 4);
                    
                    int spin = 0;
                    while ((port->cmd & (1 << 14)) && spin++ < 1000000);
                    spin = 0;
                    while ((port->cmd & (1 << 15)) && spin++ < 1000000);
                    
                    void* clb_mem = malloc(2048);
                    uintptr_t clb_align = (((uintptr_t)clb_mem + 1023) & ~1023);
                    port->clb = (uint32_t)clb_align; port->clbu = 0;
                    memset((void*)clb_align, 0, 1024);
                    
                    void* fb_mem = malloc(512);
                    uintptr_t fb_align = (((uintptr_t)fb_mem + 255) & ~255);
                    port->fb = (uint32_t)fb_align; port->fbu = 0;
                    memset((void*)fb_align, 0, 256);
                    
                    HBACommandHeader* cmdheader = (HBACommandHeader*)clb_align;
                    for (int j = 0; j < 32; j++) {
                        cmdheader[j].prdtl = 8;
                        void* ctba_mem = malloc(512);
                        uintptr_t ctba_align = (((uintptr_t)ctba_mem + 127) & ~127);
                        cmdheader[j].ctba = (uint32_t)ctba_align; cmdheader[j].ctbau = 0;
                        memset((void*)ctba_align, 0, 256);
                    }
                    port->cmd |= (1 << 4);
                    port->cmd |= (1 << 0);
                    print_string("Storage device safely mapped.\n");
                    return;
                }
            }
        }
    }
    print_string("No active SATA interface detected. Continuing without disk.\n");
    active_port = NULL;
}

/* -------------------------------------------------------------
   Timing Fallback Utilities (DO NOT REMOVE)
   ------------------------------------------------------------- */

long long get_time_ms() { static long long mock_time = 0; return mock_time++; }
void sleep_ms(long long ms) { for (volatile long long i = 0; i < ms * 10000; i++); }
void exit(int status) { (void)status; print_string("\nKernel exited. System Halted.\n"); while (1) { __asm__ volatile("cli; hlt"); } }

/* -------------------------------------------------------------
   Easec VM Linkage Definitions
   ------------------------------------------------------------- */

typedef enum { VAL_NULL, VAL_BOOL, VAL_INT, VAL_FLOAT, VAL_OBJ } ValType;

typedef struct {
    ValType type;
    union { int boolean; long long integer; double floating; void* obj; } as;
} Value;

extern void init_vm();
extern void* create_env(void* parent);
extern void run_script(const char* source, void* env);
extern void env_define(void* env, const char* name, Value val);
extern void* allocate_string(const char* chars, int length);

static Value make_obj_val(void* o) {
    Value v; v.type = VAL_OBJ; v.as.obj = o; return v;
}

/* -------------------------------------------------------------
   Flat Filesystem Structure definitions
   ------------------------------------------------------------- */

#define MAX_FILES 12

typedef struct {
    char filename[32];
    uint32_t start_lba;
    uint32_t file_size;
} FileEntry;

typedef struct {
    FileEntry entries[MAX_FILES];
    uint8_t padding[32];
} DirectoryTable;

int check_installation_state(HBAPort* port) {
    char sector_buffer[512];
    memset(sector_buffer, 0, 512);
    if (ahci_read(port, 1, 0, 1, (uint16_t*)sector_buffer)) {
        if (memcmp(sector_buffer, "INPSOS_INSTALLED", 16) == 0) return 1; 
    }
    return 0; 
}

/* -------------------------------------------------------------
   Dynamic Hard Drive Scanning Execution (run_easec)
   ------------------------------------------------------------- */

void run_easec(const char* filename) {
    if (!active_port) { print_string("Error: Active AHCI port missing.\n"); return; }

    DirectoryTable dir_table; memset(&dir_table, 0, sizeof(DirectoryTable));
    if (!ahci_read(active_port, 2, 0, 1, (uint16_t*)&dir_table)) { print_string("Error: Failed to fetch storage directory.\n"); return; }

    FileEntry* target_entry = NULL;
    for (int i = 0; i < MAX_FILES; i++) {
        if (strcmp(dir_table.entries[i].filename, filename) == 0) {
            target_entry = &dir_table.entries[i]; break;
        }
    }

    if (!target_entry) { printf("Error: Module script '%s' not found.\n", filename); return; }

    uint32_t sectors_to_read = (target_entry->file_size + 511) / 512;
    char* file_content = (char*)malloc(sectors_to_read * 512 + 1);
    if (!file_content) { print_string("Error: Allocation failure.\n"); return; }

    if (!ahci_read(active_port, target_entry->start_lba, 0, sectors_to_read, (uint16_t*)file_content)) {
        print_string("Error: Read aborted mid-transmission.\n"); free(file_content); return;
    }

    file_content[target_entry->file_size] = '\0';

    init_vm();
    void* global_env = create_env(NULL);
    void* list_str = allocate_string("Dynamic modules detected on SATA disk:\n  list.easec\n  pattern.easec\n  game.easec\n", 200);
    env_define(global_env, "sys_list_dir", make_obj_val(list_str));

    run_script(file_content, global_env);
}

void run_install() {
    print_string("Initializing physical installation onto hard disk...\n");
    if (!active_port) { print_string("Error: Compatible AHCI SATA controller not detected.\n"); return; }
    
    char write_buffer[512]; memset(write_buffer, 0, 512); memcpy(write_buffer, "INPSOS_INSTALLED", 16);
    if (!ahci_write(active_port, 1, 0, 1, (uint16_t*)write_buffer)) { print_string("Error: Local layout boot sector write failure.\n"); return; }

    DirectoryTable dir_table; memset(&dir_table, 0, sizeof(DirectoryTable));

    strcpy(dir_table.entries[0].filename, "list"); dir_table.entries[0].start_lba = 3; dir_table.entries[0].file_size = 230;
    strcpy(dir_table.entries[1].filename, "pattern"); dir_table.entries[1].start_lba = 4; dir_table.entries[1].file_size = 110;
    strcpy(dir_table.entries[2].filename, "game"); dir_table.entries[2].start_lba = 5; dir_table.entries[2].file_size = 250;

    if (!ahci_write(active_port, 2, 0, 1, (uint16_t*)&dir_table)) { print_string("Error: Directory Table write aborted.\n"); return; }

    char file_buffer[512];
    memset(file_buffer, 0, 512);
    strcpy(file_buffer, "say \"=== inpsos SATA Storage Explorer ===\"\nsay \"Reading file allocation sectors on drive Port 0...\"\nsay \"Local Easec script files:\"\nsay \"  - list\"\nsay \"  - pattern\"\nsay \"  - game\"\n");
    ahci_write(active_port, 3, 0, 1, (uint16_t*)file_buffer);

    memset(file_buffer, 0, 512);
    strcpy(file_buffer, "say \"=== Asterisk Pattern Loop ===\"\nvar line \"*\"\nrepeat 5 [\nsay line\nline = line + \"*\"\n]\n");
    ahci_write(active_port, 4, 0, 1, (uint16_t*)file_buffer);

    memset(file_buffer, 0, 512);
    strcpy(file_buffer, "say \"=== Cave Adventure ===\"\nsay \"You find yourself inside a dark, humid cave. Left or Right?\"\nvar path get\nif path == \"left\" [\nsay \"You discovered a cache of physical gold bullion. You win!\"\n] else [\nsay \"A modular partition collapsed on you. Game over.\"\n]\n");
    ahci_write(active_port, 5, 0, 1, (uint16_t*)file_buffer);

    print_string("INPSOS installation onto SATA partitions completed.\n");
    print_string("Please detach your installation media and reboot computer.\n");
}

/* -------------------------------------------------------------
   Main Shell Entry
   ------------------------------------------------------------- */

void kernel_main(uint32_t magic, uint32_t addr) {
    (void)magic; (void)addr;
    __asm__ volatile("cli"); // Ensure interrupts are fully locked to prevent triple faults

    clear_screen();
    print_string("=========================================\n");
    print_string("          Welcome to inpsos              \n");
    print_string("=========================================\n");

    find_ahci_device();
    
    int installed = 0;
    if (active_port) installed = check_installation_state(active_port);

    if (!installed) {
        print_string("STATUS: Running from Bootable Live ISO.\n");
        print_string("Warning: All operating system features are locked.\n");
        print_string("Please run command 'install' to setup onto local hardware.\n\n");
    } else {
        print_string("STATUS: Booted from Physical Drive.\n");
        print_string("All system operations are unlocked.\n\n");
    }

    while (1) {
        print_string("inpsos> ");
        char command_buf[64];
        fgets_freestanding(command_buf, sizeof(command_buf));

        if (strcmp(command_buf, "install") == 0) {
            run_install();
        } else {
            if (!installed) {
                print_string("Error: Command locked. This system command is disabled on Live Media.\n");
                print_string("Please partition disk by running the 'install' utility.\n");
            } else {
                if (strcmp(command_buf, "clear") == 0) clear_screen();
                else if (strcmp(command_buf, "restart") == 0) sys_reboot();
                else if (strcmp(command_buf, "shutdown") == 0) sys_shutdown();
                else {
                    if (active_port) {
                        DirectoryTable dir_table;
                        memset(&dir_table, 0, sizeof(DirectoryTable));
                        if (ahci_read(active_port, 2, 0, 1, (uint16_t*)&dir_table)) {
                            int found = 0;
                            for (int i = 0; i < MAX_FILES; i++) {
                                if (strcmp(dir_table.entries[i].filename, command_buf) == 0) {
                                    run_easec(command_buf);
                                    found = 1; break;
                                }
                            }
                            if (!found && strlen(command_buf) > 0) printf("Error: Command or script file '%s' not recognized.\n", command_buf);
                        } else print_string("Error: Could not read local file records from disk.\n");
                    }
                }
            }
        }
    }
}