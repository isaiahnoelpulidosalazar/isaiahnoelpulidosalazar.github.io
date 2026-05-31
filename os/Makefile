CC = gcc
CXX = g++
AS = nasm

# Shared freestanding flags to disable hosted dependencies and stack protection wrappers
COMMON_FLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -fno-stack-protector -fno-pie -fno-stack-clash-protection -U_FORTIFY_SOURCE -D_FORTIFY_SOURCE=0
CFLAGS = $(COMMON_FLAGS)
CXXFLAGS = $(COMMON_FLAGS) -fno-exceptions -fno-rtti
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

.PHONY: all clean iso

all: $(ISO_IMAGE)

iso: $(ISO_IMAGE)

$(BOOT_BIN): boot/boot.asm
	$(AS) -f bin $< -o $@

kernel/kernel.o: kernel/kernel.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

drivers/ahci.o: drivers/ahci.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

fs/simple_fs.o: fs/simple_fs.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

easec/libc_shim.o: easec/libc_shim.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

shell/shell.o: shell/shell.cpp
	$(CXX) $(CXXFLAGS) -c $< -o $@

easec/easec.o: easec/easec.c
	$(CC) $(CFLAGS) -c $< -o $@

$(KERNEL_BIN): $(OBJ)
	# Link via CXX with libgcc resolved automatically
	$(CXX) -m32 $(LDFLAGS) $(OBJ) -lgcc -o $@

$(ISO_IMAGE): $(BOOT_BIN) $(KERNEL_BIN)
	dd if=/dev/zero of=$(OS_IMAGE) bs=512 count=2880
	dd if=$(BOOT_BIN) of=$(OS_IMAGE) conv=notrunc bs=512 count=1
	dd if=$(KERNEL_BIN) of=$(OS_IMAGE) conv=notrunc bs=512 seek=1
	
	mkdir -p iso_stage
	cp $(OS_IMAGE) iso_stage/
	# Corrected: removed the invalid '-no-emul-toc' option
	xorriso -as mkisofs -b $(OS_IMAGE) -o $(ISO_IMAGE) iso_stage/

clean:
	rm -rf $(OBJ) $(BOOT_BIN) $(KERNEL_BIN) $(OS_IMAGE) $(ISO_IMAGE) iso_stage