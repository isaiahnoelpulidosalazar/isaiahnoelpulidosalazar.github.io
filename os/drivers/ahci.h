#pragma once
#include <stdint.h>

#define SATA_SIG_ATA    0x00000101
#define SATA_SIG_ATAPI  0xEB140101
#define AHCI_DEV_SATA   1

#define AHCI_BASE       0x400000 // Fixed MMIO mapping offset used during boot
#define HBA_PxIS_TFES   (1 << 30)

struct HBA_port {
    uint32_t clb;       // 0x00, command list base address, 1K aligned
    uint32_t clbu;      // 0x04, command list base address upper 32 bits
    uint32_t fb;        // 0x08, FIS base address, 256-byte aligned
    uint32_t fbu;       // 0x0C, FIS base address upper 32 bits
    uint32_t is;        // 0x10, interrupt status
    uint32_t ie;        // 0x14, interrupt enable
    uint32_t cmd;       // 0x18, command and status
    uint32_t rsv0;      // 0x1C, Reserved
    uint32_t tfd;       // 0x20, task file data
    uint32_t sig;       // 0x24, signature
    uint32_t ssts;      // 0x28, SATA status (SCR0:SStatus)
    uint32_t sctl;      // 0x2C, SATA control (SCR1:SControl)
    uint32_t serr;      // 0x30, SATA error (SCR2:SError)
    uint32_t sact;      // 0x34, SATA active (SCR3:SActive)
    uint32_t ci;        // 0x38, command issue
    uint32_t sntf;      // 0x3C, SATA notification (SCR4:SNotification)
    uint32_t fbs;       // 0x40, FIS-based switch control
    uint32_t rsv1[11];  // 0x44 ~ 0x6F, Reserved
    uint32_t vendor[4]; // 0x70 ~ 0x7F, vendor specific
};

struct HBA_mem {
    uint32_t cap;       // Host capabilities
    uint32_t ghc;       // Global host control
    uint32_t is;        // Interrupt status
    uint32_t pi;        // Ports implemented
    uint32_t vs;        // Version
    uint32_t ccc_ctl;   // Command completion coalescing control
    uint32_t ccc_pts;   // Command completion coalescing ports
    uint32_t em_loc;    // Enclosure management location
    uint32_t em_ctl;    // Enclosure management control
    uint32_t cap2;      // Host capabilities extended
    uint32_t bohc;      // BIOS/OS handoff control and status
    uint8_t  rsv[116];  // Reserved
    uint8_t  vendor[96];// Vendor specific registers
    HBA_port ports[1];  // Port structures (actual length resolved from cap.np)
};

struct AHCI_cmd_header {
    uint8_t  cfl:5;     // Command FIS length in dwords, 2 ~ 16
    uint8_t  a:1;       // ATAPI
    uint8_t  w:1;       // Write
    uint8_t  p:1;       // Prefetchable
    uint8_t  r:1;       // Reset
    uint8_t  b:1;       // BIST
    uint8_t  c:1;       // Clear busy upon R_OK
    uint8_t  rsv0:1;    // Reserved
    uint8_t  pmp:4;     // Port multiplier port
    uint16_t prdtl;     // Physical region descriptor table length in entries
    volatile uint32_t prdbc; // Physical region descriptor byte count transferred
    uint32_t ctba;      // Command table descriptor base address
    uint32_t ctbau;     // Command table descriptor base address upper 32 bits
    uint32_t rsv1[4];   // Reserved
};

struct AHCI_prdt_entry {
    uint32_t dba;       // Data base address
    uint32_t dbau;      // Data base address upper 32 bits
    uint32_t rsv0;      // Reserved
    uint32_t dbc:22;    // Byte count, 4M max
    uint32_t rsv1:9;    // Reserved
    uint32_t i:1;       // Interrupt on completion
};

struct AHCI_cmd_table {
    uint8_t  cfis[64];  // Command FIS
    uint8_t  acmd[16];  // ATAPI command, 12 or 16 bytes
    uint8_t  rsv[48];   // Reserved
    AHCI_prdt_entry prdt_entry[1]; // Physical Region Descriptor Table entries
};

struct FIS_reg_h2d {
    uint8_t  fis_type;  // FIS_TYPE_REG_H2D
    uint8_t  pmport:4;  // Port multiplier port
    uint8_t  rsv0:3;    // Reserved
    uint8_t  c:1;       // 1: Command, 0: Control
    uint8_t  command;   // Command register
    uint8_t  featurel;  // Feature register low
    uint8_t  lba0;      // LBA low register, 7:0
    uint8_t  lba1;      // LBA mid register, 15:8
    uint8_t  lba2;      // LBA high register, 23:16
    uint8_t  device;    // Device register
    uint8_t  lba3;      // LBA register, 31:24
    uint8_t  lba4;      // LBA register, 39:32
    uint8_t  lba5;      // LBA register, 47:40
    uint8_t  featureh;  // Feature register high
    uint8_t  countl;    // Count register low
    uint8_t  counth;    // Count register high
    uint8_t  icc;       // Isochronous command completion
    uint8_t  control;   // Control register
    uint8_t  rsv1[4];   // Reserved
};

void ahci_init();
bool ahci_read(uint32_t sector, uint32_t count, uint8_t* buffer);
bool ahci_write(uint32_t sector, uint32_t count, const uint8_t* buffer);