class Program {
        #pragma warning disable CS8618
    public static BookSet bookSet;
        #pragma warning restore CS8618
    public static void Main() {
        bookSet = new(true);
        while (true) {
            Console.WriteLine("1). Save Books");
            Console.WriteLine("2). View Books");
            Console.WriteLine("3). Edit Books");
            Console.WriteLine("4). Quit");

            string? response = Console.ReadLine();
            MainLoopCaseSwitch(response);
        }
    }

    private static void MainLoopCaseSwitch(string? option) {
        switch (option) {
            case "1":
            bookSet.Save(); return;
            case "2":
            bookSet.DisplayAll(); return;
            case "3":
            EditMenu(); return;
            case "4":
            Environment.Exit(1); return;
        }
        Console.Clear();
    }

    private static void EditMenu() {
        while (true) {
            Console.Clear();
            Console.WriteLine("Select Book");
            Console.WriteLine("0). return");
            bookSet.Display();
            
            string? response = Console.ReadLine();
            if (response != null) {
                if (EditLoopSwitchCase(response)) {
                    return;
                }
            }
        }
    }

    private static bool EditLoopSwitchCase(string response) {
        if (response == "0") {
            return true;
        }
        try {
            BookEditLoop(bookSet.SelectBookByID(ushort.Parse(response)));
        }
        catch {
            return false;
        }
        return false;
    }

    private static void BookEditLoop(Book? book) {
        if (book == null) {
            return;
        }
        while (true) {
            Console.Clear();
            Console.WriteLine("Select what in the book you wish to edit");
            Console.WriteLine("1). spell");
            Console.WriteLine("2). return");
            
            string? response = Console.ReadLine();
            if (BookEditLoop(response, book)) {
                return;
            }
        }
    }

    private static bool BookEditLoop(string? response, Book book) {
        switch (response) {
            case "1":
            SpellEditSelectLoop(book); break;
            case "2":
            return true;
        }
        return false;
    }

    private static void SpellEditSelectLoop(Book book) {
        while (true) {
            Console.Clear();
            Console.WriteLine("Select which spell to edit");
            Console.WriteLine("0). return");
        
            book.SpellDisplay();

            string? response = Console.ReadLine();
            if (SpellEditSelectLoopSwitchCase(book, response)) {
                return;
            }
        }
    }

    private static bool SpellEditSelectLoopSwitchCase(Book book, string? response) {
        if (response == null) {
            return false;
        }
        switch (response) {
            case "0":
            return true;
            default:
            SpellEditLoop(book.SManager[ushort.Parse(response) - 1]); break;
        }
        return false;
    }

    private static void SpellEditLoop(Spell spell) {
        while (true) {
            Console.Clear();
            Console.WriteLine("What part of the spell do you want to edit");
            Console.WriteLine("1). Name");
            Console.WriteLine("2). Level");
            Console.WriteLine("3). Components");
            Console.WriteLine("4). return");

            string? response = Console.ReadLine();
            if (SpellEditLoopSwitchCase(spell, response)) {
                return;
            }
        }
    }

    private static bool SpellEditLoopSwitchCase(Spell spell, string? response) {
        switch (response) {
            case "1":
            spell.EditName(); break;
            case "2":
            spell.EditLevel(); break;
            case "3":
            spell.EditComponents(); break;
            case "4":
            return true;
        }
        return false;
    }
}