# isaiahnoelpulidosalazar.github.io
My website

---

# androiddependency

**androiddependency** is an Android library that eliminates boilerplate across projects. Includes Check (a class for validation), Convert (a class for data conversion), EasySQL (a simplified SQLite CRUD wrapper), FlippableImageView with custom animations, a custom RoundedAlertDialog with a modern card view, SimpleList for dynamic data binding, and an async URLRequest utility with callback runnables..

---

## Features

`androiddependency` contains

---

## Installation

Install the dependency by adding it to your root `settings.gradle` and project `build.gradle` files

`settings.gradle`
```gradle
dependencyResolutionManagement {
    repositoriesMode.set(RepositoriesMode.FAIL_ON_PROJECT_REPOS)
    repositories {
        google()
        mavenCentral()
        maven { url 'https://jitpack.io' } // Add this
    }
}
```

`build.gradle`
```gradle
dependencies {
    implementation libs.activity.ktx
    implementation libs.appcompat
    implementation libs.constraintlayout
    implementation libs.material

    // Add this
    implementation 'com.github.isaiahnoelpulidosalazar:isaiahnoelpulidosalazar.github.io:androiddependency-v1.2.1'
    
    testImplementation libs.junit
    androidTestImplementation libs.espresso.core
    androidTestImplementation libs.ext.junit
}
```

---

## Quick Usage Examples

---

## Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change. 

