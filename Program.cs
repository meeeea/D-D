using System.Collections;
using System.Text;
using System.Text.Unicode;

class Program {
    public static List<Book> BookSet = new();
    public static void Main() {
        OpenBookSet();
    }

    private static void OpenBookSet() {
        DirectoryInfo d = new DirectoryInfo(".\\books"); //Assuming Test is your Folder

        FileInfo[] Files = d.GetFiles("*.book"); //Getting Text files

        foreach(FileInfo file in Files )
        {
            try {
                BookSet.Add(new Book(file.Name));
            }
            catch (Book.Exceptions.IDException) {
                Console.WriteLine($"Failed to Open {file.Name} due to IDError");
                BookSet.RemoveAt(BookSet.Count - 1);
            }
            catch (Book.Exceptions.NameException) {
                Console.WriteLine($"Failed To Open {file.Name} due to NameError");
                BookSet.RemoveAt(BookSet.Count - 1);
            }
            catch (Book.Exceptions.SpellException) {
                Console.WriteLine($"Failure to Open {file.Name} due to SpellError");
                BookSet.RemoveAt(BookSet.Count - 1);
            }
        }
        foreach (Book book in BookSet) {
            Console.WriteLine($"{book.Name}: {book.ID}");
            book.SManager.DisplayAll();
            book.SaveBook();
        }

    }
}