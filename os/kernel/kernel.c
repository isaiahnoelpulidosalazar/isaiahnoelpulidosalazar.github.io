#include "libc.h"
#include "ahci.h"

extern void init_allocator();
extern void init_ahci();
extern void init_vm();
extern void* create_env(void* parent);
extern void run_script(const char* source, void* env);

extern HBA_Port* active_ports[32];
extern int active_port_count;

#define DIRECTORY_SECTOR 129
struct InpsFileEntry dir_cache[10];
bool is_installed_mode = false;

// VGA Console Drivers
#define VGA_WIDTH  80
#define VGA_HEIGHT 25
#define VGA_MEM    ((volatile uint16_t*)0xB8000)

static int cursor_x = 0;
static int cursor_y = 0;
static uint8_t term_color = 0x07;

void clear_screen() {
    for (int i = 0; i < VGA_WIDTH * VGA_HEIGHT; i++) {
        VGA_MEM[i] = (term_color << 8) | ' ';
    }
    cursor_x = 0; cursor_y = 0;
}

static inline void outb(uint16_t port, uint8_t val) {
    __asm__ volatile ("outb %0, %1" : : "a"(val), "Nd"(port));
}

static inline uint8_t inb(uint16_t port) {
    uint8_t ret;
    __asm__ volatile ("inb %1, %0" : "=a"(ret) : "Nd"(port));
    return ret;
}

void update_cursor() {
    uint16_t pos = cursor_y * VGA_WIDTH + cursor_x;
    outb(0x3D4, 0x0F); outb(0x3D5, (uint8_t)(pos & 0xFF));
    outb(0x3D4, 0x0E); outb(0x3D5, (uint8_t)((pos >> 8) & 0xFF));
}

void scroll() {
    if (cursor_y >= VGA_HEIGHT) {
        int diff = cursor_y - VGA_HEIGHT + 1;
        for (int i = 0; i < (VGA_HEIGHT - diff) * VGA_WIDTH; i++) {
            VGA_MEM[i] = VGA_MEM[i + diff * VGA_WIDTH];
        }
        for (int i = (VGA_HEIGHT - diff) * VGA_WIDTH; i < VGA_HEIGHT * VGA_WIDTH; i++) {
            VGA_MEM[i] = (term_color << 8) | ' ';
        }
        cursor_y = VGA_HEIGHT - 1;
    }
}

void kputc(char c) {
    if (c == '\n') { cursor_x = 0; cursor_y++; }
    else if (c == '\r') { cursor_x = 0; }
    else if (c == '\b') {
        if (cursor_x > 0) {
            cursor_x--;
            VGA_MEM[cursor_y * VGA_WIDTH + cursor_x] = (term_color << 8) | ' ';
        }
    } else {
        VGA_MEM[cursor_y * VGA_WIDTH + cursor_x] = (term_color << 8) | c;
        cursor_x++;
    }
    if (cursor_x >= VGA_WIDTH) { cursor_x = 0; cursor_y++; }
    scroll();
    update_cursor();
}

void kputs(const char* s) {
    while (*s) kputc(*s++);
}

char kget_char() {
    static const char layout[128] = {
        0, 0, '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', '\b',
        '\t', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\n',
        0, 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'', '`', 0, '\\',
        'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/', 0, '*', 0, ' '
    };
    while (1) {
        while ((inb(0x64) & 1) == 0);
        uint8_t code = inb(0x60);
        if (!(code & 0x80)) {
            char c = layout[code];
            if (c) return c;
        }
    }
}

// InpsFS Filesystem Drivers
void read_directory(HBA_Port* disk) {
    ahci_read(disk, DIRECTORY_SECTOR, 0, 1, (uint16_t*)dir_cache);
}

void write_directory(HBA_Port* disk) {
    ahci_write(disk, DIRECTORY_SECTOR, 0, 1, (uint16_t*)dir_cache);
}

bool create_file(HBA_Port* disk, const char* name, const char* content, uint32_t size) {
    read_directory(disk);
    int slot = -1;
    for (int i = 0; i < 10; i++) {
        if (!dir_cache[i].used) { slot = i; break; }
    }
    if (slot == -1) return false;
    
    uint32_t start_sec = 130;
    for (int i = 0; i < 10; i++) {
        if (dir_cache[i].used) {
            if (dir_cache[i].start_sector + dir_cache[i].num_sectors > start_sec) {
                start_sec = dir_cache[i].start_sector + dir_cache[i].num_sectors;
            }
        }
    }
    
    uint32_t num_secs = (size + 511) / 512;
    if (num_secs == 0) num_secs = 1;
    
    uint8_t* buf = malloc(num_secs * 512);
    memset(buf, 0, num_secs * 512);
    memcpy(buf, content, size);
    
    if (!ahci_write(disk, start_sec, 0, num_secs, (uint16_t*)buf)) {
        free(buf);
        return false;
    }
    free(buf);
    
    strcpy(dir_cache[slot].filename, name);
    dir_cache[slot].start_sector = start_sec;
    dir_cache[slot].num_sectors = num_secs;
    dir_cache[slot].size = size;
    dir_cache[slot].used = 1;
    
    write_directory(disk);
    return true;
}

bool delete_file(HBA_Port* disk, const char* name) {
    read_directory(disk);
    for (int i = 0; i < 10; i++) {
        if (dir_cache[i].used && strcmp(dir_cache[i].filename, name) == 0) {
            dir_cache[i].used = 0;
            write_directory(disk);
            return true;
        }
    }
    return false;
}

