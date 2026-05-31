#pragma once
#include <stdint.h>
#include <stddef.h>

#define FS_START_SECTOR   4096
#define MAX_FILES         64
#define FILE_NAME_MAX     32

struct FileEntry {
    char name[FILE_NAME_MAX];
    uint32_t start_sector;
    uint32_t size_bytes;
    uint8_t active;
};

struct DirectoryBlock {
    FileEntry files[MAX_FILES];
};

void init_fs_metadata();
bool create_file(const char* name, const char* content, size_t size);
bool read_file(const char* name, char* output_buffer, size_t max_size);
bool delete_file_fs(const char* name);
void list_files_fs();