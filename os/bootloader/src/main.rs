#![no_std]
#![no_main]

extern crate alloc;

use uefi::prelude::*;
use uefi::proto::media::file::{File, FileAttribute, FileMode};
use uefi::proto::media::fs::SimpleFileSystem;
use uefi::table::boot::{AllocateType, MemoryType};

#[repr(C)]
struct ElfHeader {
    magic: [u8; 4],
    class: u8,
    data: u8,
    version: u8,
    os_abi: u8,
    abi_version: u8,
    pad: [u8; 7],
    elf_type: u16,
    machine: u16,
    version2: u32,
    entry: u64,
    phoff: u64,
    shoff: u64,
    flags: u32,
    ehsize: u16,
    phentsize: u16,
    phnum: u16,
    shentsize: u16,
    shnum: u16,
    shstrndx: u16,
}

#[entry]
fn main(image: Handle, mut system_table: SystemTable<Boot>) -> Status {
    uefi_services::init(&mut system_table).unwrap();
    let bs = system_table.boot_services();

    // 1. Locate the simple file system
    let sfs_handle = bs.get_handle_for_protocol::<SimpleFileSystem>().unwrap();
    let mut sfs = bs.open_protocol_exclusive::<SimpleFileSystem>(sfs_handle).unwrap();
    let mut root_dir = sfs.open_volume().unwrap();

    // 2. Open the physical kernel.elf file
    let file_handle = root_dir
        .open(
            cstr16!("kernel.elf"),
            FileMode::Read,
            FileAttribute::empty(),
        )
        .unwrap();

    let mut file = match file_handle.into_type().unwrap() {
        uefi::proto::media::file::FileType::Regular(f) => f,
        _ => return Status::LOAD_ERROR,
    };

    // 3. Read ELF Header
    let mut header = ElfHeader {
        magic: [0; 4], class: 0, data: 0, version: 0, os_abi: 0, abi_version: 0,
        pad: [0; 7], elf_type: 0, machine: 0, version2: 0, entry: 0, phoff: 0,
        shoff: 0, flags: 0, ehsize: 0, phentsize: 0, phnum: 0, shentsize: 0,
        shnum: 0, shstrndx: 0,
    };
    let header_slice = unsafe {
        core::slice::from_raw_parts_mut(
            &mut header as *mut ElfHeader as *mut u8,
            core::mem::size_of::<ElfHeader>(),
        )
    };
    file.read(header_slice).unwrap();

    if header.magic != [0x7F, b'E', b'L', b'F'] {
        return Status::INVALID_PARAMETER;
    }

    // 4. Allocate memory and load kernel
    let pages = 512; // 2MB
    let kernel_address = bs
        .allocate_pages(
            AllocateType::AnyPages,
            MemoryType::LOADER_DATA,
            pages,
        )
        .unwrap();

    file.set_position(0).unwrap();
    let dest_slice = unsafe {
        core::slice::from_raw_parts_mut(kernel_address as *mut u8, pages * 4096)
    };
    file.read(dest_slice).unwrap();

    // 5. Exit boot services to take pure hardware control
    // This consumes the system table and retrieves the final Memory Map
    let (_, _mmap) = system_table.exit_boot_services(MemoryType::LOADER_DATA);
    
    // Calculate entry point dynamically from ELF header
    let entry_fn: extern "sysv64" fn() -> ! = unsafe {
        core::mem::transmute(kernel_address + header.entry)
    };

    entry_fn();
}