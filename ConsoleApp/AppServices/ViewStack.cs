using StackTrack.ConsoleApp.AccountServices;
using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.AppServices;

class ViewStack
{
    public static void Interface()
    {
        DisplayUserStack();
        Console.ReadLine();
        Console.Clear();
    }
    
    public static void DisplayUserStack()
    {
        List<Book> books = BookData.QueryBooksByCheckedOutByID(UserIdentification.currentUserID ?? string.Empty);

        System.Console.WriteLine("Current User Stack:");
        foreach (var book in books)
        {
            System.Console.WriteLine($"{book.BookTitle}, By {book.BookAuthor} | {book.CheckedOutAt}");
        }
        System.Console.WriteLine("--");
    }
}