class SpellManager {
    private List<Spell> spells = new();
    public Spell this[int index] => spells[index];

    public bool Add(BinaryReader reader) {
        try {
            while (true) {
                bool shouldGo = BitManager.ByteToBoolean(reader.ReadByte());
                if (shouldGo) {
                    spells.Add(new(reader));
                }
                else {break;}
            }
        }
        catch {
            throw new Book.Exceptions.SpellException();
        }
        return true;
    }

    public void DisplayAll() {
        foreach (Spell spell in spells) {
            spell.Display();
        }
    }

    public void ListedDisplay() {
        for (int i = 0; i < spells.Count; i++) {
            Console.WriteLine($"{i + 1}). {spells[i].Name}");
        }
    }

    public void SaveAll(BinaryWriter writer) {
        foreach (Spell spell in spells) {
            spell.save(writer);
        }
    }
}