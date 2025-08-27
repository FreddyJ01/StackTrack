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
        // 1. Create a temporary variable for holding the users book stack
        var userStack = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack;

        // 2. Display current stack to user
        System.Console.WriteLine("Current User Stack:");
        foreach (var pair in userStack)
        {
            System.Console.WriteLine($"{pair.Key}, {pair.Value}");
        }

        // 3. Prompt the user which book they would like to return
        System.Console.WriteLine("--");
        System.Console.Write("Return Selection > ");

        // 4. Take user input
        userBookSelection = Console.ReadLine();

        // 5. Passes user selection to BookReturnLogic
        BookReturnLogic(userBookSelection);
    }

    public static void BookReturnLogic(string userBookSelection)
    {
        // 1. Ensure the selected book is in the current users stack
        bookInUserStack = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.ContainsKey(userBookSelection);

        // 2. Alert the user they have not checked out the book if the book is not in their stack
        if (!bookInUserStack)
        {
            Console.Clear();
            System.Console.WriteLine($"> Return Failed - Invalid Selection\n");
            return;
        }

        // 3. Determine how long the user had the book checked out for to handle any late fees (10 Seconds For Demo)

        // Pulls the time the user checked the book out
        whenUserCheckedOut = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack[userBookSelection];

        // Determines how long the user had the book checked out
        userPosession = DateTime.Now - whenUserCheckedOut;

        // Handles Late Fees (10 Seconds)
        if (userPosession.TotalSeconds >= 10)
        {
            // Adds userBalance Total
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance += (userPosession.TotalSeconds - 10) * 10;

            // Informs user they have had the book too long
            Console.Clear();

            System.Console.WriteLine($"> You Had \"{userBookSelection}\" For {(userPosession.TotalSeconds - 10):f0} Seconds Past Your Return Time.");
            System.Console.WriteLine($"> Accrued Balance On \"{userBookSelection}\": ${((userPosession.TotalSeconds - 10) * 10):f2}");
            System.Console.WriteLine($"> Total Balance: ${(UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance):f2}\n");

            // Removes the book from the users stack
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.Remove(userBookSelection);

            // Returns user to ServiceDashboard
            return;
        }
        else
        {
            // Thanks the user for returning the book on time
            Console.Clear();
            System.Console.WriteLine($"> Return Succesful - {userBookSelection} returned on time.\n");

            // Removes the book from the users stack
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.Remove(userBookSelection);

            return;
        }
    }
}