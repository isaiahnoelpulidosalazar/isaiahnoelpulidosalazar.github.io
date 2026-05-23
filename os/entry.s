.code32
.global _start

_start:
    /* Update segment registers to point to our new 32-bit GDT Data Segment */
    mov $0x10, %ax
    mov %ax, %ds
    mov %ax, %es
    mov %ax, %fs
    mov %ax, %gs
    mov %ax, %ss

    /* Set up the C stack */
    mov $stack_top, %esp

    /* Call your C kernel! */
    call kernel_main

hang:
    cli
    hlt
    jmp hang

.section .bss
.align 16
stack_bottom:
.skip 16384 # 16 KiB
stack_top: