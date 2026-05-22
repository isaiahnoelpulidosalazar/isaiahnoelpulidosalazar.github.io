CC = gcc
AS = gcc

CFLAGS = -m32 -ffreestanding -O2 -Wall -Wextra -fno-exceptions -fno-pie -Wno-misleading-indentation -Wno-unused-parameter -Wno-unused-variable
ASFLAGS = -m32
LDFLAGS = -m32 -T linker.ld -ffreestanding -O2 -nostdlib -no-pie

OBJS = boot.o kernel.o easec.o

all: inpsos.iso

scripts.h:
	@echo "Bundling easec scripts..."
	@python3 -c 'import os, json; out = open("scripts.h", "w"); out.write("void load_easec_scripts() {\\n"); [out.write(f"    fs_write(\"/inpsos/easec/{f}\", {json.dumps(open(os.path.join(\"easec\", f)).read())});\\n") for f in os.listdir("easec") if f.endswith(".easec")] if os.path.exists("easec") else None; out.write("}\\n"); out.close()'

boot.o: boot.s
	$(AS) $(ASFLAGS) -c $< -o $@

kernel.o: kernel.c scripts.h
	$(CC) $(CFLAGS) -c $< -o $@

easec.o: easec.c
	$(CC) $(CFLAGS) -c $< -o $@

inpsos.bin: $(OBJS)
	$(CC) $(LDFLAGS) -o $@ $(OBJS) -lgcc

inpsos.iso: inpsos.bin
	mkdir -p isodir/boot/grub
	cp inpsos.bin isodir/boot/inpsos.bin
	cp grub.cfg isodir/boot/grub/grub.cfg
	grub-mkrescue -o inpsos.iso isodir

clean:
	rm -rf *.o inpsos.bin inpsos.iso isodir scripts.h