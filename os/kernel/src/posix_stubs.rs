use core::ffi::{c_char, c_void, c_int};
use crate::ahci; // Connects to our AHCI driver

extern "C" {
    // The main function from your easec.c modified slightly to accept a string buffer
    pub fn run_script(source: *const c_char, env: *mut c_void); 
}

// 1. Memory Allocator Stubs
#[no_mangle]
pub extern "C" fn malloc(size: usize) -> *mut c_void {
    // Hook into Rust's Global Allocator
    extern crate alloc;
    use alloc::alloc::{alloc, Layout};
    unsafe { alloc(Layout::from_size_align(size, 8).unwrap()) as *mut c_void }
}

#[no_mangle]
pub extern "C" fn free(_ptr: *mut c_void) {
    // Free memory
}

// 2. IO Stubs (Outputs to Kernel VGA/Serial)
#[no_mangle]
pub extern "C" fn printf(format: *const c_char) -> c_int {
    // Map to the kernel's print! macro
    0
}

// 3. File System Stubs (Connects to AHCI Driver)
#[no_mangle]
pub extern "C" fn fopen(path: *const c_char, mode: *const c_char) -> *mut c_void {
    // 1. Read path string
    // 2. Ask AHCI driver to open the file on the physical drive
    // 3. Return a file handle pointer
    core::ptr::null_mut()
}