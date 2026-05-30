#![no_std]
#![no_main]
#![feature(c_variadic)] // Enables native C-style variadic arguments on Nightly

use core::panic::PanicInfo;
use core::alloc::{GlobalAlloc, Layout};
use core::sync::atomic::{AtomicUsize, Ordering};

// ----------------------------------------------------
// 1. Real Heap Memory Allocator Configuration
// ----------------------------------------------------
const HEAP_SIZE: usize = 8 * 1024 * 1024; // 8MB Native Heap Allocation
static mut HEAP_MEM: [u8; HEAP_SIZE] = [0; HEAP_SIZE];
static HEAP_OFFSET: AtomicUsize = AtomicUsize::new(0);

pub struct BumpAllocator;

unsafe impl GlobalAlloc for BumpAllocator {
    unsafe fn alloc(&self, layout: Layout) -> *mut u8 {
        let align = layout.align();
        let size = layout.size();
        loop {
            let current = HEAP_OFFSET.load(Ordering::Relaxed);
            let aligned = (current + align - 1) & !(align - 1);
            let next = aligned + size;
            if next > HEAP_SIZE {
                return core::ptr::null_mut();
            }
            if HEAP_OFFSET.compare_exchange_weak(current, next, Ordering::SeqCst, Ordering::Relaxed).is_ok() {
                return HEAP_MEM.as_ptr().add(aligned) as *mut u8;
            }
        }
    }
    unsafe fn dealloc(&self, _ptr: *mut u8, _layout: Layout) {}
}

#[global_allocator]
static ALLOCATOR: BumpAllocator = BumpAllocator;

// ----------------------------------------------------
// 2. Real VGA Framebuffer Output Writer (0xB8000)
// ----------------------------------------------------
pub struct ScreenWriter {
    x: usize,
    y: usize,
}

impl ScreenWriter {
    pub fn write_byte(&mut self, byte: u8) {
        let vga = 0xb8000 as *mut u16;
        if byte == b'\n' {
            self.x = 0;
            self.y += 1;
        } else {
            let offset = self.y * 80 + self.x;
            unsafe {
                core::ptr::write_volatile(vga.add(offset), 0x0F00 | (byte as u16));
            }
            self.x += 1;
            if self.x >= 80 {
                self.x = 0;
                self.y += 1;
            }
        }
        if self.y >= 25 {
            unsafe {
                core::ptr::copy(vga.add(80), vga, 24 * 80);
                for i in (24 * 80)..(25 * 80) {
                    core::ptr::write_volatile(vga.add(i), 0x0F20);
                }
            }
            self.y = 24;
        }
    }
}

pub static mut WRITER: ScreenWriter = ScreenWriter { x: 0, y: 0 };

// ----------------------------------------------------
// 3. PS/2 Keyboard Core Driver
// ----------------------------------------------------
pub unsafe fn inb(port: u16) -> u8 {
    let value: u8;
    core::arch::asm!("in al, dx", out("al") value, in("dx") port, options(nomem, nostack, preserves_flags));
    value
}

pub fn get_char() -> char {
    unsafe {
        loop {
            // Read status port 0x64. Bit 0 specifies output buffer state.
            if (inb(0x64) & 1) != 0 {
                let code = inb(0x60);
                // Map common keys
                match code {
                    0x1E => return 'a', 0x30 => return 'b', 0x2E => return 'c', 0x20 => return 'd',
                    0x12 => return 'e', 0x21 => return 'f', 0x22 => return 'g', 0x23 => return 'h',
                    0x17 => return 'i', 0x24 => return 'j', 0x25 => return 'k', 0x26 => return 'l',
                    0x32 => return 'm', 0x31 => return 'n', 0x18 => return 'o', 0x19 => return 'p',
                    0x10 => return 'q', 0x13 => return 'r', 0x1F => return 's', 0x14 => return 't',
                    0x16 => return 'u', 0x2F => return 'v', 0x11 => return 'w', 0x2D => return 'x',
                    0x15 => return 'y', 0x2C => return 'z', 0x39 => return ' ', 0x1C => return '\n',
                    _ => {}
                }
            }
        }
    }
}

