using StackTrack.ConsoleApp.Menus;
using StackTrack.ConsoleApp.Models;
using StackTrack.ConsoleApp.UserServices;

namespace StackTrack.ConsoleApp.AppServices;

class BookCheckout
{
    // Variables used to recieve user book selection
    public static string? userBookSelection;
    public static bool exitStatement;
    public static int userBookSelectionIndex;

    public static void BookCheckoutInterface()
    {
        // 1. Interface Header
        System.Console.WriteLine("Available Books:");

        // 2. Print all available books
        foreach (string book in LibraryInventory.bookInventory)
        {
            Console.WriteLine(book);
        }

        // 3. Console Formatting
        System.Console.WriteLine("--");

        // 4. Prompt user to select a book to checkout
        System.Console.WriteLine("Select a Book:");

        // 5. Takes user selection
        userBookSelection = Console.ReadLine();

        // 6. Allows user to exit interface
        exitStatement = userBookSelection.ToLower().Trim() == "exit";
        if (exitStatement) { return; }

        // 7. Passes user selection to BookCheckoutLogic
        BookCheckoutLogic(userBookSelection);
    }

    public static void BookCheckoutLogic(string userBookSelection)
    {
        // 1. Find the book in the array to ensure the selection is valid
        userBookSelectionIndex = Array.IndexOf(LibraryInventory.bookInventory, userBookSelection);

        // 2. Checks the book out to the user and logs the time if the book exists | Alerts the user that the book does not exist and takes them back to PrintBookInventory if the book doesn't exist
        if (userBookSelectionIndex == -1)
        {
            // Alert the User that Book is Unavailable
            Console.Clear();
            System.Console.WriteLine("Sorry, That Book is Currently Unavailable.\n");

            // Recursion back to BookCheckoutInterface
            BookCheckoutInterface();
        }
        //  Handles if the user already checked out that book
        else if (UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.ContainsKey(LibraryInventory.bookInventory[userBookSelectionIndex]))
        {
            Console.Clear();

            // Prompt the user that they already have that book checked out and when they checked it out
            System.Console.WriteLine($"You already checked this book out on {UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack[LibraryInventory.bookInventory[userBookSelectionIndex]]}\n");

            // Redirect the user back to print book inventory
            BookCheckoutInterface();

        }
        // Adds the book to the users book stack
        else
        {
            // Adds the book that the user selected to the current logged in users book stack alongside the checkout time. Then returns user to the Service Dashboard
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.Add(LibraryInventory.bookInventory[userBookSelectionIndex], DateTime.Now);

            // Shows the user their current stack
            ViewStack.PrintUserStack();
        }
    }
}