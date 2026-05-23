#include "kernel.h"
#include "scripts.h" // AUTO-GENERATED BUNDLE FROM MAKEFILE

// --- SETJMP / LONGJMP ---
__attribute__((naked)) int setjmp(jmp_buf buf) { asm volatile ("mov 4(%esp), %eax\n mov %ebx, 0(%eax)\n mov %esi, 4(%eax)\n mov %edi, 8(%eax)\n mov %ebp, 12(%eax)\n mov %esp, 16(%eax)\n mov 0(%esp), %edx\n mov %edx, 20(%eax)\n xor %eax, %eax\n ret\n"); }
__attribute__((naked)) void longjmp(jmp_buf buf, int val) { asm volatile ("mov 4(%esp), %edx\n mov 8(%esp), %eax\n test %eax, %eax\n jnz 1f\n inc %eax\n 1:\n mov 0(%edx), %ebx\n mov 4(%edx), %esi\n mov 8(%edx), %edi\n mov 12(%edx), %ebp\n mov 16(%edx), %esp\n mov 20(%edx), %ecx\n jmp *%ecx\n"); }

// --- MEMORY ALLOCATOR ---
extern char end; uint32_t heap_ptr = 0;
void* malloc(size_t size) { if (!heap_ptr) heap_ptr = (uint32_t)&end; if (size % 4) size += 4 - (size % 4); uint32_t tmp = heap_ptr; *(size_t*)tmp = size; heap_ptr += size + sizeof(size_t); return (void*)(tmp + sizeof(size_t)); }
void free(void* p) { (void)p; }
void* calloc(size_t n, size_t size) { void* p = malloc(n * size); memset(p, 0, n * size); return p; }
void* realloc(void* p, size_t size) { if(!p) return malloc(size); if(size == 0) return NULL; size_t old_size = *((size_t*)p - 1); if (old_size >= size) return p; void* newp = malloc(size); memcpy(newp, p, old_size); return newp; }

// --- LIBC STRING & CTYPE ---
size_t strlen(const char* s) { size_t i=0; while(s[i]) i++; return i; } char* strcpy(char* d, const char* s) { int i=0; while((d[i]=s[i])) i++; return d; } char* strncpy(char* d, const char* s, size_t n) { size_t i=0; while(i<n && s[i]) {d[i]=s[i]; i++;} while(i<n) d[i++]='\0'; return d; } int strcmp(const char* s1, const char* s2) { while(*s1 && (*s1==*s2)) { s1++; s2++; } return *(const unsigned char*)s1 - *(const unsigned char*)s2; } int strncmp(const char* s1, const char* s2, size_t n) { while(n--) { if(*s1!=*s2) return *(const unsigned char*)s1 - *(const unsigned char*)s2; s1++; s2++; } return 0; } int strcasecmp(const char* s1, const char* s2) { while(*s1) { char c1 = (*s1>='A'&&*s1<='Z')?*s1+32:*s1; char c2 = (*s2>='A'&&*s2<='Z')?*s2+32:*s2; if (c1 != c2) return c1 - c2; s1++; s2++; } return *s2 == 0 ? 0 : -1; } char* strrchr(const char* s, int c) { const char* last = NULL; while(*s) { if (*s == (char)c) last = s; s++; } return (char*)last; } void* memset(void* s, int c, size_t n) { unsigned char* p=s; while(n--) *p++=(unsigned char)c; return s; } void* memcpy(void* d, const void* s, size_t n) { unsigned char* pd=d; const unsigned char* ps=s; while(n--) *pd++=*ps++; return d; } void* memmove(void* d, const void* s, size_t n) { unsigned char* pd=d; const unsigned char* ps=s; if(pd<ps) while(n--) *pd++=*ps++; else { pd+=n; ps+=n; while(n--) *--pd=*--ps; } return d; } size_t strcspn(const char* s, const char* reject) { size_t c=0; while(*s) { const char* r=reject; while(*r) { if(*s==*r) return c; r++; } s++; c++; } return c; } char* strcat(char* d, const char* s) { strcpy(d + strlen(d), s); return d; } int isspace(int c) { return c==' '||c=='\t'||c=='\n'||c=='\r'||c=='\f'||c=='\v'; } int isdigit(int c) { return c>='0'&&c<='9'; } int isalpha(int c) { return (c>='a'&&c<='z')||(c>='A'&&c<='Z'); } int isalnum(int c) { return isalpha(c)||isdigit(c); }
long long atoll(const char* str) { long long res=0; int sign=1; while(isspace(*str)) str++; if(*str=='-') { sign=-1; str++; } else if(*str=='+') str++; while(isdigit(*str)) res = res*10 + (*str++ - '0'); return res*sign; } double atof(const char* str) { double res=0, frac=1; int sign=1; while(isspace(*str)) str++; if(*str=='-') { sign=-1; str++; } else if(*str=='+') str++; while(isdigit(*str)) res = res*10 + (*str++ - '0'); if(*str=='.') { str++; while(isdigit(*str)) { res = res*10 + (*str++ - '0'); frac*=10; } } return sign * (res/frac); } long long strtoll(const char* str, char** endptr, int base) { (void)base; if(endptr) *endptr = (char*)str + strlen(str); return atoll(str); } double strtod(const char* str, char** endptr) { if(endptr) *endptr = (char*)str + strlen(str); return atof(str); }
void itoa(long long num, char* str) { int i = 0; bool is_neg = false; if (num == 0) { str[i++] = '0'; str[i] = '\0'; return; } if (num < 0) { is_neg = true; num = -num; } while(num != 0) { str[i++] = (num % 10) + '0'; num /= 10; } if (is_neg) str[i++] = '-'; str[i] = '\0'; for(int j=0; j<i/2; j++) { char t=str[j]; str[j]=str[i-1-j]; str[i-1-j]=t; } }
void ftoa(double n, char* res) { long long ipart = (long long)n; double fpart = n - (double)ipart; if(n < 0 && ipart == 0) { *res++ = '-'; fpart = -fpart; } else if (n < 0) fpart = -fpart; itoa(ipart, res); int len = strlen(res); res[len] = '.'; long long frac = (long long)(fpart * 1000000); itoa(frac, res + len + 1); }
int os_sprintf(char* buf, const char* fmt, ...) { va_list args; va_start(args, fmt); int i = 0; while(*fmt) { if (*fmt == '%') { fmt++; if (*fmt == 'l' && *(fmt+1) == 'l' && *(fmt+2) == 'd') { char tmp[64]; itoa(va_arg(args, long long), tmp); strcpy(&buf[i], tmp); i += strlen(tmp); fmt += 2; } else if (*fmt == 'g' || *fmt == 'f') { char tmp[64]; ftoa(va_arg(args, double), tmp); strcpy(&buf[i], tmp); i += strlen(tmp); } else if (*fmt == 's') { char* str = va_arg(args, char*); strcpy(&buf[i], str); i += strlen(str); } else if (*fmt == 'c') { buf[i++] = (char)va_arg(args, int); } } else { buf[i++] = *fmt; } fmt++; } buf[i] = '\0'; va_end(args); return i; }

