using System.IO.Compression;
using Accounts;
namespace ConsoleUI;

class MainMenu
{
    UserCreation userCreation = new UserCreation();
    UserAuthentication userAuthentication = new UserAuthentication();
    public void MainMenuDisplay()
    {
        string? userInput;
        int userChoice;
        do
        {
            Console.Clear();
            System.Console.WriteLine("======Main Menu======");
            System.Console.WriteLine("1. Login");
            System.Console.WriteLine("2. Create an Account");
            userInput = Console.ReadLine().ToLower().Trim();
            int.TryParse(userInput, out userChoice);
            MainMenuDisplayLogic(userChoice);

        } while (userInput != "exit");
    }

    public void MainMenuDisplayLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1: // User Authentication
                Console.Clear();
                userAuthentication.UserIdentification();
                break;
            case 2: // User Creation
                userCreation.AccountCreation();
                break;
        }
    }
}

class HomeScreen
{
    public void HomeScreenDisplay()
    {
        string? userInput;
        int userChoice;
        do
        {
            System.Console.WriteLine("==========Home Screen==========");
            System.Console.WriteLine("1. Check Out Books");
            System.Console.WriteLine("2. Return Books");
            System.Console.WriteLine("3. View Current Stack of Books");
            System.Console.WriteLine("4. View Current Balance");
            System.Console.WriteLine("5. View Terms and Conditions");
            System.Console.WriteLine("6. Log Out");
            userInput = Console.ReadLine();
            int.TryParse(userInput, out userChoice);
        } while (userInput != "exit");
    }

    void HomeScreenLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                // Call to DisplayBooks() -> Display Strings of Available books by genre
                // Take user input to allow a user to select a book -> allow the user to either be done or check out more books (recursive call)
                // WHen a user checks out a book we need to log the time and give them a message telling them the date when they need to return the book by
                break;
            case 2:
                // Call to RetrunBooks() -> Display current stack of books
                // Take user input to allow them to return a specific book
                // Handle any late fees that the user might have and take them to a payment page or allow them to add it to their balance
                // Ask if they have more returns if not back to the home screen if they do then recursion
                break;
            case 3:
                // Shows the user their current stack of books and allow them to select a book and see how long they've had it checked out.
                // Maybe down the road allow users to take notes on the book and acces their notes on specific books, this would mean storing historical checkouts so they dont lose their notes when they return a book
                break;
            case 4:
                // Show the user their current balance and allow them to make a payment
                break;
            case 5:
                // Shows the user our late fee terms and other policies
                break;
            case 6:
                // Logs the user out and takes them back to the Main Menu
                break;
        }
    }
}