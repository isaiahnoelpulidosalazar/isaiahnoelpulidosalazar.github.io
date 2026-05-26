.section .entry
.code32
.global _start

_start:
    mov $0x10, %ax
    mov %ax, %ds
    mov %ax, %es
    mov %ax, %fs
    mov %ax, %gs
    mov %ax, %ss

    mov $stack_top, %esp

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