// --- VGA DRIVER ---
uint16_t* vga = (uint16_t*)0xB8000; int cx = 0, cy = 0;
void vga_scroll() { if (cy >= 25) { for(int i=0; i<24*80; i++) vga[i] = vga[i + 80]; for(int i=24*80; i<25*80; i++) vga[i] = 0x0720; cy = 24; } }
void vga_putchar(char c) { if (c == '\n') { cx = 0; cy++; } else if (c == '\b') { if (cx > 0) cx--; else if (cy > 0) { cy--; cx = 79; } vga[cy * 80 + cx] = 0x0720; } else { vga[cy * 80 + cx] = (uint16_t)c | 0x0700; cx++; if (cx >= 80) { cx = 0; cy++; } } vga_scroll(); uint16_t pos = cy * 80 + cx; outb(0x3D4, 0x0F); outb(0x3D5, (uint8_t)(pos & 0xFF)); outb(0x3D4, 0x0E); outb(0x3D5, (uint8_t)((pos >> 8) & 0xFF)); }
void vga_clear() { for(int i=0; i<25*80; i++) vga[i] = 0x0720; cx = 0; cy = 0; }
void os_printf(const char* fmt, ...) { 
    va_list args; va_start(args, fmt); 
    while(*fmt) { 
        if (*fmt == '%') { 
            fmt++; 
            if (*fmt == 'l' && *(fmt+1) == 'l' && *(fmt+2) == 'd') { 
                char tmp[64]; itoa(va_arg(args, long long), tmp); 
                for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]); fmt += 2; 
            } else if (*fmt == 'g' || *fmt == 'f') { 
                char tmp[64]; ftoa(va_arg(args, double), tmp); 
                for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]); 
            } else if (*fmt == 's') { 
                char* str = va_arg(args, char*); 
                for(int k=0; str[k]; k++) vga_putchar(str[k]); 
            } else if (*fmt == 'c') { 
                vga_putchar((char)va_arg(args, int)); 
            } else if (*fmt == 'd') { 
                int val = va_arg(args, int);
                char tmp[64]; itoa(val, tmp);
                for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]);
            } else if (*fmt == 'x') { 
                uint32_t val = va_arg(args, uint32_t);
                char tmp[64]; int pos = 0;
                if (val == 0) { tmp[pos++] = '0'; }
                else { while (val > 0) { int d = val % 16; if (d < 10) tmp[pos++] = d + '0'; else tmp[pos++] = (d - 10) + 'a'; val /= 16; } }
                tmp[pos] = '\0';
                for(int j=0; j<pos/2; j++) { char t=tmp[j]; tmp[j]=tmp[pos-1-j]; tmp[pos-1-j]=t; }
                for(int k=0; tmp[k]; k++) vga_putchar(tmp[k]);
            }
        } else { vga_putchar(*fmt); } fmt++; 
    } 
    va_end(args); 
}

// --- KEYBOARD DRIVER ---
const char kbd_US[128] = { 0,27,'1','2','3','4','5','6','7','8','9','0','-','=','\b','\t','q','w','e','r','t','y','u','i','o','p','[',']','\n', 0,'a','s','d','f','g','h','j','k','l',';','\'','`',0,'\\','z','x','c','v','b','n','m',',','.','/',0,'*',0,' ',0 };
const char kbd_US_shift[128] = { 0,27,'!','@','#','$','%','^','&','*','(',')','_','+','\b','\t','Q','W','E','R','T','Y','U','I','O','P','{','}','\n', 0,'A','S','D','F','G','H','J','K','L',':','"','~',0,'|','Z','X','C','V','B','N','M','<','>','?',0,'*',0,' ',0 };
char os_getchar() { static bool shift = false; while(1) { if (inb(0x64) & 1) { uint8_t sc = inb(0x60); if (sc == 0x2A || sc == 0x36) shift = true; else if (sc == 0xAA || sc == 0xB6) shift = false; else if (!(sc & 0x80)) { char c = shift ? kbd_US_shift[sc] : kbd_US[sc]; if (c) return c; } } } }
void os_getline(char* buf, int max) { int i = 0; while(i < max - 1) { char c = os_getchar(); if (c == '\b') { if (i > 0) { i--; vga_putchar('\b'); vga_putchar(' '); vga_putchar('\b'); } } else if (c == '\n') { os_printf("\n"); buf[i] = '\0'; return; } else { os_printf("%c", c); buf[i++] = c; } } buf[i] = '\0'; }

