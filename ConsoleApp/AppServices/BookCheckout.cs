using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;
using StackTrack.ConsoleApp.AccountServices;
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
                DisplayAllBooks();
                break;
            case 2:
                Console.Clear();
                System.Console.WriteLine("==Book Checkout==");
                System.Console.Write("Genre: ");
                BookData.QueryBooksByGenre(Console.ReadLine()?.ToString().ToLower().Trim() ?? "");
                break;
            case 3:
                Console.Clear();
                System.Console.WriteLine("==Book Checkout==");
                System.Console.Write("Author: ");
                BookData.QueryBooksByAuthor(Console.ReadLine()?.ToString().ToLower().Trim() ?? "");
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                return;
        }
    }

    public static void DisplayAllBooks()
    {
        List<Book> books = BookData.QueryAllBooks();

        Console.Clear();
        System.Console.WriteLine("All Books:");
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
        System.Console.WriteLine("Selection > ");
        var result = BookData.TryCheckoutBook(Console.ReadLine() ?? "");

        switch (result)
        {
            case BookData.CheckoutResult.Success:
                Console.Clear();
                System.Console.WriteLine("> Book Successfully Checked Out!\n");
                break;
            case BookData.CheckoutResult.NotFound:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection.\n");
                break;
            case BookData.CheckoutResult.AlreadyCheckedOut:
                Console.Clear();
                System.Console.WriteLine("> This Book Is Already Checked Out.\n");
                break;
        }
    }
}