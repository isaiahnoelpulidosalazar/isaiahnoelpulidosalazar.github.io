use core::ptr::{read_volatile, write_volatile};

#[repr(C)]
pub struct HbaPort {
    pub clb: u32,       // Command list base addr
    pub clbu: u32,      // Command list base addr upper
    pub fb: u32,        // FIS base addr
    pub fbu: u32,       // FIS base addr upper
    pub is: u32,        // Interrupt status
    pub ie: u32,        // Interrupt enable
    pub cmd: u32,       // Command and status
    pub _reserved: [u32; 1],
    pub tfd: u32,       // Task file data
    pub sig: u32,       // Signature
    // ... truncated for brevity
}

#[repr(C)]
pub struct HbaMem {
    pub cap: u32,       // Host capabilities
    pub ghc: u32,       // Global host control
    pub is: u32,        // Interrupt status
    pub pi: u32,        // Ports implemented
    pub vs: u32,        // Version
    pub ccc_ctl: u32,   // Command completion coalescing
    pub ccc_pts: u32,
    pub em_loc: u32,
    pub em_ctl: u32,
    pub cap2: u32,
    pub bohc: u32,
    pub reserved: [u8; 116],
    pub vendor: [u8; 96],
    pub ports: [HbaPort; 32],
}

pub fn init_ahci_controller(base_addr: usize) {
    let hba = unsafe { &mut *(base_addr as *mut HbaMem) };
    
    // Enable AHCI mode
    unsafe { write_volatile(&mut hba.ghc, read_volatile(&hba.ghc) | (1 << 31)); }
    
    // Scan implemented ports
    let pi = unsafe { read_volatile(&hba.pi) };
    for i in 0..32 {
        if (pi & (1 << i)) != 0 {
            // Port is active, we can initialize command tables and FIS to read/write disk sectors
        }
    }
}