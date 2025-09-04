using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;
namespace StackTrack.ConsoleApp.AppServices;

class BookCheckout
{
    public static void Interface()
    {
        System.Console.WriteLine("==Book Checkout==");
        System.Console.WriteLine("1. All Books");
        System.Console.WriteLine("2. Filter By Genre");
        System.Console.WriteLine("3. Filter By Author");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");

        BookCheckoutLogic(int.TryParse(Console.ReadLine(), out int tryParse) ? tryParse : 0);
    }

    public static void BookCheckoutLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                AllBooks();
                break;
            case 2:
                BooksByGenre();
                break;
            case 3:
                BooksByAuthor();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                return;
        }
    }

    public static void AllBooks()
    {
        List<Book> books = BookData.QueryAllBooks();

        Console.Clear();
        System.Console.WriteLine("All Books:");
        BookDisplay(books);

        CheckoutResult(BookData.TryCheckoutBook(Console.ReadLine() ?? string.Empty));
    }

    public static void BooksByGenre()
    {
        Console.Clear();
        System.Console.WriteLine("==Book Checkout==");
        System.Console.Write("Genre: ");
        string genre = Console.ReadLine()?.ToString().ToLower().Trim() ?? "";

        List<Book> books = BookData.QueryBooksByGenre(genre);

        Console.Clear();
        System.Console.WriteLine($"{char.ToUpper(genre[0]) + genre.Substring(1)} Books:");
        BookDisplay(books);

        CheckoutResult(BookData.TryCheckoutBook(Console.ReadLine() ?? string.Empty));
    }

    public static void BooksByAuthor()
    {
        Console.Clear();
        System.Console.WriteLine("==Book Checkout==");
        System.Console.Write("Author: ");
        string author = Console.ReadLine()?.ToString().ToLower().Trim() ?? "";

        List<Book> books = BookData.QueryBooksByAuthor(author);

        Console.Clear();
        System.Console.WriteLine($"Books By {char.ToUpper(author[0]) + author.Substring(1)}:");
        BookDisplay(books);

        CheckoutResult(BookData.TryCheckoutBook(Console.ReadLine() ?? string.Empty));
    }

    public static void BookDisplay(List<Book> books)
    {
        foreach (var book in books)
        {
            string output = $"> {book.BookTitle}, By {book.BookAuthor}";

            if (!String.IsNullOrEmpty(book.CheckedOutByID))
            {
                output += $" | {UserData.QueryUsernameById(book.CheckedOutByID)}, {book.CheckedOutAt:MMM-dd-yyyy @ hh:mm tt}";
            }

            System.Console.WriteLine(output);
        }
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");
    }

    public static void CheckoutResult(BookData.ActionResult result)
    {
        switch (result)
        {
            case BookData.ActionResult.Success:
                Console.Clear();
                System.Console.WriteLine("> Book Successfully Checked Out!\n");
                break;
            case BookData.ActionResult.InvalidSelection:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection.\n");
                break;
            case BookData.ActionResult.Failure:
                Console.Clear();
                System.Console.WriteLine("> This Book Is Already Checked Out.\n");
                break;
        }
    } 
}