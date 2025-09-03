using System.Security.Cryptography.X509Certificates;

namespace StackTrack.ConsoleApp.AdminServices;

class AdminServiceDashboard
{
    public static int userSelection;
    public static string prompt(string message) { System.Console.WriteLine(message); return Console.ReadLine(); }

    public static void AdminServiceDashboardDisplay()
    {
        do
        {
            // Interface Header & Options
            System.Console.WriteLine("==Admin Service Dashboard==");
            System.Console.WriteLine("1. Add Books To Library (Beta)");
            System.Console.WriteLine("6. Log Out");
            System.Console.WriteLine("--");
            System.Console.Write("Selection > ");

            // Take admin input, parse for selection
            AdminServiceDashboardLogic(int.TryParse(Console.ReadLine(), out userSelection) ? userSelection : 0);
        }
        while (userSelection != 6);
    }

    public static void AdminServiceDashboardLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                Console.Clear();
                LibraryManagement.AddBookToLibrary(Guid.NewGuid().ToString(), prompt("Enter Book Title:"), prompt("Enter Author Name:"), prompt("Enter Genre:"));
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