class Spell : Content {
    private ushort _level = 0;
    public ushort Level => _level;
    public string _name = "";
    public override string Name => _name;
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

    public Spell() {
        SetName();
        SetLevel();
        SetComponents();
    }

    public void SetName() {
        _name = Helper.GetString("Spell Name");
    }
    
    public void SetLevel() {
        _level = (ushort) Helper.GetInt("Spell Level");
    }
   
    public void SetComponents() {
        _verbal = Helper.GetBool("Is Verbal?");
        _somatic = Helper.GetBool("Is Somatic?");
        _matirial = Helper.GetBool("Is Matirial");
        if (_matirial) {
            _matirialComponents = Helper.GetString("Spell Components: ");
            _matirialIsConsumed = Helper.GetBool("Is The Matirial Consumed");
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

    public Spell(string? spell) {
        if (spell == null) {
            throw new Book.Exceptions.SpellException();
        }
        ushort nameLength = ushort.Parse(spell.Substring(0,2));
        _name = spell.Substring(2, nameLength);
        _level = ushort.Parse($"{spell[nameLength + 2]}");
        _verbal = RWTools.IsTrue(spell[nameLength + 3]);
        _somatic = RWTools.IsTrue(spell[nameLength + 4]);
        _matirial = RWTools.IsTrue(spell[nameLength + 5]);
        if (Matirial) {
            _matirialIsConsumed = RWTools.IsTrue(spell[nameLength + 6]);
        }
    }

    public override void Display() {
        Console.WriteLine(Name);
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Components: {Components}");
    }

    public override void Save(StreamWriter writer) {
        writer.Write('S');
        writer.Write(Name.Length.ToString().Trim('0').PadLeft(2, '0'));
        writer.Write(Name);
        writer.Write(Level.ToString().Trim('0').PadLeft(1, '0'));
        writer.Write(RWTools.BoolToChar(Somatic));
        writer.Write(RWTools.BoolToChar(Verbal));
        writer.Write(RWTools.BoolToChar(Matirial));
        if (Matirial) {
            writer.Write(RWTools.BoolToChar(MatirialIsConsumed));
            // saving of the acctual matirials and the main body of spell not done TODO
        }
        writer.Write('\n');
    }
}