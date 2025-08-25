namespace StackTrack.ConsoleApp.Menus;

class ServiceDashboard
{
    public static string? userInput;
    public static int userSelection;

    public static void ServiceDashboardDisplay()
    {
        do
        {
            // 1. Interface Header
            System.Console.WriteLine("==Home Screen==");

            // 2. Display user options
            System.Console.WriteLine("1. Check Out Books");
            System.Console.WriteLine("2. Return Books");
            System.Console.WriteLine("3. View Current Stack of Books");
            System.Console.WriteLine("4. View Current Balance");
            System.Console.WriteLine("5. View Terms and Conditions");
            System.Console.WriteLine("6. Exit");

            // 3. Take user input, trim it, and put it in lower case
            userInput = Console.ReadLine().ToLower().Trim();

            // 4. Parse user input for a number choice and store it in userSelection
            int.TryParse(userInput, out userSelection);

            // 5. Pass userSelection into ServiceDashBoardLogic to route the user
            ServiceDashboardLogic(userSelection);
        } while (userInput != "exit");
    }

    public static void ServiceDashboardLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                // Allow users to check out books.
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