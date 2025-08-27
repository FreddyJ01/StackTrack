using StackTrack.ConsoleApp.UserServices;
namespace StackTrack.ConsoleApp.Menus;

class Home
{
    public static int userSelection;

    public static void HomeDisplay()
    {
        do
        {
            // 1. Interface Header
            System.Console.WriteLine("==StackTrack==");

            // 2. Display user options
            System.Console.WriteLine("1. Log In");
            System.Console.WriteLine("2. Create Account");
            System.Console.WriteLine("3. Exit");
            System.Console.WriteLine("--");
            System.Console.Write("Selection > ");

            // 3. Take user input, parse for selection
            int.TryParse(Console.ReadLine(), out userSelection);

            // 5. Passes userSelection to HomeLogic to route the user
            HomeLogic(userSelection);
        }
        while (userSelection != 3);
    }

    public static void HomeLogic(int userSelection)
    {
        switch (userSelection)
        {
            // Case 1 -> Takes User To UserIdentification
            case 1:
                Console.Clear();
                UserIdentification.IdentificationInterface();
                break;
            // Case 2 -> Takes User To UserCreation
            case 2:
                Console.Clear();
                UserCreation.AccountCreationInterface();
                break;
            // Case 3 -> Terminates The Application
            case 3:
                return;
            // Default -> Detects Invalid Selection, Informs The User, Calls HomeDisplay()
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                return;
        }
    }
}