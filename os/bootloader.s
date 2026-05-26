.code16
.global _start

_start:
    cli
    xor %ax, %ax
    mov %ax, %ds
    mov %ax, %es
    mov %ax, %ss
    mov $0x7C00, %sp
    sti

    mov %dl, boot_drive

    mov $250, %cx
load_loop:
    push %cx

    mov $0x42, %ah
    mov $dap, %si
    mov boot_drive, %dl
    int $0x13
    jc disk_error

    mov dap_lba, %eax
    inc %eax
    mov %eax, dap_lba

    mov dap_seg, %ax
    add $0x0020, %ax
    mov %ax, dap_seg

    pop %cx
    loop load_loop

    cli
    lgdt gdt_descriptor
    mov %cr0, %eax
    or $1, %eax
    mov %eax, %cr0

    ljmpl $0x08, $0x10000

disk_error:
    hlt
    jmp disk_error

boot_drive:
    .byte 0

.align 4
dap:
    .byte 0x10
    .byte 0
    .short 1
    .short 0x0000
dap_seg:
    .short 0x1000
dap_lba:
    .long 1
    .long 0

.align 8
gdt_start:
    .quad 0x0000000000000000 
gdt_code:
    .quad 0x00CF9A000000FFFF 
gdt_data:
    .quad 0x00CF92000000FFFF 
gdt_end:

gdt_descriptor:
    .short gdt_end - gdt_start - 1
    .long gdt_start

.org 510
.word 0xAA55