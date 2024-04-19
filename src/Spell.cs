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
        _name = BitManager.BytesToString(reader.ReadBytes(36)).Trim();
        _level = reader.ReadByte();
        _somatic = BitManager.ByteToBoolean(reader.ReadByte());
        _verbal = BitManager.ByteToBoolean(reader.ReadByte());
        _matirial = BitManager.ByteToBoolean(reader.ReadByte());
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
        foreach (char letter in Name.PadRight(36)) {
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