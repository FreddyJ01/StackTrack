using StackTrack.ConsoleApp.Data;

namespace StackTrack.ConsoleApp.AdminServices;

class ManageLibrary
{
    public static void Interface()
    {
        System.Console.WriteLine("==Manage Library==");
        System.Console.WriteLine("1. Add Book To Library");
        System.Console.WriteLine("2. Remove Book From Library");
        System.Console.WriteLine("3. Edit Book");
        System.Console.WriteLine("4. Exit");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");

        int.TryParse(Console.ReadLine() ?? "", out int selection);

        switch (selection)
        {
            case 1:
                Console.Clear();
                AddBook();
                break;
            case 2:
                Console.Clear();
                RemoveBook();
                break;
            case 3:
                Console.Clear();
                EditBook();
                break;
            case 4:
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
    public static void AddBook()
    {
        string bookTitle = PromptUser("Book Title > ");
        string bookAuthor = PromptUser("Book Author > ");
        string bookGenre = PromptUser("Book Genre > ");

        bool actionSuccess = BookData.TryAddBook(bookTitle, bookAuthor, bookGenre);

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
        string bookTitle = PromptUser("Book Title > ");

        bool actionSuccess = BookData.TryRemoveBook(bookTitle);

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
        
    }
}