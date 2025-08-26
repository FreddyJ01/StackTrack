using StackTrack.ConsoleApp.AppServices;

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
            Console.Clear();
            System.Console.WriteLine("==Service Dashboard==");

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
                Console.Clear();
                // 1. Take users to bookcheckout service
                BookCheckout.BookCheckoutInterface();
                break;
            case 2:
                // 2. Takes users to bookreturn service
                BookReturn.BookReturnInterface();
                break;
            case 3:
                // 3. Takes user to printuserstack service
                ViewStack.PrintUserStack();
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }
}