CC = gcc
AS = nasm
LD = ld

CFLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -I. -nostdlib -fno-builtin -fno-stack-protector -fno-pie -DBAREMETAL

LDFLAGS = -m elf_i386 -T linker.ld -nostdlib -n

LIBGCC := $(shell $(CC) $(CFLAGS) -print-libgcc-file-name)

OBJS = boot.o easec.o kernel.o

all: inpsos.bin

boot.o: boot.asm
	$(AS) -f elf32 boot.asm -o boot.o

kernel.o: kernel.c
	$(CC) $(CFLAGS) -c kernel.c -o kernel.o

easec.o: easec.c
	$(CC) $(CFLAGS) -c easec.c -o easec.o

inpsos.bin: $(OBJS)
	$(LD) $(LDFLAGS) -o inpsos.bin $(OBJS) $(LIBGCC)

clean:
	rm -f *.o inpsos.bin