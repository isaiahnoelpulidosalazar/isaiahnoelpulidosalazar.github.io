if __name__ == "__main__":
    db = EasySQL("my_database")

    db.create_table("users", {
        "id": "INTEGER PRIMARY KEY", 
        "name": "TEXT",
        "age": "INTEGER"
    })

    db.insert_to_table("users", {"id": 1, "name": "Alice", "age": 28})
    db.insert_to_table("users", {"id": 2, "name": "Bob", "age": 35})
    db.insert_to_table("users", {"id": 3, "name": "Charlie", "age": 28})

    print("--- After Insertions ---")
    db.print_table("users")

    db.delete_from_table("users", {"age": 28, "name": "Alice"})

    print("\n--- After Deleting Alice ---")
    db.print_table("users")

    db.clear_table("users")
    print("\n--- After Clearing Table ---")
    db.print_table("users")

    db.delete_table("users")