// --- PCI BUS SCANNER ---
uint32_t pci_read_config_dword(uint8_t bus, uint8_t slot, uint8_t func, uint8_t offset) {
    uint32_t address = (uint32_t)((uint32_t)bus << 16) | ((uint32_t)slot << 11) | ((uint32_t)func << 8) | (offset & 0xFC) | ((uint32_t)0x80000000);
    outl(0xCF8, address); return inl(0xCFC);
}
void pci_write_config_dword(uint8_t bus, uint8_t slot, uint8_t func, uint8_t offset, uint32_t value) {
    uint32_t address = (uint32_t)((uint32_t)bus << 16) | ((uint32_t)slot << 11) | ((uint32_t)func << 8) | (offset & 0xFC) | ((uint32_t)0x80000000);
    outl(0xCF8, address); outl(0xCFC, value);
}
bool find_ahci_controller(uint8_t* out_bus, uint8_t* out_device, uint8_t* out_func) {
    for (uint16_t bus = 0; bus < 256; bus++) {
        for (uint8_t device = 0; device < 32; device++) {
            for (uint8_t func = 0; func < 8; func++) {
                uint32_t reg0 = pci_read_config_dword(bus, device, func, 0);
                if ((reg0 & 0xFFFF) == 0xFFFF) continue; 
                uint32_t reg8 = pci_read_config_dword(bus, device, func, 0x08);
                uint8_t class_code = (reg8 >> 24) & 0xFF; uint8_t subclass = (reg8 >> 16) & 0xFF; uint8_t prog_if = (reg8 >> 8) & 0xFF;
                if (class_code == 0x01 && subclass == 0x06 && prog_if == 0x01) { *out_bus = bus; *out_device = device; *out_func = func; return true; }
            }
        }
    } return false;
}
void* get_ahci_abar(uint8_t bus, uint8_t device, uint8_t func) {
    uint32_t bar5 = pci_read_config_dword(bus, device, func, 0x24);
    if (bar5 & 0x01) return NULL;
    uint8_t type = (bar5 >> 1) & 0x03;
    uint32_t abar_low = bar5 & 0xFFFFFFF0;
    if (type == 0x02 && pci_read_config_dword(bus, device, func, 0x28) != 0) return NULL; 
    return (void*)abar_low;
}

// --- AHCI DRIVER (SATA DMA) ---
HBA_PORT* active_ahci_ports[32]; 

void* ahci_alloc_aligned(size_t size, size_t alignment) {
    extern char end; static uint32_t ahci_heap = 0;
    if (!ahci_heap) ahci_heap = (uint32_t)&end + 0x200000; 
    if (ahci_heap % alignment) ahci_heap += alignment - (ahci_heap % alignment);
    uint32_t ptr = ahci_heap; ahci_heap += size; memset((void*)ptr, 0, size); return (void*)ptr;
}

void ahci_stop_cmd(HBA_PORT* port) {
    port->cmd &= ~0x0001; port->cmd &= ~0x0010; 
    int spin = 0;
    while (1) { if (port->cmd & 0x4000) { spin++; if(spin>100000) break; continue; } if (port->cmd & 0x8000) { spin++; if(spin>100000) break; continue; } break; }
}
void ahci_start_cmd(HBA_PORT* port) {
    int spin = 0;
    while (port->cmd & 0x8000) { spin++; if(spin>100000) break; } 
    port->cmd |= 0x0010; port->cmd |= 0x0001; 
}
void ahci_port_rebase(HBA_PORT* port) {
    ahci_stop_cmd(port);
    port->clb = (uint32_t)ahci_alloc_aligned(1024, 1024); port->clbu = 0;
    port->fb = (uint32_t)ahci_alloc_aligned(256, 256); port->fbu = 0;
    HBA_CMD_HEADER* cmdheader = (HBA_CMD_HEADER*)(port->clb);
    for (int i = 0; i < 32; i++) {
        cmdheader[i].prdtl = 1;
        cmdheader[i].ctba = (uint32_t)ahci_alloc_aligned(256, 128); cmdheader[i].ctbau = 0;
    }
    ahci_start_cmd(port);
}

