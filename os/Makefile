CC = gcc
AS = nasm
LD = ld

CFLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -I. -nostdlib -fno-builtin -fno-stack-protector -DBAREMETAL
LDFLAGS = -m32 -T linker.ld -nostdlib

OBJS = boot.o kernel.o easec.o

all: inpsos.bin

boot.o: boot.asm
	$(AS) -f elf32 boot.asm -o boot.o

kernel.o: kernel.c
	$(CC) $(CFLAGS) -c kernel.c -o kernel.o

easec.o: easec.c
	$(CC) $(CFLAGS) -c easec.c -o easec.o

inpsos.bin: $(OBJS)
	$(LD) $(LDFLAGS) -o inpsos.bin $(OBJS)

clean:
	rm -f *.o inpsos.bin