#include "simple_fs.h"
#include "../drivers/ahci.h"

extern void kprint(const char* str);
extern void* kmalloc(size_t size);

DirectoryBlock dir_cache;

void init_fs_metadata() {
    // Attempt to load directory metadata sector into layout map
    if (!ahci_read(FS_START_SECTOR, 1, (uint8_t*)&dir_cache)) {
        kprint("Failed to initialize filesystem on physical SATA disk.\n");
        return;
    }
}

bool create_file(const char* name, const char* content, size_t size) {
    int target_index = -1;
    uint32_t next_free_sector = FS_START_SECTOR + 10; // Allocate dynamic spacing offset for file system table

    for (int i = 0; i < MAX_FILES; i++) {
        if (dir_cache.files[i].active) {
            uint32_t file_sectors = (dir_cache.files[i].size_bytes + 511) / 512;
            if (dir_cache.files[i].start_sector + file_sectors > next_free_sector) {
                next_free_sector = dir_cache.files[i].start_sector + file_sectors;
            }
        }
        if (!dir_cache.files[i].active && target_index == -1) {
            target_index = i;
        }
    }

    if (target_index == -1) return false;

    // Copy metadata properties
    size_t name_len = 0;
    while (name[name_len] && name_len < FILE_NAME_MAX - 1) {
        dir_cache.files[target_index].name[name_len] = name[name_len];
        name_len++;
    }
    dir_cache.files[target_index].name[name_len] = '\0';
    dir_cache.files[target_index].start_sector = next_free_sector;
    dir_cache.files[target_index].size_bytes = size;
    dir_cache.files[target_index].active = 1;

    // Write file context payload block back to physical disk sectors
    uint32_t sectors_needed = (size + 511) / 512;
    uint8_t* temp_sector_buf = (uint8_t*)kmalloc(sectors_needed * 512);
    for (size_t i = 0; i < size; i++) temp_sector_buf[i] = content[i];

    if (!ahci_write(next_free_sector, sectors_needed, temp_sector_buf)) return false;
    if (!ahci_write(FS_START_SECTOR, 1, (uint8_t*)&dir_cache)) return false;

    return true;
}

bool read_file(const char* name, char* output_buffer, size_t max_size) {
    for (int i = 0; i < MAX_FILES; i++) {
        if (dir_cache.files[i].active) {
            // Compare string matching
            bool match = true;
            for (int k = 0; name[k] || dir_cache.files[i].name[k]; k++) {
                if (name[k] != dir_cache.files[i].name[k]) {
                    match = false;
                    break;
                }
            }
            if (match) {
                uint32_t sectors_needed = (dir_cache.files[i].size_bytes + 511) / 512;
                uint8_t* temp_buf = (uint8_t*)kmalloc(sectors_needed * 512);
                if (!ahci_read(dir_cache.files[i].start_sector, sectors_needed, temp_buf)) return false;
                
                size_t copy_limit = dir_cache.files[i].size_bytes < max_size ? dir_cache.files[i].size_bytes : max_size - 1;
                for (size_t k = 0; k < copy_limit; k++) {
                    output_buffer[k] = (char)temp_buf[k];
                }
                output_buffer[copy_limit] = '\0';
                return true;
            }
        }
    }
    return false;
}

bool delete_file_fs(const char* name) {
    for (int i = 0; i < MAX_FILES; i++) {
        if (dir_cache.files[i].active) {
            bool match = true;
            for (int k = 0; name[k] || dir_cache.files[i].name[k]; k++) {
                if (name[k] != dir_cache.files[i].name[k]) {
                    match = false;
                    break;
                }
            }
            if (match) {
                dir_cache.files[i].active = 0;
                return ahci_write(FS_START_SECTOR, 1, (uint8_t*)&dir_cache);
            }
        }
    }
    return false;
}