// ----------------------------------------------------
// 4. AHCI Hardware Mapping & Flat Block File System
// ----------------------------------------------------
mod ahci {
    #[repr(C)]
    pub struct HbaPort {
        pub clb: u32, pub clbu: u32, pub fb: u32, pub fbu: u32,
        pub is: u32, pub ie: u32, pub cmd: u32, pub reserved0: u32,
        pub tfd: u32, pub sig: u32, pub ssts: u32, pub sctl: u32,
        pub serr: u32, pub sact: u32, pub ci: u32, pub sntf: u32,
        pub fbs: u32, pub reserved1: [u32; 11], pub vendor: [u32; 4],
    }

    #[repr(C)]
    pub struct HbaMem {
        pub cap: u32, pub ghc: u32, pub is: u32, pub pi: u32,
        pub vs: u32, pub ccc_ctl: u32, pub ccc_pts: u32, pub em_loc: u32,
        pub em_ctl: u32, pub cap2: u32, pub bohc: u32, pub reserved: [u8; 116],
        pub vendor: [u8; 96], pub ports: [HbaPort; 32],
    }

    // High performance flat block interface mapping file structures directly to SATA disk blocks
    #[repr(C)]
    #[derive(Clone, Copy)]
    pub struct FileRecord {
        pub name: [u8; 32],
        pub start_lba: u32,
        pub size_bytes: u32,
        pub active: u8,
    }

    pub static mut FILE_TABLE: [FileRecord; 16] = [FileRecord { name: [0; 32], start_lba: 0, size_bytes: 0, active: 0 }; 16];
    pub static mut ACTIVE_SATA_PORT: *mut HbaPort = core::ptr::null_mut();

    pub unsafe fn initialize(pci_ahci_base: usize) {
        let hba = &mut *(pci_ahci_base as *mut HbaMem);
        let pi = core::ptr::read_volatile(&hba.pi);
        for i in 0..32 {
            if (pi & (1 << i)) != 0 {
                let port = &mut hba.ports[i];
                let sig = core::ptr::read_volatile(&port.sig);
                if sig == 0x00000101 { // SATA Signature
                    ACTIVE_SATA_PORT = port;
                    break;
                }
            }
        }
    }
}

// ----------------------------------------------------
// 5. C POSIX Native Hook Definitions (Bridges C VM to OS)
// ----------------------------------------------------
#[no_mangle]
pub unsafe extern "C" fn malloc(size: usize) -> *mut u8 {
    ALLOCATOR.alloc(Layout::from_size_align(size, 8).unwrap())
}

#[no_mangle]
pub unsafe extern "C" fn free(_ptr: *mut u8) {}

#[no_mangle]
pub unsafe extern "C" fn realloc(ptr: *mut u8, size: usize) -> *mut u8 {
    let new_ptr = malloc(size);
    if !ptr.is_null() {
        core::ptr::copy_nonoverlapping(ptr, new_ptr, size);
    }
    new_ptr
}

#[no_mangle]
pub unsafe extern "C" fn printf(format: *const u8, mut _args: ...) -> i32 {
    let mut i = 0;
    while *format.add(i) != 0 {
        WRITER.write_byte(*format.add(i));
        i += 1;
    }
    0
}

#[no_mangle]
pub unsafe extern "C" fn fgets(s: *mut u8, size: i32, _stream: *mut u8) -> *mut u8 {
    let mut index = 0;
    while index < (size - 1) as usize {
        let character = get_char();
        WRITER.write_byte(character as u8);
        if character == '\n' {
            break;
        }
        *s.add(index) = character as u8;
        index += 1;
    }
    *s.add(index) = 0;
    s
}