char* read_file(HBA_Port* disk, const char* name, uint32_t* out_size) {
    read_directory(disk);
    for (int i = 0; i < 10; i++) {
        if (dir_cache[i].used && strcmp(dir_cache[i].filename, name) == 0) {
            uint32_t num_secs = dir_cache[i].num_sectors;
            uint8_t* buf = malloc(num_secs * 512);
            if (!ahci_read(disk, dir_cache[i].start_sector, 0, num_secs, (uint16_t*)buf)) {
                free(buf); return NULL;
            }
            *out_size = dir_cache[i].size;
            char* out = malloc(dir_cache[i].size + 1);
            memcpy(out, buf, dir_cache[i].size);
            out[dir_cache[i].size] = '\0';
            free(buf);
            return out;
        }
    }
    return NULL;
}

// Easec preinstalled system commands
const char* list_easec_src = 
"note [ list.easec - Lists files ]\n"
"say \"Installed .easec programs on disk:\"\n"
"say \"- list.easec\"\n"
"say \"- create_file.easec\"\n"
"say \"- delete_file.easec\"\n";

const char* create_file_easec_src =
"note [ create_file.easec - Creates a new file ]\n"
"say \"Enter target filename:\"\n"
"var text filename get\n"
"say \"Enter content to write:\"\n"
"var text content get\n"
"file create filename content\n"
"say \"File created successfully.\"\n";

const char* delete_file_easec_src =
"note [ delete_file.easec - Deletes a file ]\n"
"say \"Enter target filename:\"\n"
"var text filename get\n"
"file delete filename\n"
"say \"File deleted successfully.\"\n";

void run_installer() {
    kputs("\nPreparing physical installation disk validation...\n");
    if (active_port_count < 2) {
        kputs("Error: Physical target drive not detected. Set up two active AHCI storage controllers!\n");
        return;
    }
    HBA_Port* src = active_ports[0];
    HBA_Port* dest = active_ports[1];
    
    kputs("SATA physical target detected on Port 1. Installing bootloader & kernel... ");
    uint8_t* kernel_buffer = malloc(64 * 1024);
    for (int s = 0; s < 128; s++) {
        ahci_read(src, s, 0, 1, (uint16_t*)(kernel_buffer + s * 512));
    }
    for (int s = 0; s < 128; s++) {
        ahci_write(dest, s, 0, 1, (uint16_t*)(kernel_buffer + s * 512));
    }
    free(kernel_buffer);
    kputs("Done.\n");
    
    kputs("Initializing disk filesystem blocks... ");
    uint8_t* empty_sector = malloc(512);
    memset(empty_sector, 0, 512);
    ahci_write(dest, DIRECTORY_SECTOR, 0, 1, (uint16_t*)empty_sector);
    free(empty_sector);
    kputs("Done.\n");
    
    kputs("Deploying core system programs (*.easec)... ");
    create_file(dest, "list.easec", list_easec_src, strlen(list_easec_src));
    create_file(dest, "create_file.easec", create_file_easec_src, strlen(create_file_easec_src));
    create_file(dest, "delete_file.easec", delete_file_easec_src, strlen(delete_file_easec_src));
    kputs("Done.\n");
    
    kputs("\nSUCCESS! inpsos installation complete.\n");
    kputs("Eject the installation ISO, restart, and boot from target hard drive.\n");
}

void k_main() {
    clear_screen();
    kputs("==================================================\n");
    kputs("          INPSOS OPERATING SYSTEM                  \n");
    kputs("==================================================\n");
    
    init_allocator();
    init_ahci();
    
    if (active_port_count > 0) {
        read_directory(active_ports[0]);
        if (dir_cache[0].used) {
            is_installed_mode = true;
            active_ports[1] = active_ports[0]; // Map active filesystem partition
        }
    }
    
    if (is_installed_mode) {
        kputs("Booted from primary hard drive. inpsos is active.\n");
        kputs("Type a program name to execute (e.g. list.easec)\n\n");
    } else {
        kputs("Booted from installation ISO.\n");
        kputs("Type 'install' to physically write inpsos to your hard drive.\n\n");
    }
    
    init_vm();
    void* global_env = create_env(NULL);
    char input_buffer[256];
    
    while (1) {
        if (is_installed_mode) kputs("inpsos$ "); else kputs("installer$ ");
        fgets(input_buffer, sizeof(input_buffer), stdin);
        
        int len = strlen(input_buffer);
        while (len > 0 && (input_buffer[len-1] == '\n' || input_buffer[len-1] == '\r')) {
            input_buffer[len-1] = '\0';
            len--;
        }
        if (strlen(input_buffer) == 0) continue;
        
        if (!is_installed_mode) {
            if (strcmp(input_buffer, "install") == 0) {
                run_installer();
            } else {
                kputs("Unsupported command. The ISO environment only supports 'install'.\n");
            }
        } else {
            uint32_t fsize = 0;
            char* script_src = read_file(active_ports[1], input_buffer, &fsize);
            if (script_src) {
                run_script(script_src, global_env);
                free(script_src);
            } else if (strcmp(input_buffer, "help") == 0) {
                kputs("Available programs on storage:\n");
                read_directory(active_ports[1]);
                for (int i = 0; i < 10; i++) {
                    if (dir_cache[i].used) printf("  - %s (%d bytes)\n", dir_cache[i].filename, dir_cache[i].size);
                }
            } else {
                printf("Command '%s' not found. Type 'help' to check program directories.\n", input_buffer);
            }
        }
    }
}