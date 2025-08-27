using Microsoft.Extensions.DependencyModel;
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
        System.Console.Write("Book Selection > ");

        // 5. Takes user selection
        userBookSelection = Console.ReadLine();

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
            System.Console.WriteLine("> Invalid Book Selection\n");
            return;
        }
        //  Handles if the user already checked out that book
        else if (UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.ContainsKey(LibraryInventory.bookInventory[userBookSelectionIndex]))
        {
            Console.Clear();

            // Prompt the user that they already have that book checked out and when they checked it out
            System.Console.WriteLine($"> You already checked out {userBookSelection} on {UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack[LibraryInventory.bookInventory[userBookSelectionIndex]]}\n");
            return;

        }
        // Adds the book to the users book stack
        else
        {
            // Adds the book to the users booklist
            UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack.Add(LibraryInventory.bookInventory[userBookSelectionIndex], DateTime.Now);

            // Alerts the user that the checkout was successful
            Console.Clear();
            System.Console.WriteLine($"> Successfully Checked Out {LibraryInventory.bookInventory[userBookSelectionIndex]} at {UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack[userBookSelection]}\n");

            // Returns user to service dashboard
            return;
        }
    }
}