class BitManager {
    private static List<char> nullCars = new(){'0','\0',' '};

    public static string BytesToString(byte[] bytes) {
        string returnable = "";
        foreach (byte letter in bytes) {
            returnable += (char) letter;
        }
        return returnable;
    }

    public static bool ByteToBoolean(byte theByte) {
        if (CharInList(nullCars, (char) theByte)) {
            return false;
        }
        return true;
    }

    private static bool CharInList(List<char> list, char item) {
        foreach (char letter in list) {
            if (letter == item) {
                return true;
            }
        }
        return false;
    }

    public static string ReadVariableLengthString(BinaryReader reader, int lengthBytes) {
        byte[] bytes = reader.ReadBytes(lengthBytes);
        ushort length;
        if (bytes.Count() > 1) {
            length = BitConverter.ToUInt16(bytes);
        }
        else {
            length = bytes[0];
        }
        return BytesToString(reader.ReadBytes(length));
    }
}