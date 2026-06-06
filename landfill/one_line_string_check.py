while True:
    pw, a = input("Enter password: "), ""
    exit() if (print("Correct password.") if pw == "default123" else exec("a = 'Incorrect password.'")) is None and a != "Incorrect password." else print(a)
