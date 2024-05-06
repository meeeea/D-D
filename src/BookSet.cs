class BookSet : List<Book> {
    public BookSet(bool manual) {
        while (true) {
            Console.WriteLine("Options:");
            Console.WriteLine("1). Add Book");
            Console.WriteLine("2). End");

            string? response = Console.ReadLine();
            if (response != null) {
                switch (response) {
                    case "1":
                    AddManualBook();break;
                    case "2":
                    return;
                }
            }
        }
    }

    private void AddManualBook() {
        Add(new());
    }

    public BookSet(string folder = ".\\books") {
        DirectoryInfo d = new DirectoryInfo(folder); //Assuming Test is your Folder

        FileInfo[] Files = d.GetFiles("*.book"); //Getting Text files

        foreach(FileInfo file in Files )
        {
            try {
                Add(new Book(file.Name));
            }
            catch (Book.Exceptions.IDException) {
                Console.WriteLine($"Failed to Open {file.Name} due to IDError");
            }
            catch (Book.Exceptions.NameException) {
                Console.WriteLine($"Failed To Open {file.Name} due to NameError");
            }
            catch (Book.Exceptions.SpellException) {
                Console.WriteLine($"Failure to Open {file.Name} due to SpellError");
            }
            catch (Book.Exceptions.MissingFileException) {
                Console.WriteLine($"Faiure to Open {file.Name} due to it not existing.");
            }
        }
    }

    public void Display() {
        foreach (Book book in this) {
            book.Display();
        }
    }

    public Book? SelectBookByID(ushort ID) {
        foreach (Book book in this) {
            if (book.ID == ID) {
                return book;
            }
        }
        return null;
    }

    public Book? SelectBook() {
        while (true) {
            Console.WriteLine("Select Book");
            Console.WriteLine("0). return");
            Display();
            
            string? response = Console.ReadLine();
            if (response == null) {
                continue;
            }
            switch (response) {
                case "0":
                    return null;
                default:
                    try {
                        return this[int.Parse(response) - 1];
                    }
                    catch (ArgumentOutOfRangeException) {
                        continue;
                    }
            }
        }
    }

    public void Save() {
        foreach (Book book in this) {
            book.SaveBook();
        }
    }
}