bool ahci_read(HBA_PORT* port, uint32_t startl, uint32_t count, uint16_t* buf) {
    port->is = (uint32_t)-1; int slot = 0;
    HBA_CMD_HEADER* cmdheader = (HBA_CMD_HEADER*)port->clb;
    cmdheader[slot].cfl = sizeof(FIS_REG_H2D) / sizeof(uint32_t); cmdheader[slot].w = 0; cmdheader[slot].prdtl = 1;
    HBA_CMD_TBL* cmdtbl = (HBA_CMD_TBL*)(cmdheader[slot].ctba); memset(cmdtbl, 0, sizeof(HBA_CMD_TBL));
    cmdtbl->prdt_entry[0].dba = (uint32_t)buf; cmdtbl->prdt_entry[0].dbau = 0; cmdtbl->prdt_entry[0].dbc = (count * 512) - 1; cmdtbl->prdt_entry[0].i = 1;
    FIS_REG_H2D* cmdfis = (FIS_REG_H2D*)(&cmdtbl->cfis);
    cmdfis->fis_type = 0x27; cmdfis->c = 1; cmdfis->command = 0x25; // READ DMA EXT
    cmdfis->lba0 = (uint8_t)startl; cmdfis->lba1 = (uint8_t)(startl >> 8); cmdfis->lba2 = (uint8_t)(startl >> 16); cmdfis->device = 1 << 6;
    cmdfis->lba3 = (uint8_t)(startl >> 24); cmdfis->lba4 = 0; cmdfis->lba5 = 0;
    cmdfis->countl = count & 0xFF; cmdfis->counth = (count >> 8) & 0xFF;
    int spin = 0; while ((port->tfd & (0x80 | 0x08)) && spin < 100000) spin++; if (spin == 100000) return false;
    port->ci = (1 << slot);
    int spin2 = 0;
    while (1) { if ((port->ci & (1 << slot)) == 0) break; if (port->is & (1 << 30)) return false; spin2++; if (spin2 > 500000) return false; } return true;
}

bool ahci_write(HBA_PORT* port, uint32_t startl, uint32_t count, uint16_t* buf) {
    port->is = (uint32_t)-1; int slot = 0;
    HBA_CMD_HEADER* cmdheader = (HBA_CMD_HEADER*)port->clb;
    cmdheader[slot].cfl = sizeof(FIS_REG_H2D) / sizeof(uint32_t); cmdheader[slot].w = 1; cmdheader[slot].prdtl = 1;
    HBA_CMD_TBL* cmdtbl = (HBA_CMD_TBL*)(cmdheader[slot].ctba); memset(cmdtbl, 0, sizeof(HBA_CMD_TBL));
    cmdtbl->prdt_entry[0].dba = (uint32_t)buf; cmdtbl->prdt_entry[0].dbau = 0; cmdtbl->prdt_entry[0].dbc = (count * 512) - 1; cmdtbl->prdt_entry[0].i = 1;
    FIS_REG_H2D* cmdfis = (FIS_REG_H2D*)(&cmdtbl->cfis);
    cmdfis->fis_type = 0x27; cmdfis->c = 1; cmdfis->command = 0x35; // WRITE DMA EXT
    cmdfis->lba0 = (uint8_t)startl; cmdfis->lba1 = (uint8_t)(startl >> 8); cmdfis->lba2 = (uint8_t)(startl >> 16); cmdfis->device = 1 << 6;
    cmdfis->lba3 = (uint8_t)(startl >> 24); cmdfis->lba4 = 0; cmdfis->lba5 = 0;
    cmdfis->countl = count & 0xFF; cmdfis->counth = (count >> 8) & 0xFF;
    int spin = 0; while ((port->tfd & (0x80 | 0x08)) && spin < 100000) spin++; if (spin == 100000) return false;
    port->ci = (1 << slot);
    int spin2 = 0;
    while (1) { if ((port->ci & (1 << slot)) == 0) break; if (port->is & (1 << 30)) return false; spin2++; if (spin2 > 500000) return false; } return true;
}

void pci_scan_storage() {
    uint8_t ahci_bus, ahci_dev, ahci_func;
    for (int i=0; i<32; i++) active_ahci_ports[i] = NULL;

    if (find_ahci_controller(&ahci_bus, &ahci_dev, &ahci_func)) {
        uint32_t pci_cmd = pci_read_config_dword(ahci_bus, ahci_dev, ahci_func, 0x04);
        pci_write_config_dword(ahci_bus, ahci_dev, ahci_func, 0x04, pci_cmd | 0x06);
        HBA_MEM* abar = (HBA_MEM*)get_ahci_abar(ahci_bus, ahci_dev, ahci_func);
        if (abar != NULL && abar != (void*)0xFFFFFFFF) {
            abar->ghc |= (1U << 31); // Enable AHCI Mode
            uint32_t pi = abar->pi;
            for (int i = 0; i < 32; i++) {
                if (pi & (1 << i)) {
                    HBA_PORT* port = &abar->ports[i];
                    uint32_t ssts = port->ssts;
                    uint8_t det = ssts & 0x0F; uint8_t ipm = (ssts >> 8) & 0x0F;
                    if (det == 3 && ipm == 1 && port->sig == SATA_SIG_ATA) {
                        ahci_port_rebase(port);
                        active_ahci_ports[i] = port;
                    }
                }
            }
        }
    }
}

// --- ATA ENUMERATOR & PARTITION DRIVER ---
typedef struct { bool present; bool is_atapi; uint32_t sectors; uint16_t io_base; uint8_t drive_sel; } ata_drive_t;
ata_drive_t drives[4];

typedef struct { uint8_t status; uint8_t chs_first[3]; uint8_t type; uint8_t chs_last[3]; uint32_t lba_start; uint32_t num_sectors; } __attribute__((packed)) mbr_entry_t;

bool ata_wait_bsy(uint16_t io) { for(int i=0; i<100000; i++) { uint8_t status = inb(io + 7); if (status == 0xFF) return false; if (!(status & 0x80)) return true; } return false; }
bool ata_wait_drq(uint16_t io) { for(int i=0; i<100000; i++) { if (inb(io + 7) & 0x08) return true; } return false; }

