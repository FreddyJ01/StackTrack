using System.Security.Cryptography.X509Certificates;
using StackTrack.ConsoleApp.AppServices;

namespace StackTrack.ConsoleApp.Menus;

class ServiceDashboard
{
    public static void Interface()
    {
        int userSelection;
        do
        {
            System.Console.WriteLine("==Service Dashboard==");
            System.Console.WriteLine("1. Book Checkout");
            System.Console.WriteLine("2. Book Return");
            System.Console.WriteLine("3. My Stack");
            System.Console.WriteLine("4. My Balance");
            System.Console.WriteLine("5. Terms and Conditions");
            System.Console.WriteLine("6. Log Out");
            System.Console.WriteLine("--");
            System.Console.Write("Selection > ");

            ServiceDashboardLogic(int.TryParse(Console.ReadLine(), out userSelection) ? userSelection : 0);
        }
        while (userSelection != 6);
    }

    public static void ServiceDashboardLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                Console.Clear();
                BookCheckout.Interface();
                break;
            case 2:
                Console.Clear();
                BookReturn.Interface();
                break;
            case 3:
                Console.Clear();
                ViewStack.Interface();
                break;
            case 4:
                Console.Clear();
                BalanceView.Interface();
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