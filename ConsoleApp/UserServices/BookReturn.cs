using StackTrack.ConsoleApp.AccountServices;
using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.UserServices;

class BookReturn
{
    public static double lateFeeMultiplier = 10.0;
    public static double timeBeforeLate = 10.0;

    public static void Interface()
    {
        MyStack.DisplayUserStack();
        System.Console.Write("Return Selection > ");
        BookReturnLogic(Console.ReadLine() ?? string.Empty);
    }

    public static void BookReturnLogic(string bookTitle)
    {
        var (status, fee) = LateFeeCalculator(bookTitle);

        switch (status)
        {
            case -1:
                BookNotFound();
                break;
            case 0:
                ReturnWithoutFee(bookTitle);
                break;
            case 1:
                ReturnWithFee(bookTitle, fee);
                break;
        }
    }

    public static void BookNotFound()
    {
        Console.Clear();
        System.Console.WriteLine("> Invalid Selection\n");
        return;
    }

    public static void ReturnWithoutFee(string bookTitle)
    {
        Console.Clear();
        System.Console.WriteLine($"> Thank you for returning {bookTitle} on time!\n");

        var actionResult = BookData.TryReturnBook(bookTitle);

        if (actionResult == BookData.ActionResult.Failure)
        {
            Console.Clear();
            System.Console.WriteLine($"> You don't have {bookTitle} checked out.\n");
        }
        else if (actionResult == BookData.ActionResult.InvalidSelection)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Selection.\n");
        }
        else
        {
            return;
        }
    }

    public static void ReturnWithFee(string bookTitle, double fee)
    {
        Console.Clear();
        System.Console.WriteLine($"> Thank you for returning {bookTitle}, You've Accrued {fee:C} in late fees.");
        System.Console.WriteLine($"> This bring your total balance to {UserData.QueryUserByFilter("Id", UserIdentification.currentUserID ?? "")[0].userBalance:C}\n");

        var actionResult = BookData.TryReturnBook(bookTitle);

        if (actionResult == BookData.ActionResult.Failure)
        {
            Console.Clear();
            System.Console.WriteLine($"> You don't have {bookTitle} checked out.\n");
        }
        else if (actionResult == BookData.ActionResult.InvalidSelection)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Selection.\n");
        }
        else
        {
            return;
        }
    }

    public static (int status, double fee) LateFeeCalculator(string bookTitle)
    {
        List<Book> books = BookData.QueryBooksByFilter("BookTitle", bookTitle);
        if (books == null || books.Count == 0 || books[0].CheckedOutAt == null)
        { return (-1, 0); }

        TimeSpan userPosession = DateTime.Now - books[0].CheckedOutAt.Value;
        double secondsLate = userPosession.TotalSeconds - timeBeforeLate;

        if (secondsLate > 0)
        {
            UserData.UpdateUserBalance(secondsLate * lateFeeMultiplier);
            return (1, secondsLate * lateFeeMultiplier);
        }
        else
        {
            return (0, 0);
        }
    }
}