void ata_identify(int idx, uint16_t io_base, uint8_t drive_sel) {
    drives[idx].present = false; drives[idx].io_base = io_base; drives[idx].drive_sel = drive_sel;
    if (inb(io_base + 7) == 0xFF) return; 
    outb(io_base + 6, drive_sel); outb(io_base + 2, 0); outb(io_base + 3, 0); outb(io_base + 4, 0); outb(io_base + 5, 0); outb(io_base + 7, 0xEC); 
    uint8_t status = inb(io_base + 7); if (status == 0 || status == 0xFF) return; if (!ata_wait_bsy(io_base)) return;
    if (inb(io_base + 4) != 0 || inb(io_base + 5) != 0) { drives[idx].is_atapi = true; drives[idx].present = true; return; }
    
    int spin = 0; 
    while (1) { status = inb(io_base + 7); if (status & 0x01) return; if (status & 0x08) break; spin++; if(spin > 100000) return; }
    uint16_t buf[256] = {0}; // FIXED: Initialize to prevent compiler warnings [2]
    for (int i = 0; i < 256; i++) buf[i] = inw(io_base + 0);
    drives[idx].present = true; drives[idx].is_atapi = false; 
    // FIXED: Use bitwise shifts to resolve pointer-pun aliasing warnings [2]
    drives[idx].sectors = (uint32_t)buf[60] | ((uint32_t)buf[61] << 16);
}

void ata_init() {
    ata_identify(0, 0x1F0, 0xA0); ata_identify(1, 0x1F0, 0xB0); 
    ata_identify(2, 0x170, 0xA0); ata_identify(3, 0x170, 0xB0); 
}

void raw_ata_write_sector(int idx, uint32_t lba, uint8_t* buffer) {
    uint16_t io = drives[idx].io_base; uint8_t sel = drives[idx].drive_sel;
    if (!ata_wait_bsy(io)) return;
    outb(io + 6, (sel | 0x40) | ((lba >> 24) & 0x0F));
    outb(io + 2, 1); outb(io + 3, (uint8_t)lba); outb(io + 4, (uint8_t)(lba >> 8)); outb(io + 5, (uint8_t)(lba >> 16)); outb(io + 7, 0x30); 
    if (!ata_wait_bsy(io) || !ata_wait_drq(io)) return;
    for(int i=0; i<256; i++) outw(io + 0, ((uint16_t*)buffer)[i]);
}

void raw_ata_read_sector(int idx, uint32_t lba, uint8_t* buffer) {
    uint16_t io = drives[idx].io_base; uint8_t sel = drives[idx].drive_sel;
    if (!ata_wait_bsy(io)) { memset(buffer, 0, 512); return; }
    outb(io + 6, (sel | 0x40) | ((lba >> 24) & 0x0F));
    outb(io + 2, 1); outb(io + 3, (uint8_t)lba); outb(io + 4, (uint8_t)(lba >> 8)); outb(io + 5, (uint8_t)(lba >> 16)); outb(io + 7, 0x20); 
    if (!ata_wait_bsy(io)) { memset(buffer, 0, 512); return; }
    if(inb(io + 7) & 0x01) { memset(buffer, 0, 512); return; } 
    if (!ata_wait_drq(io)) { memset(buffer, 0, 512); return; }
    for(int i=0; i<256; i++) ((uint16_t*)buffer)[i] = inw(io + 0);
}

// OS Disk Abstraction Layer
int boot_drive_idx = -1; 
bool boot_is_ahci = false;
uint32_t fs_lba_offset = 0; 
uint8_t ram_disk[1024 * 1024]; 

uint16_t* ahci_bounce_buffer = NULL;

void ata_write_sector(uint32_t lba, uint8_t* buffer) {
    if (boot_drive_idx == -1) { if(lba < 2048) memcpy(&ram_disk[lba * 512], buffer, 512); return; }
    if (boot_is_ahci) {
        if (!ahci_bounce_buffer) ahci_bounce_buffer = (uint16_t*)ahci_alloc_aligned(512, 16);
        memcpy(ahci_bounce_buffer, buffer, 512); 
        ahci_write(active_ahci_ports[boot_drive_idx], fs_lba_offset + lba, 1, ahci_bounce_buffer);
    } else {
        raw_ata_write_sector(boot_drive_idx, fs_lba_offset + lba, buffer);
    }
}
void ata_read_sector(uint32_t lba, uint8_t* buffer) {
    if (boot_drive_idx == -1) { if(lba < 2048) memcpy(buffer, &ram_disk[lba * 512], 512); return; }
    if (boot_is_ahci) {
        if (!ahci_bounce_buffer) ahci_bounce_buffer = (uint16_t*)ahci_alloc_aligned(512, 16);
        if(ahci_read(active_ahci_ports[boot_drive_idx], fs_lba_offset + lba, 1, ahci_bounce_buffer)) {
            memcpy(buffer, ahci_bounce_buffer, 512); 
        } else {
            memset(buffer, 0, 512);
        }
    } else {
        raw_ata_read_sector(boot_drive_idx, fs_lba_offset + lba, buffer);
    }
}

// --- INPSOS REAL FILESYSTEM (IFS) ---
#define FS_MAGIC "INPSOSFS"
#define MAX_NODES 256
#define DATA_START_SECTOR 100
#pragma pack(push, 1)
typedef struct { uint8_t used; uint8_t is_dir; uint16_t parent; char name[60]; uint32_t size; } fs_node_t;
#pragma pack(pop)

fs_node_t inodes[MAX_NODES];
uint16_t current_dir_id = 0;
char current_dir_path[128] = "/inpsos";

