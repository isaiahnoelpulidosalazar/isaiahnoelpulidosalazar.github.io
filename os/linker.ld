/* The bootloader jumps exactly to the 64KB mark (0x10000) */
ENTRY(_start)

SECTIONS
{
    . = 0x10000;

    .text : ALIGN(4K)
    {
        entry.o(.entry) 
        *(.text)
    }

    .rodata : ALIGN(4K)
    {
        *(.rodata)
    }

    .data : ALIGN(4K)
    {
        *(.data)
    }

    .bss : ALIGN(4K)
    {
        *(COMMON)
        *(.bss)
        PROVIDE(end = .);
    }
    
    /DISCARD/ : {
        *(.note.gnu.build-id)
        *(.comment)
    }
}