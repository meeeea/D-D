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
                Console.WriteLine($"Issue regarding the book ID of {file.Name}.");
            }
        }
        foreach (Book book in BookSet) {
            Console.WriteLine($"{book.Name}: {book.ID}");
        }
    }
}