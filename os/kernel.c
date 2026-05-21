#include <stdint.h>
#include <stddef.h>

void kernel_main(void) {
    const char* str = "inpsos booted.";
    
    uint16_t* video_memory = (uint16_t*) 0xB8000;
    
    uint16_t color = 0x07 << 8;

    for (size_t i = 0; str[i] != '\0'; i++) {
        video_memory[i] = str[i] | color;
    }
}