void fs_flush_nodes() {
    uint8_t magic_buf[512] = {0}; strcpy((char*)magic_buf, FS_MAGIC);
    ata_write_sector(1, magic_buf);
    uint8_t* ptr = (uint8_t*)inodes;
    for(int i=0; i < 34; i++) ata_write_sector(2 + i, ptr + i*512);
}
int fs_find_child(int parent_id, const char* name) { for(int i=0; i<MAX_NODES; i++) if(inodes[i].used && inodes[i].parent == parent_id && strcmp(inodes[i].name, name) == 0) return i; return -1; }
int fs_resolve(const char* path) {
    if(!path || strlen(path) == 0) return current_dir_id;
    int curr = current_dir_id; int i = 0;
    if (path[0] == '/') { curr = 0; i++; if(strncmp(&path[i], "inpsos", 6) == 0 && (path[i+6] == '/' || path[i+6] == '\0')) { i += 6; if(path[i] == '/') i++; } }
    char part[64];
    while(path[i]) {
        int p = 0; while(path[i] && path[i] != '/') part[p++] = path[i++]; part[p] = '\0';
        if (p > 0) {
            if (strcmp(part, "..") == 0) { if (curr != 0) curr = inodes[curr].parent; }
            else if (strcmp(part, ".") != 0) { int next = fs_find_child(curr, part); if (next == -1) return -1; curr = next; }
        } if(path[i] == '/') i++;
    } return curr;
}

void fs_rebuild_path(int id, char* out) {
    if (id == 0) { strcpy(out, "/inpsos"); return; }
    char tmp[256]; fs_rebuild_path(inodes[id].parent, tmp); os_sprintf(out, "%s/%s", tmp, inodes[id].name);
}

void sys_format_os() {
    memset(inodes, 0, sizeof(inodes));
    inodes[0].used = 1; inodes[0].is_dir = 1; inodes[0].parent = 0xFFFF; strcpy(inodes[0].name, "inpsos");
    inodes[1].used = 1; inodes[1].is_dir = 1; inodes[1].parent = 0; strcpy(inodes[1].name, "boot");
    inodes[2].used = 1; inodes[2].is_dir = 1; inodes[2].parent = 0; strcpy(inodes[2].name, "easec");
    fs_flush_nodes();

    fs_write("/inpsos/easec/clear.easec", "var tmp sys_clear");
    fs_write("/inpsos/easec/shutdown.easec", "var tmp sys_shutdown");
    fs_write("/inpsos/easec/restart.easec", "var tmp sys_restart");
    fs_write("/inpsos/easec/format.easec", "var tmp sys_format");
    fs_write("/inpsos/easec/install.easec", "var tmp sys_install"); 
    fs_write("/inpsos/easec/list.easec", "var tmp sys_ls");
    fs_write("/inpsos/easec/change_directory.easec", "var tmp sys_cd");
    fs_write("/inpsos/easec/create_folder.easec", "var tmp sys_mkdir");
    fs_write("/inpsos/easec/delete_folder.easec", "var tmp sys_rmdir");
    fs_write("/inpsos/easec/create_file.easec", "say \"Filename:\"\nvar fname get\nsay \"Content:\"\nvar fcontent get\nfile create fname [ fcontent ]");
    fs_write("/inpsos/easec/delete_file.easec", "say \"Delete target:\"\nvar fname get\nfile delete fname");
    fs_write("/inpsos/easec/demo.easec", "say \"--- EASEC DEMO ---\"\nsay \"Variables:\"\nvar number mynum 42\nsay mynum\nsay \"Arrays:\"\narray number arr 10, 20\nsay arr[1]\nsay \"Conditionals:\"\nif mynum == 42 [\nsay \"Number is 42!\"\n] else [\nsay \"No\"\n]\nsay \"Loops:\"\nvar i 0\nrepeat 3 [\nsay i\nincrement i 1\n]\nsay \"Jobs:\"\njob greet x [\nsay \"Hello \" + x\n]\ngreet \"User!\"\nsay \"Demo Complete.\"");
    
    load_easec_scripts(); 
    os_printf("System Formatted successfully.\n");
}

