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

    /* 2. Check if the BIOS supports modern LBA Disk Reads */
    mov $0x41, %ah
    mov $0x55AA, %bx
    int $0x13
    jc disk_error         /* If LBA is not supported, halt the system */

    /* 3. Read the Kernel using LBA (Int 13h, AH=42h) */
    mov $0x42, %ah
    mov $dap, %si         /* Point the BIOS to our Disk Address Packet */
    mov boot_drive, %dl   /* Boot drive */
    int $0x13
    jc disk_error         /* If the read fails, halt the system */

    /* 4. Switch to 32-bit Protected Mode */
    cli                   /* Disable interrupts permanently */
    lgdt gdt_descriptor   /* Load the Global Descriptor Table */

    mov %cr0, %eax
    or $1, %eax           /* Set the Protection Enable (PE) bit */
    mov %eax, %cr0

    /* Far jump to 32-bit code segment! 
       Notice the 'l' in 'ljmpl' to tell the compiler we are using a 32-bit offset! */
    ljmpl $0x08, $0x10000

disk_error:
    hlt
    jmp disk_error

boot_drive:
    .byte 0

/* --- Disk Address Packet (DAP) --- */
/* This tells the BIOS exactly what to read and where to put it */
.align 4
dap:
    .byte 0x10            /* Size of this DAP structure (16 bytes) */
    .byte 0               /* Unused */
    .short 127            /* Read 127 sectors (~63.5 KB of kernel code) */
    .short 0x0000         /* Memory Offset to load into */
    .short 0x1000         /* Memory Segment (0x1000:0x0000 = Physical 0x10000) */
    .quad 1               /* Start reading at LBA 1 (Sector 2 on the disk) */

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

/* Boot sector magic signature (Required by BIOS to make the disk bootable) */
.org 510
.word 0xAA55