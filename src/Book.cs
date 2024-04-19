class Book {
    private ushort _id = 0;
    public ushort ID => _id;
    private string _name = "";
    public string Name => _name;
    private MonsterManager _mManager = new();
    public MonsterManager MManager => _mManager;
    private SpellManager _sManager = new();
    public SpellManager SManager => _sManager;
    private ClassManager _cManager = new();
    public ClassManager CManager => _cManager;
    private FeatManager _fManager = new();
    public FeatManager FManager => _fManager;
    private ItemManager _iManager = new();
    public ItemManager IManager => _iManager;

    public Book(string fileName) {
        try {
            using (BinaryReader reader = new BinaryReader(File.Open($".\\books\\{fileName}",
            FileMode.Open))) {
                ReadID(reader);
                ReadName(reader);
                ReadSpells(reader);
            }
        }
        catch {
            Console.WriteLine($"Failed to open {fileName}");
        }
    }

    private bool ReadID(BinaryReader reader) {
        try {
            _id = BitConverter.ToUInt16(reader.ReadBytes(2));
        }
        catch{
            throw new Exceptions.IDException();
        }
        return true;
    }

    private bool ReadName(BinaryReader reader) {
        try {
            _name = BitManager.BytesToString(reader.ReadBytes(3));
        }
        catch {
            throw new Exceptions.NameException();
        }
        return true;
    }

    private bool ReadSpells(BinaryReader reader) {
        return SManager.Add(reader);
    }

    public class Exceptions {
        [Serializable]
        public class IDException : Exception {
            public IDException() 
                : base("Missing ID") { }
        }
        [Serializable]
        public class NameException : Exception {
            public NameException() 
                : base("Failure to read name") { }
        }

        [Serializable] 
        public class SpellException : Exception {
            public SpellException()
                : base("Failure to read spells") { }
        }
    }
}