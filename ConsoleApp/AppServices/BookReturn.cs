using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using StackTrack.ConsoleApp.UserServices;

namespace StackTrack.ConsoleApp.AppServices;

class BookReturn
{
    public static string? userBookSelection;
    public static bool exitStatement;
    public static bool bookInUserStack;
    public static DateTime whenUserCheckedOut;
    public static TimeSpan userPosession;

    public static void BookReturnInterface()
    {
        // 1. Show the user their current stack
        ViewStack.PrintUserStack();

        // 2. Console Formatting
        System.Console.WriteLine("Which book would you like to return?");
        System.Console.WriteLine("--");

        // 3. Take user input
        userBookSelection = Console.ReadLine();

        // 4. Allows user to exit interface
        exitStatement = userBookSelection.ToLower().Trim() == "exit";
        if (exitStatement) { return; }

        // 5. Passes user selection to BookReturnLogic
        BookReturnLogic(userBookSelection);
    }

    public static void BookReturnLogic(string userBookSelection)
    {
        // 1. Ensure the selected book is in the current users stack
        bookInUserStack = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.ContainsKey(userBookSelection);

        // 2. Alert the user they have not checked out the book if the book is not in their stack
        if (!bookInUserStack)
            System.Console.WriteLine($"You don't have {userBookSelection} checked out.");

        // 3. Determine how long the user had the book checked out for to handle any late fees (10 Seconds For Demo)

        // Pulls the time the user checked the book out
        whenUserCheckedOut = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack[userBookSelection];

        // Determines how long the user had the book checked out
        userPosession = DateTime.Now - whenUserCheckedOut;

        // Handles Late Fees (10 Seconds)
        if (userPosession.Seconds >= 10)
        {
            // Informs user they have had the book too long
            System.Console.WriteLine($"You had this book for {userPosession.Seconds - 10} seconds longer than you should have.");

            // Adds to user balance total
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance += (userPosession.Seconds - 10) * 10;

            // Informs user how much they accrued in late fees on this book and their new total balance
            System.Console.WriteLine($"\nAt $10 a second your total accrued balance on this book is ${(userPosession.Seconds - 10) * 10}, bringing your total balance to ${UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance}");

            // Removes the book from the users stack
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.Remove(userBookSelection);

            // Waits for user to ackowledge message
            Console.ReadLine();
        }
        else
        {
            // Thanks the user for returning the book on time
            System.Console.WriteLine($"Thank you for returning {userBookSelection} on time, have a great day!");

            // Removes the book from the users stack
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.Remove(userBookSelection);

            // Waits for user to ackowledge message
            Console.ReadLine();
        }

        // 4. Remove the book from the users stack

        // 5. End this logic
    }
}