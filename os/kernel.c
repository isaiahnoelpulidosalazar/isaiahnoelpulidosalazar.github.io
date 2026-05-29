#include "freestanding.h"

/* -------------------------------------------------------------
   Standard Library Fallback Implementations
   ------------------------------------------------------------- */

FILE* stderr = (FILE*)1;
FILE* stdin  = (FILE*)2;
FILE* stdout = (FILE*)3;

uint8_t kheap[16 * 1024 * 1024]; // 16MB kernel heap
size_t heap_offset = 0;

void* kmalloc(size_t size) {
    size = (size + 3) & ~3; // 4-byte alignment
    if (heap_offset + sizeof(size_t) + size > sizeof(kheap)) return NULL;
    
    size_t* header = (size_t*)&kheap[heap_offset];
    *header = size; 
    heap_offset += sizeof(size_t) + size;
    return (void*)(header + 1);
}

void* krealloc(void* ptr, size_t new_size) {
    if (!ptr) return kmalloc(new_size);
    if (new_size == 0) return NULL; 

    size_t* header = (size_t*)ptr - 1;
    size_t old_size = *header;

    void* new_ptr = kmalloc(new_size);
    if (new_ptr) {
        memcpy(new_ptr, ptr, old_size < new_size ? old_size : new_size);
    }
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

size_t fread(void* ptr, size_t size, size_t nmemb, FILE* stream) {
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
                double val = va_arg(args, double); 
                long long int_part = (long long)val;
                char num_buf[64]; itoa(int_part, num_buf, 10);
                char* n = num_buf; while (*n && written < size - 1) str[written++] = *n++;
            } else if (*format == 'l') {
                format++; if (*format == 'l') format++;
                if (*format == 'd') {
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
    str[i] = '\0'; 
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

    while ((port->tfd & (AHCI_DEV_BUSY | AHCI_DEV_DRQ)) && spin < 10000000) spin++;
    if (spin == 10000000) return 0;
    
    port->ci = 1 << slot;

    int wait = 0;
    while (1) {
        if ((port->ci & (1 << slot)) == 0) break;
        if (port->is & (1 << 30)) return 0;
        if (wait++ > 50000000) return 0;
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

    while ((port->tfd & (AHCI_DEV_BUSY | AHCI_DEV_DRQ)) && spin < 10000000) spin++;
    if (spin == 10000000) return 0;

    port->ci = 1 << slot;

    int wait = 0;
    while (1) {
        if ((port->ci & (1 << slot)) == 0) break;
        if (port->is & (1 << 30)) return 0;
        if (wait++ > 50000000) return 0;
    }
    return 1;
}

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
                pci_config_write(bus, slot, 0, 0x04, cmd | 0x02 | 0x04); 
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
        hba_mem->ghc |= (1 << 31); // AE (AHCI Enable Globally)
        uint32_t pi = hba_mem->pi;
        
        for (int i = 0; i < 32; i++) {
            if (pi & (1 << i)) {
                HBAPort* port = &hba_mem->ports[i];
                
                // Force Spin-up and Power-On Native Drive
                if (!(port->cmd & (1 << 1))) port->cmd |= (1 << 1); // POD
                if (!(port->cmd & (1 << 2))) port->cmd |= (1 << 2); // SUD
                for(volatile int delay=0; delay<100000; delay++); // Spin-up delay
                
                port->serr = 0xFFFFFFFF; // Clear any pending boot errors

                if ((port->ssts & 0x0F) == 3 && port->sig == SATA_SIG_ATA) {
                    active_port = port;
                    
                    port->cmd &= ~(1 << 0);
                    port->cmd &= ~(1 << 4);
                    
                    int spin = 0;
                    while ((port->cmd & (1 << 14)) && spin++ < 1000000);
                    spin = 0;
                    while ((port->cmd & (1 << 15)) && spin++ < 1000000);
                    
                    void* clb_mem = malloc(2048 + 1024);
                    if (!clb_mem) return;
                    uintptr_t clb_align = (((uintptr_t)clb_mem + 1023) & ~1023);
                    port->clb = (uint32_t)clb_align; port->clbu = 0;
                    memset((void*)clb_align, 0, 1024);
                    
                    void* fb_mem = malloc(512 + 256);
                    if (!fb_mem) return;
                    uintptr_t fb_align = (((uintptr_t)fb_mem + 255) & ~255);
                    port->fb = (uint32_t)fb_align; port->fbu = 0;
                    memset((void*)fb_align, 0, 256);
                    
                    HBACommandHeader* cmdheader = (HBACommandHeader*)clb_align;
                    for (int j = 0; j < 32; j++) {
                        cmdheader[j].prdtl = 8;
                        void* ctba_mem = malloc(512 + 128);
                        if (!ctba_mem) return;
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
   Multiboot Struct Definitions
   ------------------------------------------------------------- */
#define MULTIBOOT_MAGIC 0x2BADB002

typedef struct {
    uint32_t mod_start;
    uint32_t mod_end;
    uint32_t string;
    uint32_t reserved;
} multiboot_module_t;

typedef struct {
    uint32_t flags;
    uint32_t mem_lower;
    uint32_t mem_upper;
    uint32_t boot_device;
    uint32_t cmdline;
    uint32_t mods_count;
    uint32_t mods_addr;
} multiboot_info_t;

uint32_t global_multiboot_magic = 0;
uint32_t global_multiboot_addr = 0;

/* -------------------------------------------------------------
   Dynamic Multi-File Native Filesystem (Physical Disk Storage)
   ------------------------------------------------------------- */

#define MAX_FILES 24

typedef struct {
    char filename[32];
    uint32_t start_lba;
    uint32_t file_size;
} FileEntry;

typedef struct {
    FileEntry entries[MAX_FILES];
    uint8_t padding[256];
} DirectoryTable;

/* Static DMA Arrays to securely lock SATA communications out of the CPU stack */
static char dma_sector_buffer[512] __attribute__((aligned(16)));
static DirectoryTable dma_dir_table __attribute__((aligned(16)));
static char dma_script_buffer[65536] __attribute__((aligned(16)));

typedef struct {
    char filename[32];
    char* buffer;
    size_t size;
    size_t capacity;
    int is_write;
} ActiveFile;

static ActiveFile open_file;

/* Real Native Easec VM file mapping directly to Physical SATA Layout */
FILE* fopen(const char* filename, const char* mode) {
    if (!active_port) return NULL;
    memset(&open_file, 0, sizeof(ActiveFile));
    strncpy(open_file.filename, filename, 31);
    
    if (strcmp(mode, "w") == 0 || strcmp(mode, "wb") == 0) {
        open_file.is_write = 1;
        open_file.capacity = 4096; 
        open_file.buffer = malloc(open_file.capacity);
        if (!open_file.buffer) return NULL;
        memset(open_file.buffer, 0, open_file.capacity);
        open_file.size = 0;
        return (FILE*)&open_file;
    }
    return NULL;
}

size_t fwrite(const void* ptr, size_t size, size_t nmemb, FILE* stream) {
    ActiveFile* f = (ActiveFile*)stream;
    if (!f || !f->is_write) return 0;
    
    size_t bytes_to_write = size * nmemb;
    if (f->size + bytes_to_write > f->capacity) bytes_to_write = f->capacity - f->size;
    memcpy(f->buffer + f->size, ptr, bytes_to_write);
    f->size += bytes_to_write;
    return bytes_to_write / size;
}

int fclose(FILE* stream) {
    ActiveFile* f = (ActiveFile*)stream;
    if (!f) return -1;
    
    if (f->is_write && f->size > 0) {
        memset(&dma_dir_table, 0, sizeof(DirectoryTable));
        if (!ahci_read(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table)) return -1;
        
        int slot = -1;
        uint32_t next_free_lba = 15; // User file writes get mapped dynamically
        
        for (int i = 0; i < MAX_FILES; i++) {
            if (strcmp(dma_dir_table.entries[i].filename, f->filename) == 0) { slot = i; break; }
            if (dma_dir_table.entries[i].start_lba > 0) {
                uint32_t end_lba = dma_dir_table.entries[i].start_lba + (dma_dir_table.entries[i].file_size + 511) / 512;
                if (end_lba > next_free_lba) next_free_lba = end_lba;
            }
        }
        
        if (slot == -1) {
            for (int i = 0; i < MAX_FILES; i++) {
                if (dma_dir_table.entries[i].start_lba == 0) {
                    slot = i; dma_dir_table.entries[i].start_lba = next_free_lba; break;
                }
            }
        }
        
        if (slot == -1) { print_string("Error: SATA directory limit.\n"); return -1; }
        
        strncpy(dma_dir_table.entries[slot].filename, f->filename, 31);
        dma_dir_table.entries[slot].file_size = f->size;
        
        uint32_t sectors_to_write = (f->size + 511) / 512;
        static char temp_sector[512] __attribute__((aligned(16)));
        
        for (uint32_t s = 0; s < sectors_to_write; s++) {
            memset(temp_sector, 0, 512);
            size_t chunk = (f->size - s * 512) > 512 ? 512 : (f->size - s * 512);
            memcpy(temp_sector, f->buffer + s * 512, chunk);
            ahci_write(active_port, dma_dir_table.entries[slot].start_lba + s, 0, 1, (uint16_t*)temp_sector);
        }
        ahci_write(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table); // Write 2 sectors to cover up to 24 files
    }
    return 0;
}

int remove(const char* filename) {
    if (!active_port) return -1;
    memset(&dma_dir_table, 0, sizeof(DirectoryTable));
    if (!ahci_read(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table)) return -1;
    
    for (int i = 0; i < MAX_FILES; i++) {
        if (strcmp(dma_dir_table.entries[i].filename, filename) == 0) {
            memset(&dma_dir_table.entries[i], 0, sizeof(FileEntry));
            if (ahci_write(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table)) return 0; 
            return -1;
        }
    }
    return -1; 
}

/* -------------------------------------------------------------
   Dynamic Multi-File Hard Drive Scanner & Installer
   ------------------------------------------------------------- */

int check_installation_state(HBAPort* port) {
    memset(dma_sector_buffer, 0, 512);
    if (ahci_read(port, 1, 0, 1, (uint16_t*)dma_sector_buffer)) {
        if (memcmp(dma_sector_buffer, "INPSOS_INSTALLED", 16) == 0) return 1; 
    }
    return 0; 
}

void run_easec(const char* filename) {
    if (!active_port) { print_string("Error: Active AHCI port missing.\n"); return; }

    memset(&dma_dir_table, 0, sizeof(DirectoryTable));
    if (!ahci_read(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table)) { print_string("Error: Failed to fetch storage directory.\n"); return; }

    FileEntry* target_entry = NULL;
    for (int i = 0; i < MAX_FILES; i++) {
        if (strcmp(dma_dir_table.entries[i].filename, filename) == 0) {
            target_entry = &dma_dir_table.entries[i]; break;
        }
    }

    if (!target_entry) { printf("Error: Module script '%s' not found.\n", filename); return; }

    uint32_t sectors_to_read = (target_entry->file_size + 511) / 512;
    if (sectors_to_read * 512 > sizeof(dma_script_buffer)) { print_string("Error: Script size exceeds memory.\n"); return; }

    memset(dma_script_buffer, 0, sizeof(dma_script_buffer));
    if (!ahci_read(active_port, target_entry->start_lba, 0, sectors_to_read, (uint16_t*)dma_script_buffer)) {
        print_string("Error: Read aborted mid-transmission.\n"); return;
    }

    dma_script_buffer[target_entry->file_size] = '\0';

    init_vm();
    void* global_env = create_env(NULL);
    
    // Scan real mapped directory on the fly
    char file_list_buffer[1024];
    memset(file_list_buffer, 0, sizeof(file_list_buffer));
    strcpy(file_list_buffer, "Files mapped on SATA drive:\n");
    int count = 0;
    for (int i = 0; i < MAX_FILES; i++) {
        if (dma_dir_table.entries[i].start_lba > 0) {
            char line[128];
            snprintf(line, sizeof(line), "  - %-16s [LBA: %4d]\n", dma_dir_table.entries[i].filename, dma_dir_table.entries[i].start_lba);
            strcat(file_list_buffer, line);
            count++;
        }
    }
    if (count == 0) strcat(file_list_buffer, "  (Directory is empty)\n");

    void* list_str = allocate_string(file_list_buffer, strlen(file_list_buffer));
    env_define(global_env, "sys_list_dir", make_obj_val(list_str));

    run_script(dma_script_buffer, global_env);
}

void run_install() {
    print_string("Initializing physical installation onto hard disk...\n");
    if (!active_port) { print_string("Error: Compatible AHCI SATA controller not detected.\n"); return; }
    
    multiboot_info_t* mbi = (multiboot_info_t*)global_multiboot_addr;
    if (global_multiboot_magic != MULTIBOOT_MAGIC || !(mbi->flags & (1 << 3))) {
        print_string("Error: No installation modules provided by ISO bootloader.\n");
        return;
    }
    
    memset(dma_sector_buffer, 0, 512); memcpy(dma_sector_buffer, "INPSOS_INSTALLED", 16);
    if (!ahci_write(active_port, 1, 0, 1, (uint16_t*)dma_sector_buffer)) { 
        print_string("Error: Local layout boot sector write failure.\n"); 
        print_string("Hint: Make sure a secondary writable SATA Hard Disk is attached to the VM!\n");
        return; 
    }

    memset(&dma_dir_table, 0, sizeof(DirectoryTable));
    multiboot_module_t* mods = (multiboot_module_t*)mbi->mods_addr;
    uint32_t current_lba = 4; // Start laying out files at LBA 4

    // Iterate through every .easec file dynamically detected and mapped by GRUB
    for (uint32_t i = 0; i < mbi->mods_count && i < MAX_FILES; i++) {
        char* mod_string = (char*)mods[i].string;
        uint32_t size = mods[i].mod_end - mods[i].mod_start;
        char* content = (char*)mods[i].mod_start;

        char name[32];
        memset(name, 0, sizeof(name));
        char* last_slash = strrchr(mod_string, '/');
        char* base = last_slash ? last_slash + 1 : mod_string;
        
        int j = 0;
        while (base[j] && base[j] != '.' && base[j] != ' ' && j < 31) {
            name[j] = base[j];
            j++;
        }

        strcpy(dma_dir_table.entries[i].filename, name);
        dma_dir_table.entries[i].start_lba = current_lba;
        dma_dir_table.entries[i].file_size = size;

        uint32_t sectors = (size + 511) / 512;
        for (uint32_t s = 0; s < sectors; s++) {
            memset(dma_sector_buffer, 0, 512);
            size_t chunk = (size - s * 512) > 512 ? 512 : (size - s * 512);
            memcpy(dma_sector_buffer, content + s * 512, chunk);
            ahci_write(active_port, current_lba + s, 0, 1, (uint16_t*)dma_sector_buffer);
        }
        current_lba += sectors;
        printf("Flushed file: %s (LBA Block %d)\n", name, dma_dir_table.entries[i].start_lba);
    }

    if (!ahci_write(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table)) { print_string("Error: Directory Table write aborted.\n"); return; }

    print_string("INPSOS installation onto SATA partitions completed.\n");
    print_string("Please detach your installation media and reboot computer.\n");
}

/* -------------------------------------------------------------
   Critical CPU Initializers
   ------------------------------------------------------------- */

void enable_fpu() {
    uint32_t cr0;
    __asm__ volatile("mov %%cr0, %0" : "=r"(cr0));
    cr0 &= ~(1 << 2); 
    cr0 |= (1 << 1);  
    __asm__ volatile("mov %0, %%cr0" :: "r"(cr0));
    __asm__ volatile("fninit"); 
}

/* -------------------------------------------------------------
   Main Shell Entry
   ------------------------------------------------------------- */

void kernel_main(uint32_t magic, uint32_t addr) {
    global_multiboot_magic = magic;
    global_multiboot_addr = addr;

    __asm__ volatile("cli");
    enable_fpu(); 

    clear_screen();
    print_string("=========================================\n");
    print_string("          Welcome to inpsos              \n");
    print_string("=========================================\n");

    find_ahci_device();
    
    int installed = 0;
    if (active_port) installed = check_installation_state(active_port);

    if (!installed) {
        print_string("STATUS: Running from Bootable Live ISO.\n");
        print_string("Please run command 'install' to setup onto local hardware.\n\n");
    } else {
        print_string("STATUS: Booted from Physical Drive.\n");
    }

    while (1) {
        print_string("inpsos> ");
        char command_buf[64];
        fgets_freestanding(command_buf, sizeof(command_buf));

        if (strcmp(command_buf, "install") == 0) {
            run_install();
        } else {
            if (!installed) {
                print_string("Please run command 'install' to setup onto local hardware.\n");
            } else {
                // Command processing is fully driven dynamically off target LBA sectors
                if (active_port) {
                    memset(&dma_dir_table, 0, sizeof(DirectoryTable));
                    if (ahci_read(active_port, 2, 0, 2, (uint16_t*)&dma_dir_table)) {
                        int found = 0;
                        for (int i = 0; i < MAX_FILES; i++) {
                            if (strcmp(dma_dir_table.entries[i].filename, command_buf) == 0) {
                                run_easec(command_buf);
                                found = 1; break;
                            }
                        }
                        if (!found && strlen(command_buf) > 0) {
                            // Hardcoded kernel fallbacks for system utilities
                            if (strcmp(command_buf, "clear") == 0) clear_screen();
                            else if (strcmp(command_buf, "restart") == 0) sys_reboot();
                            else if (strcmp(command_buf, "shutdown") == 0) sys_shutdown();
                            else {
                                printf("Error: Command or script file '%s' not recognized.\n", command_buf);
                            }
                        }
                    } else print_string("Error: Could not read local file records from disk.\n");
                }
            }
        }
    }
}