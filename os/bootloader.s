.code16
.global _start

_start:
    /* 1. Set up 16-bit segment registers and stack */
    cli
    xor %ax, %ax
    mov %ax, %ds
    mov %ax, %es
    mov %ax, %ss
    mov $0x7C00, %sp
    sti

    /* Save the boot drive number provided by the BIOS */
    mov %dl, boot_drive

    /* 2. Read the Kernel from the Disk into Memory */
    mov $0x02, %ah        /* BIOS Function: Read Sectors */
    mov $64, %al          /* Read 64 sectors (32 KB of kernel code) */
    mov $0x00, %ch        /* Cylinder 0 */
    mov $0x02, %cl        /* Sector 2 (Sector 1 is this bootloader!) */
    mov $0x00, %dh        /* Head 0 */
    mov boot_drive, %dl   /* Boot drive */

    /* Load kernel into RAM at 0x1000:0x0000 (Physical address 0x10000) */
    mov $0x1000, %bx
    mov %bx, %es
    mov $0x0000, %bx
    int $0x13
    jc disk_error         /* If the read fails, halt the system */

    /* 3. Switch to 32-bit Protected Mode */
    cli                   /* Disable interrupts permanently */
    lgdt gdt_descriptor   /* Load the Global Descriptor Table */

    mov %cr0, %eax
    or $1, %eax           /* Set the Protection Enable (PE) bit */
    mov %eax, %cr0

    /* Far jump to 32-bit code segment to flush the CPU pipeline */
    ljmp $0x08, $0x10000

disk_error:
    hlt
    jmp disk_error

boot_drive:
    .byte 0

/* --- Global Descriptor Table (GDT) --- */
.align 8
gdt_start:
    .quad 0x0000000000000000 /* 0x00: Null Descriptor */
gdt_code:
    .quad 0x00CF9A000000FFFF /* 0x08: 32-bit Code Segment (Executable/Readable) */
gdt_data:
    .quad 0x00CF92000000FFFF /* 0x10: 32-bit Data Segment (Readable/Writable) */
gdt_end:

gdt_descriptor:
    .short gdt_end - gdt_start - 1
    .long gdt_start

/* Boot sector magic signature (Required by BIOS) */
.org 510
.word 0xAA55