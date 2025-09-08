using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.AdminServices;

class ManageLibrary
{
    public static void Interface()
    {
        System.Console.WriteLine("==Manage Library==");
        System.Console.WriteLine("1. View Library Inventory");
        System.Console.WriteLine("2. Add Book To Library");
        System.Console.WriteLine("3. Remove Book From Library");
        System.Console.WriteLine("4. Edit Book");
        System.Console.WriteLine("5. Exit");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");

        int.TryParse(Console.ReadLine() ?? "", out int selection);

        switch (selection)
        {
            case 1:
                Console.Clear();
                LibraryInventory();
                break;
            case 2:
                Console.Clear();
                AddBook();
                break;
            case 3:
                Console.Clear();
                RemoveBook();
                break;
            case 4:
                Console.Clear();
                EditBook();
                break;
            case 5:
                Console.Clear();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection.\n");
                break;
        }
    }

    static string? PromptUser(string prompt)
    {
        System.Console.Write(prompt);
        return Console.ReadLine();
    }

    public static void LibraryInventory()
    {
        List<Book> books = new List<Book>();
        books = BookData.QueryAllBooks();

        foreach (Book book in books)
        {
            System.Console.WriteLine($"{book.BookID}, {book.BookTitle}, By {book.BookAuthor} - Genre: {book.BookGenre}");
        }
        System.Console.WriteLine("--");
        Console.ReadLine();
        Console.Clear();
    }

    public static void AddBook()
    {
        string bookTitle = PromptUser("Book Title > ");
        string bookAuthor = PromptUser("Book Author > ");
        string bookGenre = PromptUser("Book Genre > ");

        bool actionSuccess = BookData.TryAddBook(Guid.NewGuid().ToString(), bookTitle, bookAuthor, bookGenre);

        if (!actionSuccess)
        {
            Console.Clear();
            System.Console.WriteLine("> Book Addition Failed.\n");
            return;
        }

        Console.Clear();
        System.Console.WriteLine("> Book Succesfully Added.\n");
    }

    public static void RemoveBook()
    {
        string bookId = PromptUser("Book Id > ");

        bool actionSuccess = BookData.TryRemoveBook(bookId);

        if (!actionSuccess)
        {
            Console.Clear();
            System.Console.WriteLine("> Book Removal Failed.\n");
            return;
        }
        
        Console.Clear();
        System.Console.WriteLine("> Book Succesfully Removed.\n");
    }

    public static void EditBook()
    {
        List<Book> books = new List<Book>();
        string bookId = PromptUser("Book Id > ");

        books = BookData.QueryBooksByFilter("BookID", bookId) ?? null;

        if (books == null)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Selection.\n");
            return;
        }

        Console.Clear();
        foreach (Book book in books)
        {
            System.Console.WriteLine($"{book.BookID}, {book.BookTitle}, By {book.BookAuthor} - Genre: {book.BookGenre}");
        }
        System.Console.WriteLine("--");

        bool actionSuccess = BookData.TryRemoveBook(bookId);

        if (!actionSuccess)
        {
            Console.Clear();
            System.Console.WriteLine("> Book Edit Failed.\n");
            return;
        }
        
        string bookTitle = PromptUser("Update Title > ");
        string bookAuthor = PromptUser("Update Author > ");
        string bookGenre = PromptUser("Update Genre > ");

        actionSuccess = BookData.TryAddBook(Guid.NewGuid().ToString(), bookTitle, bookAuthor, bookGenre);

        if (!actionSuccess)
        {
            Console.Clear();
            System.Console.WriteLine("> Book Edit Failed.\n");
            return;
        }

        Console.Clear();
        System.Console.WriteLine("> Book Succesfully Edited.\n");
    }
}