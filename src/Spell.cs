class Spell {
    private ushort _level = 0;
    public ushort Level => _level;
    public string _name = "";
    public string Name => _name;
    private bool _somatic = false;
    public bool Somatic => _somatic;
    private bool _verbal = false;
    public bool Verbal => _verbal;
    private bool _matirial = false;
    public bool Matirial => _matirial;
    private string _matirialComponents = "";
    public string MatirialComponents => _matirialComponents;
    private bool _matirialIsConsumed = false;
    public bool MatirialIsConsumed => _matirialIsConsumed;
    public string MatirialIsConsumedS {
        get {
            if (MatirialIsConsumed) {
                return ", which the spell consumes";
            }
            return "";
        }
    }
    public string Components {
        get {
            if (Verbal) {
                if (Somatic) {
                    if (Matirial) {
                        return $"V, S, M ({MatirialComponents}{MatirialIsConsumedS})";
                    }
                    return $"V, S";
                }
                return $"V";
            }
            else {
                if (Somatic) {
                    if (Matirial) {
                        return $"S, M ({MatirialComponents}{MatirialIsConsumedS})";
                    }
                    return "S";
                }
                return $"M ({MatirialComponents}{MatirialIsConsumedS})";
            }
        }
    }

    public Spell(bool Manual) {
        SetName();
        SetLevel();
        SetComponents();
    }

    public void SetName() {
        while (true) {
            Console.WriteLine("Select Name: ");

            string? newName = Console.ReadLine()?.Trim();
            if (newName != null) {
                _name = newName;
                return;
            }
        }
    }
    
    public void SetLevel() {
        while (true) {
            Console.WriteLine("Spell Level:");

            try {
                    #pragma warning disable CS8604
                _level = ushort.Parse(Console.ReadLine());
                    #pragma warning restore CS8604
            }
            catch {
                continue;
            }
            break;
        }
    }
   
    public void SetComponents() {
        _verbal = GetBool("Is Verbal?");
        _somatic = GetBool("Is Somatic?");
        _matirial = GetBool("Is Matirial");
        if (_matirial) {
            _matirialIsConsumed = GetBool("Is The Matirial Consumed");
        }
    }

    private static bool GetBool(string Message) {
        while (true) {
            Console.WriteLine(Message + " (y/n)");
            string? response = Console.ReadLine()?.ToLower();
            if (response == "y") {
                return true;
            }
            else if (response == "n") {
                return false;
            }
        }
    }

    public Spell(int level = 0, string name = "") {
        _level = (ushort) level;
        _name = name;
    }

    public Spell(int level = 0, string name = "", bool somatic = false, bool verbal = false) {
        _level = (ushort) level;
        _name = name;
        _somatic = somatic;
        _verbal = verbal;
    }
    
    public Spell(int level = 0, string name = "", bool somatic = false, bool verbal = false,
     bool matirial = false, string components = " ", bool isConsumed = false) {
        _level = (ushort) level;
        _name = name;
        _somatic = somatic;
        _verbal = verbal;
        _matirial = matirial;
        _matirialComponents = components;
        _matirialIsConsumed = isConsumed;
    }

    public Spell(BinaryReader reader) {
        _name = BitManager.ReadVariableLengthString(reader).Trim();
        Console.WriteLine(Name);
        _level = reader.ReadByte();
        Console.WriteLine(Level);
        _somatic = BitManager.ByteToBoolean(reader.ReadByte());
        Console.WriteLine(Somatic);
        _verbal = BitManager.ByteToBoolean(reader.ReadByte());
        Console.WriteLine(Verbal);
        _matirial = BitManager.ByteToBoolean(reader.ReadByte());
        Console.WriteLine(Matirial);
        if (Matirial) {
            _matirialIsConsumed = BitManager.ByteToBoolean(reader.ReadByte());
        }
    }

    public void Display() {
        Console.WriteLine(Name);
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Components: {Components}");
    }

    public void save(BinaryWriter writer) {
        writer.Write('>');
        writer.Write((ushort) Name.Length);
        foreach (char letter in Name) {
            writer.Write(letter);
        }
        writer.Write(Level);
        writer.Write(Somatic);
        writer.Write(Verbal);
        writer.Write(Matirial);
        if (Matirial) {
            writer.Write(MatirialIsConsumed);
            // saving of the acctual matirials and the main body of spell not done TODO
        }
    }
}