def encryption():
    print("Encryption")

    print("The message can only contain uppercase or lowercase letters.")
    msg = input("Enter message: ")
    key = int(input("Enter key(0-25): "))

    encrypted_text = ""

    for i in range(len(msg)):
        if ord(msg[i]) == 32:
            encrypted_text += chr(ord(msg[i]))

        elif ord(msg[i]) + key > 122:
            temp = (ord(msg[i]) + key) - 122
            encrypted_text += chr(96+temp)

        elif (ord(msg[i]) + key > 90) and (ord(msg[i]) <= 96):
            temp = (ord(msg[i]) + key) - 90
            encrypted_text += chr(64+temp)

        else:
            encrypted_text += chr(ord(msg[i]) + key)

    print("Encrypted: " + encrypted_text)


def decryption():
    print("Decryption")

    print("Message can only be Lower or Uppercase alphabet")
    encrp_msg = input("Enter encrypted Text: ")
    decrp_key = int(input("Enter key(0-25): "))

    decrypted_text = ""

    for i in range(len(encrp_msg)):
        if ord(encrp_msg[i]) == 32:
            decrypted_text += chr(ord(encrp_msg[i]))

        elif ((ord(encrp_msg[i]) - decrp_key) < 97) and ((ord(encrp_msg[i]) - decrp_key) > 90):
            temp = (ord(encrp_msg[i]) - decrp_key) + 26
            decrypted_text += chr(temp)

        elif (ord(encrp_msg[i]) - decrp_key) < 65:
            temp = (ord(encrp_msg[i]) - decrp_key) + 26
            decrypted_text += chr(temp)

        else:
            decrypted_text += chr(ord(encrp_msg[i]) - decrp_key)

    print("Decrypted Text: " + decrypted_text)


def main():
    choice = int(input("1. Encryption\n2. Decryption\nChoose(1,2): "))
    if choice == 1:
        encryption()
    elif choice == 2:
        decryption()
    else:
        print("Wrong Choice")


if __name__ == "__main__":
    main()
