using StackTrack.ConsoleApp.AppServices;

namespace StackTrack.ConsoleApp.Menus;

class ServiceDashboard
{
    public static int userSelection;

    public static void ServiceDashboardDisplay()
    {
        do
        {
            // 1. Interface Header
            System.Console.WriteLine("==Service Dashboard==");

            // 2. Display user options
            System.Console.WriteLine("1. Book Checkout");
            System.Console.WriteLine("2. Book Return");
            System.Console.WriteLine("3. My Stack");
            System.Console.WriteLine("4. My Balance");
            System.Console.WriteLine("5. Terms and Conditions");
            System.Console.WriteLine("6. Exit");
            System.Console.WriteLine("--");
            System.Console.Write("Selection > ");

            // 3. Take user input, parse for selection
            int.TryParse(Console.ReadLine(), out userSelection);

            // 4. Pass userSelection into ServiceDashBoardLogic to route the user
            ServiceDashboardLogic(userSelection);
        }
        while (userSelection != 6);
    }

    public static void ServiceDashboardLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                Console.Clear();
                BookCheckout.BookCheckoutInterface();
                break;
            case 2:
                Console.Clear();
                BookReturn.BookReturnInterface();
                break;
            case 3:
                Console.Clear();
                ViewStack.PrintUserStack();
                break;
            case 4:
                Console.Clear();
                BalanceView.BalanceViewInterface();
                break;
            case 5:
                Console.Clear();
                TermsConditions.TermsConditionsDisplay();
                break;
            case 6:
                Console.Clear();
                System.Console.WriteLine("> Logged Out\n");
                return;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                return;
        }
    }
}