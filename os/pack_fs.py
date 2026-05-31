import os
import sys
import struct

def pack_fs(programs_dir, out_file):
    # Initialize directory sector (512 bytes)
    dir_sector = bytearray(512)
    entries = []
    
    if not os.path.exists(programs_dir):
        print(f"Error: Programs directory '{programs_dir}' does not exist.")
        sys.exit(1)
        
    # Read and sort all .easec files from host programs directory
    files = sorted([f for f in os.listdir(programs_dir) if f.endswith('.easec')])
    
    # InpsFS supports up to 10 files in the root directory index sector
    files = files[:10]
    
    # File data starts immediately after the directory sector (Sector 129)
    current_relative_sector = 1
    file_data_blocks = bytearray()
    
    for filename in files:
        filepath = os.path.join(programs_dir, filename)
        with open(filepath, 'rb') as f:
            data = f.read()
            
        size = len(data)
        num_sectors = (size + 511) // 512
        if num_sectors == 0:
            num_sectors = 1
            
        # Absolute start sector is 129 + current_relative_sector
        absolute_start_sector = 129 + current_relative_sector
        
        # Format InpsFileEntry struct (48 bytes total):
        # char filename[32];      (32s)
        # uint32_t start_sector;  (I)
        # uint32_t num_sectors;   (I)
        # uint32_t size;          (I)
        # uint8_t used;           (B)
        # Padding to 48 bytes     (3x)
        entry_bytes = struct.pack('<32sIIIB3x', 
                                  filename.encode('utf-8')[:31], 
                                  absolute_start_sector, 
                                  num_sectors, 
                                  size, 
                                  1)
        entries.append(entry_bytes)
        
        # Pad file data to 512-byte blocks
        padded_data = bytearray(data)
        padding_needed = (512 - (len(padded_data) % 512)) % 512
        padded_data.extend(b'\x00' * padding_needed)
        
        file_data_blocks.extend(padded_data)
        current_relative_sector += num_sectors

    # Pack the entries into the 512-byte directory sector
    for i, entry in enumerate(entries):
        offset = i * 48
        dir_sector[offset:offset+48] = entry
        
    with open(out_file, 'wb') as out:
        out.write(dir_sector)
        out.write(file_data_blocks)
        
    print(f"Packed {len(files)} files into {out_file} successfully.")

if __name__ == '__main__':
    pack_fs('programs', 'fs.bin')