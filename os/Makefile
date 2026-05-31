CC = gcc -m32
CXX = g++ -m32
AS = nasm

CFLAGS = -ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-rtti
LDFLAGS = -T linker.ld -nostdlib

BOOT_BIN = boot/boot.bin
KERNEL_BIN = kernel.bin
OS_IMAGE = inpsos.img
ISO_IMAGE = inpsos.iso

OBJ = kernel/kernel.o \
      drivers/ahci.o \
      fs/simple_fs.o \
      easec/libc_shim.o \
      shell/shell.o \
      easec/easec.o

all: $(ISO_IMAGE)

$(BOOT_BIN): boot/boot.asm
	$(AS) -f bin $< -o $@

%.o: %.cpp
	$(CXX) $(CFLAGS) -c $< -o $@

easec/easec.o: easec/easec.c
	$(CC) $(CFLAGS) -c $< -o $@

$(KERNEL_BIN): $(OBJ)
	ld -m elf_i386 $(LDFLAGS) $(OBJ) -o $@

$(ISO_IMAGE): $(BOOT_BIN) $(KERNEL_BIN)
	# Compile floppy disk container mapping to fit physical standard size limits
	dd if=/dev/zero of=$(OS_IMAGE) bs=512 count=2880
	dd if=$(BOOT_BIN) of=$(OS_IMAGE) conv=notrunc bs=512 count=1
	dd if=$(KERNEL_BIN) of=$(OS_IMAGE) conv=notrunc bs=512 seek=1
	
	# Pack the filesystem image structure into bootable El-Torito CD ISO payload
	mkdir -p iso_stage
	cp $(OS_IMAGE) iso_stage/
	xorriso -as mkisofs -b $(OS_IMAGE) -no-emul-toc -boot-load-size 4 -o $(ISO_IMAGE) iso_stage/

clean:
	rm -rf $(OBJ) $(BOOT_BIN) $(KERNEL_BIN) $(OS_IMAGE) $(ISO_IMAGE) iso_stage