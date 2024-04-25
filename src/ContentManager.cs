class Manager<T> : List<Content> 
    where T : Content, new()
{
    public void DisplayAll() {
        if (Count == 0) {
            return;
        }
        foreach (Content item in this) {
            item.Display();
        }
    }
    
    public void SaveAll(StreamWriter writer) {
        foreach (Spell spell in this) {
            spell.Save(writer);
        }
    }

    public void AddNew() {
        Add(new T());
    }

    public void ListedDisplay() {
        for (int i = 0; i < Count; i++) {
            Console.WriteLine($"{i + 1}). {this[i].Name}");
        }
    }
}