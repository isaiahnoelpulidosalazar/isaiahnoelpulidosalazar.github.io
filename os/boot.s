.section .multiboot
.align 8
multiboot2_header:
    .long 0xE85250D6
    .long 0
    .long header_end - multiboot2_header
    .long -(0xE85250D6 + 0 + (header_end - multiboot2_header))

    .align 8
    .short 0
    .short 0
    .long 8
header_end:

.section .bss
.align 16
stack_bottom:
.skip 16384 # 16 KiB
stack_top:

.section .text
.global _start
.type _start, @function
_start:
    mov $stack_top, %esp

    call kernel_main

    cli
1:  hlt
    jmp 1b
