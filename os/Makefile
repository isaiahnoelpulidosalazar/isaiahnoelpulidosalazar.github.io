CC = gcc
AS = gcc

CFLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-pie
ASFLAGS = -m32
LDFLAGS = -m32 -T linker.ld -ffreestanding -O2 -nostdlib -no-pie

OBJS = boot.o kernel.o

all: inpsos.iso

boot.o: boot.s
	$(AS) $(ASFLAGS) -c $< -o $@

kernel.o: kernel.c
	$(CC) $(CFLAGS) -c $< -o $@

inpsos.bin: $(OBJS)
	$(CC) $(LDFLAGS) -o $@ $(OBJS)

inpsos.iso: inpsos.bin
	# Build the ISO directory structure
	mkdir -p isodir/boot/grub
	cp inpsos.bin isodir/boot/inpsos.bin
	cp grub.cfg isodir/boot/grub/grub.cfg
	# Generate the bootable ISO
	grub-mkrescue -o inpsos.iso isodir

clean:
	rm -rf *.o inpsos.bin inpsos.iso isodir