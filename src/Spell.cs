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
    private string _description = "";
    public string Description => _description;
    private string _range = "";
    public string Range => _range;

    public Spell() {
        SetName();
        SetLevel();
        SetComponents();
        SetDescription();
        SetRange();
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

    public void SetDescription() {
        _description = Helper.GetString("Spell Description");
    }

    public void SetRange() {
        _range = Helper.GetString("Spell Range");
    }

    public Spell(string? spell) {
        if (spell == null) {
            throw new Book.Exceptions.SpellException();
        }
        ushort indenter = ushort.Parse(spell.Substring(0,2));

        _name = spell.Substring(2, indenter);
        _level = ushort.Parse($"{spell[indenter + 2]}");
        _verbal = RWTools.IsTrue(spell[indenter + 3]);
        _somatic = RWTools.IsTrue(spell[indenter + 4]);
        _matirial = RWTools.IsTrue(spell[indenter + 5]);
        indenter += 6;
        if (Matirial) {
            _matirialIsConsumed = RWTools.IsTrue(spell[indenter]);
            ushort MCLength = ushort.Parse(spell.Substring(indenter + 1, 3));
            _matirialComponents = spell.Substring(indenter + 4, MCLength);
            indenter += (ushort) (MCLength + 4);
        }
        ushort rangeLength = ushort.Parse(spell.Substring(indenter, 2));
        _range = spell.Substring(indenter + 2, rangeLength);
        indenter += (ushort) (rangeLength + 2);
        _description = spell.Substring(indenter);
    }

    public override void Display() {
        Console.WriteLine(Name);
        Console.WriteLine($"Level: {Level}");
        Console.WriteLine($"Range: {Range}");
        Console.WriteLine($"Components: {Components}");
        Console.WriteLine($"\n{Helper.Format(Description)}");
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
            writer.Write(MatirialComponents.Length.ToString().Trim('0').PadLeft(3, '0'));
            writer.Write(MatirialComponents);
        }
        writer.Write(Range.Length.ToString().Trim('0').PadLeft(2, '0'));
        writer.Write(Range);
        writer.Write(Description);
        writer.Write('\n');
    }
}