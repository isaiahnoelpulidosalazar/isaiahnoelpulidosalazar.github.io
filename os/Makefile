CC = gcc
AS = nasm
LD = gcc

CFLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -I. -nostdlib -fno-builtin -fno-stack-protector -fno-pie -DBAREMETAL

LDFLAGS = -m32 -ffreestanding -O2 -nostdlib -T linker.ld -no-pie -z noexecstack

OBJS = boot.o easec.o kernel.o

all: inpsos.bin

boot.o: boot.asm
	$(AS) -f elf32 boot.asm -o boot.o

kernel.o: kernel.c
	$(CC) $(CFLAGS) -c kernel.c -o kernel.o

easec.o: easec.c
	$(CC) $(CFLAGS) -c easec.c -o easec.o

inpsos.bin: $(OBJS)
	$(LD) $(LDFLAGS) -o inpsos.bin $(OBJS) -lgcc

clean:
	rm -f *.o inpsos.bin