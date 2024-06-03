using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;

class Book {
    private ushort _id = 0;
    public ushort ID => _id;
    private static int IDLength = 5;
    public string IDSave => ID.ToString().PadLeft(IDLength, '0');
    private string _name = "";
    public string Name => _name;
    
    public Content.OneOfManager? this[int index] { 
        get {
            switch (index) {
                case 1 : return AManager;
                case 2 : return BManager;
                case 3 : return CManager;
                case 4 : return FManager;
                case 5 : return IManager;
                case 6 : return MManager;
                case 7 : return RManager;
                case 8 : return SManager;
                case 9 : return UManager;
            }
            return null;
        }
    }

    public Content? SelectContentByID(int contentType, ushort id) {
        switch (contentType) {
            case 1: return ((Manager<Ability>?) this[1])?.SelectContentByID(id);
        }
        throw new Content.OneOfManager.ManagerTypeException();
    }

    private Manager<Monster> _mManager = new();
    public Manager<Monster> MManager => _mManager;
    private Manager<Spell> _sManager = new();
    public Manager<Spell> SManager => _sManager;
    private Manager<PCClass> _cManager = new();
    public Manager<PCClass> CManager => _cManager;
    private Manager<Feat> _fManager = new();
    public Manager<Feat> FManager => _fManager;
    private Manager<Item> _iManager = new();
    public Manager<Item> IManager => _iManager;
    private Manager<Ability> _aManager = new();
    public Manager<Ability> AManager => _aManager;
    private Manager<SubClass> _uManager = new();
    public Manager<SubClass> UManager => _uManager;
    private Manager<Race> _rManager = new();
    public Manager<Race> RManager => _rManager;
    private Manager<Background> _bManager = new();
    public Manager<Background> BManager => _bManager;
    private OverrideManager Omanager = new();
    public OverrideManager OManager => Omanager;
    
    public Book(string fileName, string folder = ".\\books\\") {
        using (StreamReader reader = new StreamReader(File.Open($"{folder}{fileName}",
        FileMode.Open))) {
            string? bookInfo = reader.ReadLine();
            if (bookInfo == null) {
                throw new Exceptions.MissingFileException();
            }
            try {
                _id = ushort.Parse(bookInfo.Substring(0, IDLength));
                _name = bookInfo.Substring(IDLength, 3);
            }
            catch (FormatException) {
                throw new Exceptions.IDException();
            }
            catch (ArgumentOutOfRangeException) {
                throw new Exceptions.NameException();
            }
            ReadContent(reader);
        }
    }

    public Book() {
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
        SManager.AddNew();
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

    private void ReadContent(StreamReader reader) {
        while (true) {
            string? nextItem = reader.ReadLine();
            if (nextItem == null) {
                break;
            }
            switch (nextItem[0]) {
                case 'S':
                    SManager.Add(new Spell(nextItem.Substring(1))); break;
                case 'O':
                    OverrideAdder(nextItem.Substring(1)); break;
            }
        }
    }

    private void OverrideAdder(string line) {
        switch (line[0]) {
            case 'S':
            OManager.Spell.Add(new SpellOverride(line.Substring(1))); break;
        }
    }

    public void SaveBook() {
        try {
            using (StreamWriter writer = new StreamWriter(File.Open($".\\books\\{Name}.book",
            FileMode.Create))) {
                writer.Write($"{IDSave}{Name}\n");
                SManager.SaveAll(writer);
            }
        }
        catch {
            Console.WriteLine($"Failed to save {Name}");
        }
    }

    public void Fix() {
        _sManager = SManager.Fix();
    }

    public class Exceptions {
        [Serializable]
        public class MissingFileException : Exception {
            public MissingFileException() : base("The Book is emtpy") {}
        }

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

        [Serializable]
        public class OverrideException : Exception {
            public OverrideException()
                : base("Failure to read override") { }
        }
    }
}