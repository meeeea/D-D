class Writer : BinaryWriter
{
    public Writer(FileStream file) : base(file) {}
    public void Write(string statment, bool AddLength = false) {
        // correct one
        if (AddLength) {
            Write((ushort) statment.Length);
        }
        byte[] bytes = new byte[statment.Length];
        foreach (char letter in statment) {
            bytes.Append((byte) letter);
        }
        Write(bytes);
    }
}