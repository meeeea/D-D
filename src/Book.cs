class Book {
    private ushort _id = 0;
    public ushort ID => _id;
    public byte[] IDSave {
        get {
            return BitConverter.GetBytes(ID);
        }
    }
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
        using (BinaryReader reader = new BinaryReader(File.Open($".\\books\\{fileName}",
        FileMode.Open))) {
            ReadID(reader);
            ReadName(reader);
            ReadSpells(reader);
        }
    }

    public Book(bool manual) {
        ManualID();
        ManualName();

        ManualSpellLoop();
    }

    private void ManualID() {
        while (true) {
            Console.WriteLine("Book ID:");

            try {
                    #pragma warning disable CS8604
                _id = ushort.Parse(Console.ReadLine());
                    #pragma warning restore CS8604
            }
            catch {
                Console.WriteLine("Input Invalid, try again");
                continue;
            }
            break;
        }
    }
    
    public void ManualName() {
        while (true) {
            Console.WriteLine("Select Name (3 Characters long): ");

            string? newName = Console.ReadLine();
            if (newName != null && newName.Length == 3) {
                _name = newName;
                return;
            }
        }
    }

    private void ManualSpellLoop() {
        while (true) {
            Console.WriteLine("1). Add Spell");
            Console.WriteLine("2). Finish Spells");

            string? response = Console.ReadLine();
            if (ManualSpellLoopOptions(response)) {
                return;
            }
        }
    }

    private bool ManualSpellLoopOptions(string? response) {
        switch (response) {
            case "1":
            AddSpell(); break;
            case "2":
            return true;
        }
        return false;
    }

    public void AddSpell() {
        SManager.AddSpell();
    }

    public void DisplayAll() {
        Console.WriteLine($"{ID}) {Name}");
        SManager.DisplayAll();
    }

    public void Display() {
        Console.WriteLine($"{ID}). {Name}");
    }

    public void SpellDisplay() {
        SManager.ListedDisplay();
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

    public void SaveBook() {
        try {
            using (Writer writer = new Writer(File.Open($".\\books\\{Name}.book",
            FileMode.Open))) {
                writer.Write(IDSave);
                writer.Write(Name);
                SManager.SaveAll(writer);
            }
        }
        catch {
            Console.WriteLine($"Failed to save {Name}");
        }
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