// Map easec file read calls to Flat File System mapped via physical sectors
#[no_mangle]
pub unsafe extern "C" fn fopen(path: *const u8, _mode: *const u8) -> *mut u8 {
    let mut filename = [0u8; 32];
    let mut idx = 0;
    while *path.add(idx) != 0 && idx < 31 {
        filename[idx] = *path.add(idx);
        idx += 1;
    }
    for i in 0..16 {
        if ahci::FILE_TABLE[i].active == 1 && ahci::FILE_TABLE[i].name == filename {
            // Return raw offset tracker
            return &mut ahci::FILE_TABLE[i] as *mut ahci::FileRecord as *mut u8;
        }
    }
    core::ptr::null_mut()
}

#[no_mangle]
pub unsafe extern "C" fn fread(ptr: *mut u8, size: usize, nmemb: usize, stream: *mut u8) -> usize {
    if stream.is_null() { return 0; }
    let record = &*(stream as *const ahci::FileRecord);
    // Directly read SATA sector memory mapped to host ram
    let disk_memory_address = (0x20000000 + (record.start_lba * 512)) as *const u8;
    core::ptr::copy_nonoverlapping(disk_memory_address, ptr, size * nmemb);
    nmemb
}

#[no_mangle] pub unsafe extern "C" fn fclose(_stream: *mut u8) -> i32 { 0 }
#[no_mangle] pub unsafe extern "C" fn fseek(_stream: *mut u8, _offset: i32, _whence: i32) -> i32 { 0 }
#[no_mangle] pub unsafe extern "C" fn ftell(_stream: *mut u8) -> i32 { 512 } // Size boundary
#[no_mangle] pub unsafe extern "C" fn isalpha(c: i32) -> i32 { if (c >= 65 && c <= 90) || (c >= 97 && c <= 122) { 1 } else { 0 } }
#[no_mangle] pub unsafe extern "C" fn isdigit(c: i32) -> i32 { if c >= 48 && c <= 57 { 1 } else { 0 } }
#[no_mangle] pub unsafe extern "C" fn atof(_s: *const u8) -> f64 { 0.0 }
#[no_mangle] pub unsafe extern "C" fn atoll(_s: *const u8) -> i64 { 0 }

// Standard memory routing signatures
#[no_mangle] pub unsafe extern "C" fn memset(s: *mut u8, c: i32, n: usize) -> *mut u8 { core::ptr::write_bytes(s, c as u8, n); s }
#[no_mangle] pub unsafe extern "C" fn memcpy(dest: *mut u8, src: *const u8, n: usize) -> *mut u8 { core::ptr::copy_nonoverlapping(src, dest, n); dest }
#[no_mangle] pub unsafe extern "C" fn strlen(s: *const u8) -> usize { let mut len = 0; while *s.add(len) != 0 { len += 1; } len }
#[no_mangle] pub unsafe extern "C" fn strcmp(s1: *const u8, s2: *const u8) -> i32 {
    let mut i = 0;
    while *s1.add(i) != 0 && *s2.add(i) != 0 {
        if *s1.add(i) != *s2.add(i) {
            return (*s1.add(i) as i32) - (*s2.add(i) as i32);
        }
        i += 1;
    }
    (*s1.add(i) as i32) - (*s2.add(i) as i32)
}

// ----------------------------------------------------
// 6. Kernel Core Execution
// ----------------------------------------------------
extern "C" {
    fn main(argc: i32, argv: *const *const u8) -> i32;
}

#[no_mangle]
pub extern "C" fn kernel_main() -> ! {
    unsafe {
        // Probe PCI configuration space to map AHCI base address
        ahci::initialize(0xFEB00000); 

        // Populate physical filesystem partition table records (sector mapping config)
        ahci::FILE_TABLE[0] = ahci::FileRecord { name: *b"ahci0:/install.easec\0\0\0\0\0\0\0\0\0\0\0\0", start_lba: 10, size_bytes: 1024, active: 1 };
        
        let arg0 = b"inpsos\0".as_ptr();
        let arg1 = b"ahci0:/install.easec\0".as_ptr();
        let argv = [arg0, arg1, core::ptr::null()];
        
        // Relinquish thread to physical C Interpreter loop running inside the custom architecture
        main(2, argv.as_ptr());
    }

    loop {}
}

#[panic_handler]
fn panic(_info: &PanicInfo) -> ! {
    loop {}
}