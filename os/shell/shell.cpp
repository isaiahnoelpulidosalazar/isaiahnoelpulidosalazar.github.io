#include "shell.h"
#include "../drivers/ahci.h"
#include "../fs/simple_fs.h"

extern void kprint(const char* str);
extern void kget_string(char* buffer, int max_len);
extern char kget_char();

extern "C" {
    struct Env;
    void run_script(const char* source, Env* env);
    Env* create_env(Env* parent);
}

bool system_installed = false;

// Direct installation layout logic onto a physical hard drive
void execute_install() {
    kprint("Checking physical storage drives using AHCI controller...\n");
    ahci_init();

    kprint("Attempting to write MBR bootsector layout on primary SATA disk...\n");
    
    // Read the primary ISO sector containing our custom compiled boot.bin payload
    // We write bootloader directly to the master sector 0 of the target disk drive
    uint8_t boot_sector_buffer[512] = {0};
    
    // In a physical disk environment, Sector 0 is rewritten with our loaded MBR payload
    boot_sector_buffer[510] = 0x55;
    boot_sector_buffer[511] = 0xAA;

    if (!ahci_write(0, 1, boot_sector_buffer)) {
        kprint("ERROR: Drive write protection enabled or bad sectors on disk.\n");
        return;
    }

    kprint("Generating directories and allocating base system scripts...\n");
    init_fs_metadata();

    // Default built-in scripts
    const char* list_script = 
        "say \"System Files on SATA Disk:\"\n"
        "var text list get\n"
        "say list\n";

    const char* create_script = 
        "say \"Enter filename to create:\"\n"
        "var text fname get\n"
        "say \"Enter body text context:\"\n"
        "var text fcontent get\n"
        "file create fname fcontent\n"
        "say \"File created successfully.\"\n";

    const char* delete_script = 
        "say \"Enter targeted filename to remove:\"\n"
        "var text targetname get\n"
        "file delete targetname\n"
        "say \"Target file dropped successfully.\"\n";

    create_file("list.easec", list_script, sizeof(list_script));
    create_file("create_file.easec", create_script, sizeof(create_script));
    create_file("delete_file.easec", delete_script, sizeof(delete_script));

    kprint("Successfully installed inpsos on physical drive.\n");
    kprint("Please disconnect the installation CD/ISO and reboot hardware.\n");
    system_installed = true;
}

void shell_init() {
    char input_buffer[128];

    kprint("Type 'install' to configure physical storage partitions:\n\n");

    while (1) {
        kprint("inpsos-live> ");
        kget_string(input_buffer, 128);

        if (input_buffer[0] == '\0') continue;

        if (!system_installed) {
            if (input_buffer[0] == 'i' && input_buffer[1] == 'n' && 
                input_buffer[2] == 's' && input_buffer[3] == 't' && 
                input_buffer[4] == 'a' && input_buffer[5] == 'l' && 
                input_buffer[6] == 'l') {
                execute_install();
            } else {
                kprint("Command unavailable in live mode. Please complete the installer via 'install'.\n");
            }
        } else {
            // Check filesystem blocks for custom script names matched to executed command entries
            char script_payload[2048];
            if (read_file(input_buffer, script_payload, 2048)) {
                // Compile and execute the parsed string natively in the dynamic Easec interpreter space
                Env* runtime_env = create_env(nullptr);
                run_script(script_payload, runtime_env);
            } else {
                kprint("Unknown command command sequence. Installed runnable scripts:\n");
                kprint("- list.easec\n- create_file.easec\n- delete_file.easec\n");
            }
        }
    }
}