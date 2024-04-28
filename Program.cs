class Program {
    //TODO Clean this up
    
        #pragma warning disable CS8618
    public static BookSet bookSet;
        #pragma warning restore CS8618
    public static void Main() {
        bookSet = new();
        while (true) {
            Console.WriteLine("\n1). Save Books");
            Console.WriteLine("2). Load Books");
            Console.WriteLine("3). View Book List");
            Console.WriteLine("4). Add Book");
            Console.WriteLine("5). Edit Books");
            Console.WriteLine("6). Search Books");
            Console.WriteLine("7). Quit");

            string? response = Console.ReadLine();
            MainLoopCaseSwitch(response);
        }
    }

    private static void MainLoopCaseSwitch(string? option) {
        switch (option) {
            case "1":
            bookSet.Save(); return;
            case "2":
            bookSet = new(); return;
            case "3":
            bookSet.Display(); return;
            case "4":
            bookSet.Add(new()); return;
            case "5":
            BookEditLoop(bookSet.SelectBook()); return;
            case "6":
            SearchMenu(); return;
            case "7":
            Environment.Exit(1); return;
        }
        Console.Clear();
    }


    private static void BookEditLoop(Book? book) {
        if (book == null) {
            return;
        }
        while (true) {
            Console.Clear();
            Console.WriteLine("Select what in the book you wish to edit");
            Console.WriteLine("1). Edit Book Name");
            Console.WriteLine("2). Fix Book");
            Console.WriteLine("3). Edit Spell");
            Console.WriteLine("4). Add Spell");
            Console.WriteLine("5). Return");
            
            string? response = Console.ReadLine();
            if (BookEditLoop(response, book)) {
                return;
            }
        }
    }

    private static bool BookEditLoop(string? response, Book book) {
        switch (response) {
            case "1":
            book.ManualName(); break;
            case "2":
            book.Fix(); break;
            case "3":
            SpellEditSelectLoop(book); break;
            case "4":
            book.AddSpell(); break;
            case "5":
            return true;
        }
        return false;
    }


    private static void SpellEditSelectLoop(Book book) {
        while (true) {
            if (SpellEditLoop((Spell?) GetContentFromBook(book, "1"))) {
                return;
            }
        }
    }

    private static bool SpellEditLoop(Spell? spell) {
        if (spell == null) {
            return true;
        }

        while (true) {
            Console.Clear();
            Console.WriteLine("What part of the spell do you want to edit");
            Console.WriteLine("1). Name");
            Console.WriteLine("2). Level");
            Console.WriteLine("3). Components");
            Console.WriteLine("4). Range");
            Console.WriteLine("5). Description");
            Console.WriteLine("6). return");

            string? response = Console.ReadLine();
            if (SpellEditLoopSwitchCase(spell, response)) {
                return false;
            }
        }
    }

    private static bool SpellEditLoopSwitchCase(Spell spell, string? response) {
        switch (response) {
            case "1":
            spell.SetName(); break;
            case "2":
            spell.SetLevel(); break;
            case "3":
            spell.SetComponents(); break;
            case "4":
            spell.SetRange(); break;
            case "5":
            spell.SetDescription(); break;
            case "6":
            return true;
        }
        return false;
    }

    public static void SearchMenu() {
        while (true) {
            Book? book = bookSet.SelectBook();
            if (book == null) {
                return;
            }
            string? contentType = SelectContentType();
            if (contentType == "2") {
                continue;
            }
            while (true) {
                Content? content = GetContentFromBook(book, contentType);
                if (content == null) {
                    break;
                }
                content.Display();
            }
        }
    }

    public static Content? GetContentFromBook(Book book, string? contentType) {
        switch (contentType) {
            case "1":
                return book.SManager.SelectContent();
            default:
                return null;
        }

    }

    public static string? SelectContentType() {
        while (true) {
            Console.WriteLine("Select Content Type");
            Console.WriteLine("1). Spell");
            Console.WriteLine("2). Return");

            string? response = Console.ReadLine();
            if (response == null) {
                continue;
            }
            return response;
        }
    }
}