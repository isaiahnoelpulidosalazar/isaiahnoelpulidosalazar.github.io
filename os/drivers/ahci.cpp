#include <stddef.h> // Added to define size_t
#include "ahci.h"

extern void kprint(const char* str);
extern void* kmalloc(size_t size);

HBA_mem* hba_ptr = nullptr;
HBA_port* active_port = nullptr;

void probe_pci_ahci() {
    // Standard AHCI Host Controller interface defaults on PCI Bus 0, Device 31, Func 2 or Bus 0, Device 23, Func 0
    // For educational bare-metal execution simplicity, we hardcode fallback to standard ABAR legacy mapping address (0x400000)
    hba_ptr = (HBA_mem*)AHCI_BASE;
}

void port_rebase(HBA_port* port, int port_no) {
    (void)port_no; // Silence unused parameter warning

    // Configure dynamic DMA memory mappings to allow reading physical sectors safely
    port->cmd &= ~0x0001; // Stop command engine (ST = 0)
    port->cmd &= ~0x0010; // Stop FIS receive (FRE = 0)

    while (port->cmd & 0x4000 || port->cmd & 0x8000); // Wait for execution to idle

    // Map 1KB command list and 256B FIS buffers to physical kernel heap boundaries
    port->clb = (uint32_t)kmalloc(1024);
    port->fb = (uint32_t)kmalloc(256);

    port->cmd |= 0x0010; // Start FIS receive (FRE = 1)
    port->cmd |= 0x0001; // Start command engine (ST = 1)
}

void ahci_init() {
    probe_pci_ahci();
    if (!hba_ptr) {
        kprint("Failed to locate AHCI HBA Controller.\n");
        return;
    }

    uint32_t pi = hba_ptr->pi;
    for (int i = 0; i < 32; i++) {
        if (pi & (1 << i)) {
            uint32_t ssts = hba_ptr->ports[i].ssts;
            uint8_t ipm = (ssts >> 8) & 0x0F;
            uint8_t det = ssts & 0x0F;

            if (det == 3 && ipm == 1) { // Device present and active
                if (hba_ptr->ports[i].sig == SATA_SIG_ATA) {
                    active_port = &hba_ptr->ports[i];
                    port_rebase(active_port, i);
                    kprint("AHCI Controller initialized on Port: SATA Hard Disk.\n");
                    return;
                }
            }
        }
    }
    kprint("No active SATA physical drive found.\n");
}

int find_cmd_slot(HBA_port* port) {
    uint32_t slots = (port->sact | port->ci);
    for (int i = 0; i < 32; i++) {
        if ((slots & (1 << i)) == 0) return i;
    }
    return -1;
}

bool ahci_read(uint32_t sector, uint32_t count, uint8_t* buffer) {
    if (!active_port) return false;

    active_port->is = (uint32_t)-1; // Clear interrupt registers
    int slot = find_cmd_slot(active_port);
    if (slot == -1) return false;

    AHCI_cmd_header* cmdheader = (AHCI_cmd_header*)(active_port->clb);
    cmdheader += slot;
    cmdheader->cfl = sizeof(FIS_reg_h2d) / sizeof(uint32_t);
    cmdheader->w = 0; // Read mode
    cmdheader->prdtl = 1;

    AHCI_cmd_table* cmdtable = (AHCI_cmd_table*)(cmdheader->ctba);
    // Zero target command structure
    for(size_t idx = 0; idx < sizeof(AHCI_cmd_table); idx++) ((char*)cmdtable)[idx] = 0;

    cmdtable->prdt_entry[0].dba = (uint32_t)buffer;
    cmdtable->prdt_entry[0].dbc = (count * 512) - 1; // 512-byte sector bounds
    cmdtable->prdt_entry[0].i = 1;

    FIS_reg_h2d* cmdfis = (FIS_reg_h2d*)(&cmdtable->cfis);
    cmdfis->fis_type = 0x27; // Register FIS host-to-device
    cmdfis->c = 1;           // Command
    cmdfis->command = 0x25;  // READ DMA LBA48

    cmdfis->lba0 = (uint8_t)sector;
    cmdfis->lba1 = (uint8_t)(sector >> 8);
    cmdfis->lba2 = (uint8_t)(sector >> 16);
    cmdfis->device = 1 << 6; // LBA mode flag

    cmdfis->lba3 = (uint8_t)(sector >> 24);
    cmdfis->countl = count & 0xFF;
    cmdfis->counth = (count >> 8) & 0xFF;

    // Await command queue processing loop
    while ((active_port->tfd & (0x80 | 0x08)) && active_port->ci);
    active_port->ci = (1 << slot); // Issue write execution sequence

    while (1) {
        if ((active_port->ci & (1 << slot)) == 0) break;
        if (active_port->is & HBA_PxIS_TFES) return false; // Disk interface error
    }

    return true;
}

bool ahci_write(uint32_t sector, uint32_t count, const uint8_t* buffer) {
    if (!active_port) return false;

    active_port->is = (uint32_t)-1;
    int slot = find_cmd_slot(active_port);
    if (slot == -1) return false;

    AHCI_cmd_header* cmdheader = (AHCI_cmd_header*)(active_port->clb);
    cmdheader += slot;
    cmdheader->cfl = sizeof(FIS_reg_h2d) / sizeof(uint32_t);
    cmdheader->w = 1; // Write mode
    cmdheader->prdtl = 1;

    AHCI_cmd_table* cmdtable = (AHCI_cmd_table*)(cmdheader->ctba);
    for(size_t idx = 0; idx < sizeof(AHCI_cmd_table); idx++) ((char*)cmdtable)[idx] = 0;

    cmdtable->prdt_entry[0].dba = (uint32_t)buffer;
    cmdtable->prdt_entry[0].dbc = (count * 512) - 1;
    cmdtable->prdt_entry[0].i = 1;

    FIS_reg_h2d* cmdfis = (FIS_reg_h2d*)(&cmdtable->cfis);
    cmdfis->fis_type = 0x27;
    cmdfis->c = 1;
    cmdfis->command = 0x35; // WRITE DMA LBA48

    cmdfis->lba0 = (uint8_t)sector;
    cmdfis->lba1 = (uint8_t)(sector >> 8);
    cmdfis->lba2 = (uint8_t)(sector >> 16);
    cmdfis->device = 1 << 6;

    cmdfis->lba3 = (uint8_t)(sector >> 24);
    cmdfis->countl = count & 0xFF;
    cmdfis->counth = (count >> 8) & 0xFF;

    while ((active_port->tfd & (0x80 | 0x08)) && active_port->ci);
    active_port->ci = (1 << slot);

    while (1) {
        if ((active_port->ci & (1 << slot)) == 0) break;
        if (active_port->is & HBA_PxIS_TFES) return false;
    }

    return true;
}