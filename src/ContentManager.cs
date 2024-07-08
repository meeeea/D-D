using System.Net.Mime;
using OneOf;

class Manager<T> : List<Content> 
    where T : Content, new() {

    public static implicit operator Manager<T>?(Content.OneOfManager? _) {
        if (_ == null) {
            return null;
        }
        return _;
    }
    public void DisplayAll() {
        if (Count == 0) {
            return;
        }
        foreach (Content item in this) {
            item.Display();
        }
    }
    
    public void SaveAll(StreamWriter writer) {
        foreach (Content content in this) {
            content.Save(writer);
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
                    catch (ArgumentOutOfRangeException) {
                        continue;
                    }
                    catch (FormatException) {
                        continue;
                    }
            }
        }
    }

    public T? SelectContentByID(ushort ID) {
        foreach (T item in this) {
            if (item.ID == ID) {
                return item;
            }
        }
        return null;
    }

    public Manager<T> Fix() {
        if (Count < 1) {
            return new();
        }
        Manager<T> contents = new(){this[0]};
        foreach (T item in GetRange(1, Count - 1)) {
            bool inserted = false;
            for(int i = 0; i < contents.Count; i++) {
                if (item <= contents[i]) {
                    Console.WriteLine(item.Name);
                    contents.Insert(i, item);
                    inserted = true;
                    break;
                }
            }
            if (!inserted) {
                contents.Add(item);
            }
        }
        contents.Reverse();
        return contents;
    }
}