CC = gcc
AS = gcc
LD = ld

CFLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-pie -Wno-misleading-indentation -Wno-unused-parameter -Wno-unused-variable
ASFLAGS = -m32
LDFLAGS = -m32 -T linker.ld -nostdlib -no-pie

OBJS = entry.o kernel.o easec.o

all: inpsos.img

scripts.h:
	@echo "Bundling easec scripts..."
	@python3 -c 'import os, json; out = open("scripts.h", "w"); out.write("void load_easec_scripts() {\n"); [out.write("    fs_write(\"/inpsos/easec/" + f + "\", " + json.dumps(open("easec/" + f).read()) + ");\n") for f in os.listdir("easec") if f.endswith(".easec")] if os.path.exists("easec") else []; out.write("}\n"); out.close()'

bootloader.bin: bootloader.s
	$(CC) -m32 -c bootloader.s -o bootloader.o
	$(LD) -m elf_i386 -Ttext 0x7C00 --oformat binary bootloader.o -o bootloader.bin

entry.o: entry.s
	$(AS) $(ASFLAGS) -c $< -o $@

kernel.o: kernel.c scripts.h
	$(CC) $(CFLAGS) -c $< -o $@

easec.o: easec.c
	$(CC) $(CFLAGS) -c $< -o $@

kernel.bin: $(OBJS)
	$(CC) $(LDFLAGS) -o kernel.elf $(OBJS) -lgcc
	objcopy -O binary kernel.elf kernel.bin

inpsos.img: bootloader.bin kernel.bin
	cat bootloader.bin kernel.bin > inpsos.img

clean:
	rm -rf *.o *.elf *.bin *.img scripts.h