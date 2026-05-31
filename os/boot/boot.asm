[bits 16]
[org 0x7c00]

KERNEL_OFFSET equ 0x1000  ; Segment where kernel is loaded (0x10000 physical)

start:
    ; Reset segments
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7c00

    mov si, MSG_BOOTING
    call print_string

    ; Save boot drive index
    mov [boot_drive], dl

    ; Load kernel from physical disk
    call load_kernel

    ; Switch to Protected Mode
    cli
    lgdt [gdt_descriptor]
    mov eax, cr0
    or eax, 0x1
    mov cr0, eax
    jmp CODE_SEG:init_pm

[bits 16]
print_string:
    pusha
.loop:
    lodsb
    or al, al
    jz .done
    mov ah, 0x0e
    int 0x10
    jmp .loop
.done:
    popa
    ret

load_kernel:
    ; Setup BIOS Extended Read (INT 13h, AH=42h) Disk Address Packet (DAP)
    mov [dap_segment], word KERNEL_OFFSET
    mov [dap_offset], word 0x0000
    mov [dap_sector_low], dword 1 ; Start right after boot sector
    mov [dap_count], word 127     ; Load 127 sectors (enough for the kernel)

    mov ah, 0x42
    mov dl, [boot_drive]
    mov si, dap
    int 0x13
    jc .error
    ret
.error:
    mov si, MSG_ERR_DISK
    call print_string
    hlt

boot_drive: db 0x80

; Disk Address Packet Structure
align 4
dap:
    db 0x10
    db 0
dap_count:
    dw 0
dap_offset:
    dw 0
dap_segment:
    dw 0
dap_sector_low:
    dd 0
dap_sector_high:
    dd 0

; Global Descriptor Table
gdt_start:
    dd 0x0
    dd 0x0
gdt_code:
    dw 0xffff
    dw 0x0
    db 0x0
    db 10011010b
    db 11001111b
    db 0x0
gdt_data:
    dw 0xffff
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

    ; Jump straight into our physical kernel entry
    jmp 0x10000

MSG_BOOTING: db "Loading inpsos bootloader...", 13, 10, 0
MSG_ERR_DISK: db "Disk read error!", 13, 10, 0

times 510-($-$$) db 0
dw 0xaa55