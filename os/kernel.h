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

// --- Var Args ---
typedef __builtin_va_list va_list;
#define va_start(v,l) __builtin_va_start(v,l)
#define va_end(v) __builtin_va_end(v)
#define va_arg(v,l) __builtin_va_arg(v,l)

// --- Exception Handling ---
typedef struct { uint32_t regs[6]; } jmp_buf[1];
int setjmp(jmp_buf buf);
void longjmp(jmp_buf buf, int val);

// --- Hardware Port I/O ---
static inline void outb(uint16_t port, uint8_t val) { asm volatile("outb %0, %1" : : "a"(val), "Nd"(port)); }
static inline void outw(uint16_t port, uint16_t val) { asm volatile("outw %0, %1" : : "a"(val), "Nd"(port)); }
static inline void outl(uint16_t port, uint32_t val) { asm volatile("outl %0, %1" : : "a"(val), "Nd"(port)); }
static inline uint8_t inb(uint16_t port) { uint8_t ret; asm volatile("inb %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }
static inline uint16_t inw(uint16_t port) { uint16_t ret; asm volatile("inw %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }
static inline uint32_t inl(uint16_t port) { uint32_t ret; asm volatile("inl %1, %0" : "=a"(ret) : "Nd"(port)); return ret; }

// --- Memory Subsystem ---
void* malloc(size_t size);
void free(void* p);
void* calloc(size_t n, size_t size);
void* realloc(void* p, size_t size);

// --- String Subsystem ---
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

// --- Ctype & Parsing ---
int isspace(int c); int isdigit(int c); int isalpha(int c); int isalnum(int c);
long long atoll(const char* str);
long long strtoll(const char* str, char** endptr, int base);
double atof(const char* str);
double strtod(const char* str, char** endptr);

// --- OS Standard Library Interfaces ---
void os_printf(const char* fmt, ...);
int os_sprintf(char* buf, const char* fmt, ...);
void os_getline(char* buf, int max);
void vga_clear();

// ====================================================================
// AHCI / SATA HARDWARE STRUCTURES
// ====================================================================

#define SATA_SIG_ATA   0x00000101  // SATA Hard Drive
#define SATA_SIG_ATAPI 0xEB140101  // SATA CD/DVD Drive (ATAPI)
#define SATA_SIG_SEMB  0xC33C0101  // Enclosure management bridge
#define SATA_SIG_PM    0x96690101  // Port multiplier

// CRITICAL: Force the compiler to not pad these structures.
// These map exactly to physical hardware silicon!
#pragma pack(push, 1)

typedef volatile struct {
    uint32_t clb;       // 0x00: Command list base address (low)
    uint32_t clbu;      // 0x04: Command list base address (high)
    uint32_t fb;        // 0x08: FIS base address (low)
    uint32_t fbu;       // 0x0C: FIS base address (high)
    uint32_t is;        // 0x10: Interrupt status
    uint32_t ie;        // 0x14: Interrupt enable
    uint32_t cmd;       // 0x18: Command and status
    uint32_t reserved0; // 0x1C: Reserved
    uint32_t tfd;       // 0x20: Task file data
    uint32_t sig;       // 0x24: Signature
    uint32_t ssts;      // 0x28: SATA status (Drive Detection)
    uint32_t sctl;      // 0x2C: SATA control
    uint32_t serr;      // 0x30: SATA error
    uint32_t sact;      // 0x34: SATA active
    uint32_t ci;        // 0x38: Command issue
    uint32_t sntf;      // 0x3C: SATA notification
    uint32_t fbs;       // 0x40: FIS-based switching control
    uint32_t reserved1[11];
    uint32_t vendor[4];
} HBA_PORT;

typedef volatile struct {
    uint32_t cap;       // 0x00: Host capabilities
    uint32_t ghc;       // 0x04: Global host control
    uint32_t is;        // 0x08: Interrupt status
    uint32_t pi;        // 0x0C: Ports implemented
    uint32_t vs;        // 0x10: AHCI Version
    uint32_t ccc_ctl;   // 0x14: Command completion coalescing control
    uint32_t ccc_pts;   // 0x18: Command completion coalescing ports
    uint32_t em_loc;    // 0x1C: Enclosure management location
    uint32_t em_ctl;    // 0x20: Enclosure management control
    uint32_t cap2;      // 0x24: Host capabilities 2
    uint32_t bohc;      // 0x28: BIOS/OS handoff control and status
    uint8_t  reserved[0xA0 - 0x2C];
    uint8_t  vendor[0x100 - 0xA0];
    HBA_PORT ports[32]; // 0x100 ~ 0x10FF: Port registers
} HBA_MEM;

typedef struct {
    uint8_t  cfl:5;     // Command FIS length in DWORDS
    uint8_t  a:1;       // ATAPI
    uint8_t  w:1;       // Write, 1: H2D, 0: D2H
    uint8_t  p:1;       // Prefetchable
    uint8_t  r:1;       // Reset
    uint8_t  b:1;       // BIST
    uint8_t  c:1;       // Clear busy upon R_OK
    uint8_t  rsv0:1;    // Reserved
    uint8_t  pmp:4;     // Port multiplier port
    uint16_t prdtl;     // PRDT entries count
    volatile uint32_t prdbc; // Byte count transferred
    uint32_t ctba;      // Command table descriptor base address
    uint32_t ctbau;     // Command table descriptor base address (high)
    uint32_t rsv1[4];   // Reserved
} HBA_CMD_HEADER;

typedef struct {
    uint32_t dba;       // Data base address
    uint32_t dbau;      // Data base address (high)
    uint32_t rsv0;      // Reserved
    uint32_t dbc:22;    // Byte count, 4M max, value must be (count - 1)
    uint32_t rsv1:9;    // Reserved
    uint32_t i:1;       // Interrupt on completion
} HBA_PRDT_ENTRY;

typedef struct {
    uint8_t  cfis[64];  // Command FIS
    uint8_t  acmd[16];  // ATAPI command
    uint8_t  rsv[48];   // Reserved
    HBA_PRDT_ENTRY prdt_entry[1]; // PRDT table
} HBA_CMD_TBL;

typedef struct {
    uint8_t  fis_type;  // FIS_TYPE_REG_H2D (0x27)
    uint8_t  pmport:4;  // Port multiplier
    uint8_t  rsv0:3;    // Reserved
    uint8_t  c:1;       // 1: Command, 0: Control
    uint8_t  command;   // Command register
    uint8_t  featurel;  // Feature register, 7:0
    uint8_t  lba0;      // LBA low register, 7:0
    uint8_t  lba1;      // LBA mid register, 15:8
    uint8_t  lba2;      // LBA high register, 23:16
    uint8_t  device;    // Device register
    uint8_t  lba3;      // LBA register, 31:24
    uint8_t  lba4;      // LBA register, 39:32
    uint8_t  lba5;      // LBA register, 47:40
    uint8_t  featureh;  // Feature register, 15:8
    uint8_t  countl;    // Count register, 7:0
    uint8_t  counth;    // Count register, 15:8
    uint8_t  icc;       // Isochronous command completion
    uint8_t  control;   // Control register
    uint8_t  rsv1[4];   // Reserved
} FIS_REG_H2D;

#pragma pack(pop)

// ====================================================================

// --- Inpsos Real Filesystem (Physical Disk) ---
extern char current_dir_path[128];
void sys_format_os();
void sys_install_os();
bool try_load_os();
void fs_init();
char* fs_read(const char* path);
void fs_write(const char* path, const char* content);
void fs_append(const char* path, const char* content);
int fs_delete(const char* path);
void fs_mkdir(const char* path);
void fs_rmdir(const char* path);
void fs_cd(const char* path);
void fs_ls(const char* path);

// --- Easec Main Entry ---
extern jmp_buf easec_env;
void run_easec(const char* code, const char* arg);

#endif