void sys_install_os() {
    struct { int drive_idx; bool is_ahci; uint32_t lba_start; uint32_t sectors; char name[128]; } install_targets[32];
    int target_count = 0;

    os_printf("\n--- INPSOS INSTALLER ---\nScanning hardware storage drives...\n\n");
    
    // 1. Map Legacy IDE Drives
    for(int i=0; i<4; i++) {
        if(drives[i].present && !drives[i].is_atapi) {
            install_targets[target_count].drive_idx = i; install_targets[target_count].is_ahci = false;
            install_targets[target_count].lba_start = 0; install_targets[target_count].sectors = drives[i].sectors;
            os_sprintf(install_targets[target_count].name, "Legacy IDE Drive %d (Whole Disk Format)", i);
            target_count++;

            uint8_t sector0[512]; raw_ata_read_sector(i, 0, sector0);
            if(sector0[510] == 0x55 && sector0[511] == 0xAA) {
                mbr_entry_t* pt = (mbr_entry_t*)(sector0 + 0x1BE);
                for(int p=0; p<4; p++) {
                    if(pt[p].type != 0) {
                        install_targets[target_count].drive_idx = i; install_targets[target_count].is_ahci = false;
                        install_targets[target_count].lba_start = pt[p].lba_start; install_targets[target_count].sectors = pt[p].num_sectors;
                        os_sprintf(install_targets[target_count].name, "Legacy IDE Drive %d, Part %d (Type 0x%x)", i, p+1, pt[p].type);
                        target_count++;
                    }
                }
            }
        }
    }

    // 2. Map Modern AHCI/SATA Drives
    for (int i=0; i<32; i++) {
        if (active_ahci_ports[i]) {
            install_targets[target_count].drive_idx = i; install_targets[target_count].is_ahci = true;
            install_targets[target_count].lba_start = 0; install_targets[target_count].sectors = 0; 
            os_sprintf(install_targets[target_count].name, "SATA/AHCI Drive on Port %d (Whole Disk Format)", i);
            target_count++;
            
            if (!ahci_bounce_buffer) ahci_bounce_buffer = (uint16_t*)ahci_alloc_aligned(512, 16);
            if (ahci_read(active_ahci_ports[i], 0, 1, ahci_bounce_buffer)) {
                uint8_t* sector0 = (uint8_t*)ahci_bounce_buffer;
                if(sector0[510] == 0x55 && sector0[511] == 0xAA) {
                    mbr_entry_t* pt = (mbr_entry_t*)(sector0 + 0x1BE);
                    for(int p=0; p<4; p++) {
                        if(pt[p].type != 0) {
                            install_targets[target_count].drive_idx = i; install_targets[target_count].is_ahci = true;
                            install_targets[target_count].lba_start = pt[p].lba_start; install_targets[target_count].sectors = pt[p].num_sectors;
                            os_sprintf(install_targets[target_count].name, "SATA/AHCI Drive %d, Part %d (Type 0x%x)", i, p+1, pt[p].type);
                            target_count++;
                        }
                    }
                }
            }
        }
    }

    if (target_count == 0) { os_printf("No installable Hard Drives found!\n"); return; }
    for(int i=0; i<target_count; i++) os_printf("[%d] %s\n", i, install_targets[i].name);
    
    os_printf("\nEnter target ID (or 'q' to cancel): "); char buf[16]; os_getline(buf, 16);
    if(buf[0] == 'q') return;
    int sel = (int)atoll(buf);
    if(sel < 0 || sel >= target_count) { os_printf("Invalid selection.\n"); return; }
    
    os_printf("WARNING: Erasing '%s'. Type 'yes' to proceed: ", install_targets[sel].name);
    os_getline(buf, 16); if(strcmp(buf, "yes") != 0) { os_printf("Cancelled.\n"); return; }
    
    boot_drive_idx = install_targets[sel].drive_idx;
    boot_is_ahci   = install_targets[sel].is_ahci;
    fs_lba_offset  = install_targets[sel].lba_start;
    
    sys_format_os();
    os_printf("\nInstallation Complete! You can safely reboot the computer.\n");
}

bool try_load_os() {
    ata_init();
    pci_scan_storage(); 

    // 1. Try IDE
    for(int i=0; i<4; i++) {
        if(!drives[i].present || drives[i].is_atapi) continue;
        uint8_t magic[512]; raw_ata_read_sector(i, 1, magic);
        if(strncmp((char*)magic, FS_MAGIC, 8) == 0) { boot_drive_idx = i; boot_is_ahci = false; fs_lba_offset = 0; goto load_fs; }
        
        uint8_t sector0[512]; raw_ata_read_sector(i, 0, sector0);
        if(sector0[510] == 0x55 && sector0[511] == 0xAA) {
            mbr_entry_t* pt = (mbr_entry_t*)(sector0 + 0x1BE);
            for(int p=0; p<4; p++) {
                if(pt[p].type != 0) {
                    raw_ata_read_sector(i, pt[p].lba_start + 1, magic);
                    if(strncmp((char*)magic, FS_MAGIC, 8) == 0) { boot_drive_idx = i; boot_is_ahci = false; fs_lba_offset = pt[p].lba_start; goto load_fs; }
                }
            }
        }
    }

    // 2. Try AHCI/SATA
    if (!ahci_bounce_buffer) ahci_bounce_buffer = (uint16_t*)ahci_alloc_aligned(512, 16);
    for (int i=0; i<32; i++) {
        if (!active_ahci_ports[i]) continue;
        
        if (ahci_read(active_ahci_ports[i], 1, 1, ahci_bounce_buffer)) {
            if(strncmp((char*)ahci_bounce_buffer, FS_MAGIC, 8) == 0) { boot_drive_idx = i; boot_is_ahci = true; fs_lba_offset = 0; goto load_fs; }
        }
        if (ahci_read(active_ahci_ports[i], 0, 1, ahci_bounce_buffer)) {
            uint8_t* sector0 = (uint8_t*)ahci_bounce_buffer;
            if(sector0[510] == 0x55 && sector0[511] == 0xAA) {
                mbr_entry_t* pt = (mbr_entry_t*)(sector0 + 0x1BE);
                for(int p=0; p<4; p++) {
                    if(pt[p].type != 0) {
                        ahci_read(active_ahci_ports[i], pt[p].lba_start + 1, 1, ahci_bounce_buffer);
                        if(strncmp((char*)ahci_bounce_buffer, FS_MAGIC, 8) == 0) { boot_drive_idx = i; boot_is_ahci = true; fs_lba_offset = pt[p].lba_start; goto load_fs; }
                    }
                }
            }
        }
    }

    return false;

load_fs:
    os_printf("Loaded OS Persistent Filesystem from %s Drive %d, LBA %lld.\n", boot_is_ahci ? "AHCI" : "IDE", boot_drive_idx, (long long)fs_lba_offset);
    uint8_t* ptr = (uint8_t*)inodes;
    for(int s=0; s<34; s++) ata_read_sector(2+s, ptr + s*512); 
    return true;
}

