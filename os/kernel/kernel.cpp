#include <stdint.h>
#include <stddef.h>

#define VGA_ADDRESS 0xB8000
#define SCREEN_WIDTH 80
#define SCREEN_HEIGHT 25

extern "C" void kernel_main();
extern "C" void init_ctype_table(); // Added local pointer linkage

// Simple VGA Text Mode Driver
int cursor_x = 0;
int cursor_y = 0;
uint16_t* vga_buffer = (uint16_t*)VGA_ADDRESS;

void clear_screen() {
    for (int i = 0; i < SCREEN_WIDTH * SCREEN_HEIGHT; i++) {
        vga_buffer[i] = (uint16_t)' ' | (0x07 << 8);
    }
    cursor_x = 0;
    cursor_y = 0;
}

void kprint_char(char c) {
    if (c == '\n') {
        cursor_x = 0;
        cursor_y++;
    } else if (c == '\r') {
        cursor_x = 0;
    } else {
        int index = cursor_y * SCREEN_WIDTH + cursor_x;
        vga_buffer[index] = (uint16_t)c | (0x0F << 8);
        cursor_x++;
    }

    if (cursor_x >= SCREEN_WIDTH) {
        cursor_x = 0;
        cursor_y++;
    }

    if (cursor_y >= SCREEN_HEIGHT) {
        for (int y = 1; y < SCREEN_HEIGHT; y++) {
            for (int x = 0; x < SCREEN_WIDTH; x++) {
                vga_buffer[(y - 1) * SCREEN_WIDTH + x] = vga_buffer[y * SCREEN_WIDTH + x];
            }
        }
        for (int x = 0; x < SCREEN_WIDTH; x++) {
            vga_buffer[(SCREEN_HEIGHT - 1) * SCREEN_WIDTH + x] = (uint16_t)' ' | (0x07 << 8);
        }
        cursor_y = SCREEN_HEIGHT - 1;
    }
}

void kprint(const char* str) {
    while (*str) {
        kprint_char(*str++);
    }
}

void outb(uint16_t port, uint8_t val) {
    asm volatile ("outb %0, %1" : : "a"(val), "Nd"(port));
}

uint8_t inb(uint16_t port) {
    uint8_t ret;
    asm volatile ("inb %1, %0" : "=a"(ret) : "Nd"(port));
    return ret;
}

uint32_t inl(uint16_t port) {
    uint32_t ret;
    asm volatile ("inl %1, %0" : "=a"(ret) : "Nd"(port));
    return ret;
}

void outl(uint16_t port, uint32_t val) {
    asm volatile ("outl %0, %1" : : "a"(val), "Nd"(port));
}

uintptr_t free_memory_start = 0x500000;

void* kmalloc(size_t size) {
    size = (size + 7) & ~7;
    void* allocated = (void*)free_memory_start;
    free_memory_start += size;
    return allocated;
}

void kfree(void* ptr) {
    (void)ptr;
}

void* krealloc(void* ptr, size_t new_size) {
    if (!ptr) return kmalloc(new_size);
    void* new_ptr = kmalloc(new_size);
    for (size_t i = 0; i < new_size; i++) {
        ((char*)new_ptr)[i] = ((char*)ptr)[i];
    }
    return new_ptr;
}

char kget_char() {
    static const char scancode_table[] = {
        0,  27, '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=', '\b',
        '\t', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\n',
        0, 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'', '`', 0, '\\',
        'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/', 0, '*', 0, ' '
    };

    while (1) {
        if (inb(0x64) & 1) {
            uint8_t scancode = inb(0x60);
            if (!(scancode & 0x80)) {
                if (scancode < sizeof(scancode_table)) {
                    return scancode_table[scancode];
                }
            }
        }
    }
}

void kget_string(char* buffer, int max_len) {
    int len = 0;
    while (len < max_len - 1) {
        char c = kget_char();
        if (c == '\n') {
            kprint_char('\n');
            break;
        } else if (c == '\b') {
            if (len > 0) {
                len--;
                cursor_x--;
                kprint_char(' ');
                cursor_x--;
            }
        } else if (c >= ' ' && c <= '~') {
            buffer[len++] = c;
            kprint_char(c);
        }
    }
    buffer[len] = '\0';
}

extern void shell_init();

extern "C" void kernel_main() {
    init_ctype_table(); // Init the standard trait array first before booting the shell
    clear_screen();
    kprint("---------------------------------------------------\n");
    kprint("  inpsos kernel successfully loaded in real hardware  \n");
    kprint("---------------------------------------------------\n\n");
    
    shell_init();
}