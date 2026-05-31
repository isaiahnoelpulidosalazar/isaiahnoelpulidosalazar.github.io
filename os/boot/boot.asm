[org 0x7C00]
[bits 16]

jmp start

; MBR Partition Table and Signature spacing
times 90-($-$$) db 0 

start:
    cli
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00
    sti

    ; Save boot drive number
    mov [boot_drive], dl

    ; Print greeting
    mov si, msg_loading
    call print_string

    ; Check BIOS LBA extensions availability
    mov ah, 0x41
    mov bx, 0x55AA
    mov dl, [boot_drive]
    int 0x13
    jc lba_not_supported

    ; Load Kernel via LBA Read Packet
    mov ah, 0x42
    mov dl, [boot_drive]
    mov si, disk_packet
    int 0x13
    jc disk_error

    ; Transition to 32-bit Protected Mode
    cli
    lgdt [gdt_descriptor]
    mov eax, cr0
    or eax, 0x1
    mov cr0, eax
    jmp CODE_SEG:init_pm

lba_not_supported:
    mov si, msg_no_lba
    call print_string
    jmp hang

disk_error:
    mov si, msg_disk_err
    call print_string
    jmp hang

hang:
    cli
    hlt
    jmp hang

print_string:
    lodsb
    or al, al
    jz .done
    mov ah, 0x0E
    int 0x10
    jmp print_string
.done:
    ret

; LBA Packet Structure for AH=42h
align 4
disk_packet:
    db 0x10         ; Packet size (16 bytes)
    db 0            ; Reserved
    dw 64           ; Number of sectors to read (32KB kernel space)
    dw 0x0000       ; Destination Offset
    dw 0x1000       ; Destination Segment (0x10000 physical)
    dq 1            ; Start LBA Sector (Sector 1, directly after MBR)

boot_drive: db 0
msg_loading: db "Loading inpsos...", 0x0D, 0x0A, 0
msg_no_lba:  db "Error: LBA extensions not supported.", 0
msg_disk_err: db "Error: Disk read failure.", 0

; Global Descriptor Table (GDT)
gdt_start:
gdt_null:
    dd 0x0
    dd 0x0
gdt_code:
    dw 0xFFFF
    dw 0x0
    db 0x0
    db 10011010b
    db 11001111b
    db 0x0
gdt_data:
    dw 0xFFFF
    dw 0x0
    db 0x0
    db 10010010b
    db 11001111b
    db 0x0
gdt_end:

gdt_descriptor:
    dw gdt_end - gdt_start - 1
    dd gdt_start

CODE_SEG equ gdt_code - gdt_start
DATA_SEG equ gdt_data - gdt_start

[bits 32]
init_pm:
    mov ax, DATA_SEG
    mov ds, ax
    mov es, ax
    mov fs, ax
    mov gs, ax
    mov ss, ax
    mov ebp, 0x90000
    mov esp, ebp

    ; Jump directly to the loaded C++ kernel entry point
    jmp 0x10000

times 510-($-$$) db 0
dw 0xAA55