void fs_init() {
    if(!try_load_os()) {
        os_printf("No installed filesystem detected. Booting into Live RAM Disk Mode...\n");
        boot_drive_idx = -1; fs_lba_offset = 0;
        sys_format_os();
    }
}

char* fs_read(const char* path) {
    int id = fs_resolve(path); if (id == -1 || inodes[id].is_dir) return NULL;
    char* buf = malloc(4096 + 1); for(int i=0; i<8; i++) ata_read_sector(DATA_START_SECTOR + (id * 8) + i, (uint8_t*)buf + i*512);
    buf[inodes[id].size] = '\0'; return buf;
}
void fs_write(const char* path, const char* content) {
    int id = fs_resolve(path);
    if (id == -1) {
        char pcopy[256]; strcpy(pcopy, path); char* last_slash = strrchr(pcopy, '/');
        char fname[64]; int parent_id = current_dir_id;
        if (last_slash) { *last_slash = '\0'; strcpy(fname, last_slash + 1); parent_id = fs_resolve(pcopy[0] == '\0' ? "/" : pcopy); }
        else strcpy(fname, pcopy);
        if (parent_id == -1) return;
        for(int i=0; i<MAX_NODES; i++) {
            if(!inodes[i].used) { id = i; inodes[i].used = 1; inodes[i].is_dir = 0; inodes[i].parent = parent_id; strncpy(inodes[i].name, fname, 59); break; }
        }
    }
    if (id == -1) return;
    inodes[id].size = strlen(content); if(inodes[id].size > 4096) inodes[id].size = 4096;
    char buf[4096] = {0}; memcpy(buf, content, inodes[id].size);
    for(int i=0; i<8; i++) ata_write_sector(DATA_START_SECTOR + (id * 8) + i, (uint8_t*)buf + i*512);
    fs_flush_nodes();
}
void fs_append(const char* path, const char* content) { char* old = fs_read(path); if(old) { char new_cnt[4096] = {0}; strcpy(new_cnt, old); strcat(new_cnt, content); fs_write(path, new_cnt); } }
int fs_delete(const char* path) { int id = fs_resolve(path); if(id != -1 && !inodes[id].is_dir) { inodes[id].used = 0; fs_flush_nodes(); return 0; } return -1; }
void fs_mkdir(const char* path) {
    if(fs_resolve(path) != -1) return; char pcopy[256]; strcpy(pcopy, path); char* last_slash = strrchr(pcopy, '/'); char fname[64]; int parent_id = current_dir_id;
    if (last_slash) { *last_slash = '\0'; strcpy(fname, last_slash + 1); parent_id = fs_resolve(pcopy[0] == '\0' ? "/" : pcopy); } else strcpy(fname, pcopy);
    if (parent_id == -1) return;
    for(int i=0; i<MAX_NODES; i++) { if(!inodes[i].used) { inodes[i].used = 1; inodes[i].is_dir = 1; inodes[i].parent = parent_id; inodes[i].size = 0; strncpy(inodes[i].name, fname, 59); fs_flush_nodes(); return; } }
}
void fs_rmdir(const char* path) {
    int id = fs_resolve(path); if(id != -1 && id != 0 && inodes[id].is_dir) { inodes[id].used = 0; for(int i=0; i<MAX_NODES; i++) if(inodes[i].used && inodes[i].parent == id) inodes[i].used = 0; fs_flush_nodes(); }
}
void fs_cd(const char* path) {
    if(!path || strlen(path) == 0) return; int target = fs_resolve(path);
    if(target != -1 && inodes[target].is_dir) { current_dir_id = target; fs_rebuild_path(current_dir_id, current_dir_path); } else os_printf("Invalid directory.\n");
}
void fs_ls(const char* path) {
    int target = fs_resolve((path && strlen(path) > 0) ? path : ".");
    if(target == -1 || !inodes[target].is_dir) { os_printf("Invalid directory.\n"); return; }
    for(int i=0; i<MAX_NODES; i++) { if(inodes[i].used && inodes[i].parent == target) os_printf("  %s %s\n", inodes[i].is_dir ? "[DIR]" : "[FILE]", inodes[i].name); }
}

// --- MAIN OS ENTRY ---
jmp_buf easec_env;

void kernel_main(void) {
    vga_clear();
    fs_init();
    
    os_printf("\nWelcome to inpsos powered by Easec OS Shell!\n");
    os_printf("Try commands: list, change_directory, install, demo\n");

    char line[256];
    while (1) {
        os_printf("\nroot@inpsos:%s> ", current_dir_path);
        os_getline(line, 256);
        if (strlen(line) == 0) continue;
        
        char cmd[64] = {0}; char arg[192] = {0};
        int i=0, j=0;
        while(line[i] && line[i] != ' ') cmd[j++] = line[i++];
        if (line[i] == ' ') {
            i++; while(line[i] == ' ') i++;
            if (line[i] == '"') { i++; int k = 0; while(line[i] && line[i] != '"') arg[k++] = line[i++]; }
            else { int k = 0; while(line[i]) arg[k++] = line[i++]; }
        }
        
        char path[128]; os_sprintf(path, "/inpsos/easec/%s.easec", cmd);
        char* code = fs_read(path);
        if (code) { if (setjmp(easec_env) == 0) run_easec(code, arg); else os_printf("Easec Error.\n"); }
        else os_printf("Error: Command not found: %s\n", cmd);
    }
}