## License
[MIT](https://choosealicense.com/licenses/mit/)

---

# com.github.isaiahnoelpulidosalazar.nugetpackage

**com.github.isaiahnoelpulidosalazar.nugetpackage** is a NuGet package for .NET that simplifies validation through a custom Check class, a custom Convert class for data conversion, and a dedicated class that simplifies file IO through SimpleFileHandler.

---

## Features

The package operates under the `nugetpackage` namespace and contains several distinct modules to help keep your codebase clean and readable:

### Validation (`Check`)
A robust validation class to simplify standard string, format, and date checks.
- **Email**: Highly customizable email domain validation (allows whitelisting specific domain names, extensions, or full domains).
- **Phone Numbers**: Validates Philippine mobile numbers (`09`, `+639`, `639`).
- **Strings**: Check for spaces, symbols, pure numerical strings, or presence of numbers.
- **Time/Dates**: Easily calculate the remaining time between two `DateTime` objects (Days, Hours, Minutes, Seconds).

### Cryptography (`Cipher`)
A class to implement classic and recreational cipher techniques for string encryption.
- Transposition Cipher
- Giovanni Cipher
- Keyword Cipher
- Caesar Cipher

### Data Conversion (`Convert`)
Effortless type casting, text manipulation, and data encoding.
- Base64, Hex, and Binary encoding/decoding.
- String reversal and Byte-array conversions (`UTF8`).
- Quick string parsing to `Int`, `Double`, `Long`, and `Float`.

### File & Resource Handling (`SimpleFileHandler`)
Static methods to rapidly read, write, and extract files.
- Rapid `Read()`, `Write()`, and `Append()` for text files.
- **Embedded Resources**: Use `ProjectToLocation()` to easily extract and copy files marked as 'Embedded Resource' from your executing assembly to a physical directory.

### Sorting Algorithms (`SortAlgorithms`)
A massive suite of sorting algorithms implemented in C# as quick plug-and-play functions for integer and double arrays.
- **Standard**: Quick Sort, Merge Sort, Heap Sort, Selection Sort, Insertion Sort, Bubble Sort.
- **Advanced/Niche**: Tim Sort, Intro Sort, Cocktail Shaker Sort, Shell Sort, Pigeonhole Sort, Bead Sort, Patience Sorting, and even Bogo Sort!

---

## Installation

Install the package via the .NET CLI:

```bash
dotnet add package com.github.isaiahnoelpulidosalazar.nugetpackage
```

Or via the NuGet Package Manager Console:

```powershell
Install-Package com.github.isaiahnoelpulidosalazar.nugetpackage
```

---

## Quick Usage Examples

To use the package, simply include the namespace at the top of your file:
```csharp
using nugetpackage;
```

### 1. Validating Phone Numbers & Dates (`Check`)
```csharp
// Validate Philippine Phone Numbers
bool isValidPhone = Check.IsAValidPhilippineMobileNumber("+639123456789");
Console.WriteLine(isValidPhone); // True

// Calculate time left
DateTime now = DateTime.Now;
DateTime future = now.AddDays(5).AddHours(2);
Console.WriteLine($"Days left: {Check.HowManyDaysLeft(now, future)}"); // ~5.083
```

### 2. Custom Email Validation (`Check.Email`)
```csharp
// Setup strict domain rules
Check.Email.AddValidDomainName("gmail");
Check.Email.AddValidDomainExtension("com");

bool isEmailValid = Check.Email.IsValid("user@gmail.com");
Console.WriteLine(isEmailValid); // True
```

### 3. File Handling & Resource Extraction (`SimpleFileHandler`)
```csharp
// Write, Append, and Read text
SimpleFileHandler.Write("log.txt", "Process started.\n");
SimpleFileHandler.Append("log.txt", "Process finished.\n");
Console.WriteLine(SimpleFileHandler.Read("log.txt"));

// Extract an Embedded Resource to a physical location
Assembly myAssembly = Assembly.GetExecutingAssembly();
SimpleFileHandler.ProjectToLocation(myAssembly, "MyConfig.json", @"C:\AppConfigs");
```

### 4. Text Encryption (`Cipher`)
```csharp
string encrypted = Cipher.CaesarCipher("HELLO WORLD", shift: 3);
Console.WriteLine(encrypted); // KHOOR ZRUOG
```

### 5. String & Data Conversions (`Convert`)
```csharp
// String Reversal
string reversed = Convert.Reverse("C# is awesome");

// Base64 Encoding
string encoded = Convert.ToBase64("Secret Message");
string decoded = Convert.FromBase64(encoded);

// Hex Conversions
string hexValue = Convert.ToHex("Hello");
Console.WriteLine(hexValue); // 48656C6C6F
```

### 6. Sorting Arrays (`SortAlgorithms`)
```csharp
int[] array = { 5, 2, 9, 1, 5, 6 };

// Using QuickSort
int[] sortedArray = SortAlgorithms.QuickSort(array);

// Using more exotic sorts
int[] introSorted = SortAlgorithms.IntroSort(array);

Console.WriteLine(string.Join(", ", sortedArray)); // 1, 2, 5, 5, 6, 9
```

---

## Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change. 

## License
[MIT](https://choosealicense.com/licenses/mit/)

---

# pythonbundle

**pythonbundle** is a PyPI package for Python 3.10+ that provides useful tools for data conversion and validation, a custom EasySQL class for simplified SQLite operations, a custom excel file handler, and a custom file IO handler.

---

## Features

`pythonbundle` contains several distinct modules to help keep your codebase clean and readable:

### Validation (`Check`)
A robust validation class to simplify standard string and format checks.
- Email formatting and customizable domain validation.
- Validates Philippine mobile numbers (+639 / 09).
- Check for spaces, symbols, and pure numerical strings.

### Cryptography (`Cipher`)
A basic cryptography class to implement classic cipher techniques.
- Transposition Cipher
- Giovanni Cipher
- Keyword Cipher
- Caesar Cipher

### Data Conversion (`Convert`)
Effortless type casting and data encoding.
- Hex, Binary, and Base64 encoding/decoding.
- String reversal and byte-array conversions.
- Quick casting for Int, Float, Double, and Long.

### Data Structures
Custom implementations for improved data manipulation.
- **`Dictionarily`**: An enhanced Dictionary object with built-in sorting (alphabetical and numerical-first).
- **`Memory`**: A clean, object-oriented list/array wrapper to handle storage, indexing, and removal.
- **`Stackily`**: A classic Stack implementation (`push`, `pop`, `peek`, `is_empty`, `size`).
- **`Node`**: A lightweight binary tree node implementation.

### Database Management (`EasySQL`)
A simplified wrapper around Python's built-in `sqlite3`.
- Create tables with ease by passing lists of dictionaries.
- Insert, delete, and clear records directly via Python dictionaries.
- Fetch and print table values seamlessly.

### Excel Operations
A wrapper for `openpyxl` allowing for extremely fast Excel file data manipulation.
- Read and write to specific columns across single or multiple sheets.
- Skip header rows easily using `skip_rows`.
- Zero-hassle reading/writing to entire column letters (e.g., Column 'A').

### File Handling (`SimpleFileHandler`)
Static methods to rapidly `read()`, `write()`, and `append()` to text files using `utf-8` encoding.

### Sorting Algorithms
A massive suite of sorting algorithms available as quick plug-and-play functions.
- Quick Sort, Merge Sort, Heap Sort, Selection Sort, Insertion Sort, Bubble Sort.
- Advanced/Niche Sorts: Tim Sort, Intro Sort, Cocktail Shaker Sort, Shell Sort, Pigeonhole Sort, Bead Sort, and even Bogo Sort!

---

## Installation

```bash
pip install pythonbundle
```

### Dependencies
The package largely uses Python's standard library (e.g., `sqlite3`, `math`, `re`, `base64`). However, the Excel operations module requires:
- `openpyxl`
- `unidecode`

---

## Quick Usage Examples

### 1. Simple SQLite Database Queries (`EasySQL`)
```python
from pythonbundle import EasySQL

db = EasySQL("my_database")

# Create a database table
db.create_table("users", {
    "id": "INTEGER PRIMARY KEY", 
    "name": "TEXT",
    "age": "INTEGER"
})

# Insert data
db.insert_to_table("users", {"id": 1, "name": "Alice", "age": 28})

# Fetch values
records = db.get_table_values("users")
print(records)
```

### 2. Validating Phone Numbers & Emails (`Check`)
```python
from pythonbundle import Check

# Validate Philippine Phone Numbers
is_valid = Check.is_a_valid_philippine_mobile_number("+639123456789")
print(is_valid)  # True

# Validate Emails with strict domain rules
Check.Email.add_valid_domain_name("gmail")
Check.Email.add_valid_domain_extension("com")
print(Check.Email.is_valid("user@gmail.com"))  # True
```

### 3. File Handling (`SimpleFileHandler`)
```python
from pythonbundle import SimpleFileHandler

# Write, Append, and Read
SimpleFileHandler.write("log.txt", "Process started.\n")
SimpleFileHandler.append("log.txt", "Process finished.\n")

print(SimpleFileHandler.read("log.txt"))
```

### 4. Text Encryption (`Cipher`)
```python
from pythonbundle import Cipher

encrypted = Cipher.caesar_cipher("HELLO WORLD", shift=3)
print(encrypted)  # KHOOR ZRUOG
```

### 5. Sorting Array Data
```python
from pythonbundle import quicksort, merge_sort

array = [5, 2, 9, 1, 5, 6]
print(quicksort(array)) #[1, 2, 5, 5, 6, 9]
```

---

## Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change. 

## License
[MIT](https://choosealicense.com/licenses/mit/)