[org 0x7C00]
[bits 16]

; Standard 3-byte boot jump sequence to appease legacy BIOS checks
jmp short start
nop

start:
    cld                 ; Clear Direction Flag (Critical: guarantees lodsb moves forward)
    cli                 ; Disable interrupts during segment setups
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00      ; Set stack pointer directly below the boot sector
    sti                 ; Restore interrupts

    ; Save the boot drive index passed by the BIOS in DL
    mov [boot_drive], dl

    ; Display greeting
    mov si, msg_loading
    call print_string

    ; Check if modern BIOS LBA extensions are available
    mov ah, 0x41
    mov bx, 0x55AA
    mov dl, [boot_drive]
    int 0x13
    jc lba_fallback      ; Fall back to CHS if LBA is not supported

    ; Attempt LBA Load
    mov ah, 0x42
    mov dl, [boot_drive]
    mov si, disk_packet
    int 0x13
    jc lba_fallback      ; Fall back to CHS if LBA read fails

    jmp transition_pm

lba_fallback:
    ; CHS fallback loader for El Torito Floppy Emulation (e.g. ISO boot)
    mov cx, 256         ; Load 256 sectors (128KB, fully covers the 97KB+ kernel)
    mov ax, 1           ; Start reading from LBA sector 1 (directly after MBR)
    mov dx, 0x1000      ; Destination Segment
    mov es, dx
    xor bx, bx          ; Destination ES:BX = 0x1000:0000 (0x10000 physical address)

read_loop:
    push cx
    push ax
    push bx

    ; Convert LBA (AX) to CHS geometry for a standard 1.44MB Floppy (18 SPT, 2 Heads)
    xor dx, dx
    mov cx, 36          ; 18 sectors/track * 2 heads
    div cx              ; AX = Cylinder (LBA / 36), DX = Remainder (LBA % 36)
    mov ch, al          ; CH = Cylinder
    
    mov ax, dx          ; AX = Remainder
    mov cl, 18
    div cl              ; AL = Head (Remainder / 18), AH = Sector - 1 (Remainder % 18)
    mov dh, al          ; DH = Head
    mov cl, ah
    inc cl              ; CL = Sector (1-based index)
    
    mov dl, [boot_drive]

    ; Issue BIOS Read Sector Command
    mov ax, 0x0201      ; AH = 02h (Read), AL = 01 (1 sector)
    int 0x13
    jc disk_error_chs

    pop bx
    pop ax
    pop cx

    ; Safely advance segment by 512 bytes (32 paragraphs) to bypass 16-bit offset bounds
    mov dx, es
    add dx, 32
    mov es, dx

    inc ax              ; Next sequential LBA sector
    loop read_loop
    jmp transition_pm

disk_error_chs:
    mov si, msg_disk_err
    call print_string
    jmp hang

transition_pm:
    ; Transition to 32-bit Protected Mode
    cli
    lgdt [gdt_descriptor]
    mov eax, cr0
    or eax, 0x1
    mov cr0, eax
    jmp CODE_SEG:init_pm

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

; LBA Packet Structure for AH=42h (SATA/HDD boots)
align 4
disk_packet:
    db 0x10         ; Packet size (16 bytes)
    db 0            ; Reserved
    dw 256          ; Read 256 sectors (128KB)
    dw 0x0000       ; Offset
    dw 0x1000       ; Segment
    dq 1            ; Start LBA Sector 1

boot_drive: db 0
msg_loading: db "Loading inpsos...", 0x0D, 0x0A, 0
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