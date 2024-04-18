class Book {
    private UInt16 id = 0;
    public UInt16 ID => id;
    private string name = "";
    public string Name => name;
    private MonsterManager mManager = new();
    public MonsterManager MManager => mManager;
    private SpellManager sManager = new();
    public SpellManager SManager => sManager;
    private ClassManager cManager = new();
    public ClassManager CManager => cManager;
    private FeatManager fManager = new();
    public FeatManager FManager => fManager;
    private ItemManager iManager = new();
    public ItemManager IManager => iManager;

    public Book(string fileName) {
        try {
            using (BinaryReader reader = new BinaryReader(File.Open($".\\books\\{fileName}",
            FileMode.Open))) {
                ReadID(reader);
                ReadName(reader);
            }
        }
        catch {
            Console.WriteLine($"Failed to open {fileName}");
        }
    }

    private bool ReadID(BinaryReader reader) {
        try {
            id = BitConverter.ToUInt16(reader.ReadBytes(2));
        }
        catch{
            throw new Exceptions.IDException();
        }
        return true;
    }

    private bool ReadName(BinaryReader reader) {
        try {
            name = Convertion.BytesToString(reader.ReadBytes(3));
        }
        catch {
            throw new Exceptions.NameReadingError();
        }
        return true;
    }

    public class Exceptions {
        [Serializable]
        public class IDException : Exception {
            public IDException() 
                : base("Missing ID") { }
        }
        [Serializable]
        public class NameReadingError : Exception {
            public NameReadingError() 
                : base("Failure to read name") { }
        }
    }
}