using System.Reflection;

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

    public T? SelectContent() {
        while (true) {
            Console.WriteLine($"Select {typeof(T)}");
            Console.WriteLine("0). return");
            ListedDisplay();
            
            string? response = Console.ReadLine();
            if (response == null) {
                continue;
            }
            switch (response) {
                case "0":
                    return null;
                default:
                    try {
                        return (T) this[int.Parse(response) - 1];
                    }
                    catch (IndexOutOfRangeException) {
                        continue;
                    }
            }
        }
    }
}