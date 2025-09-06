using StackTrack.ConsoleApp.AccountServices;
namespace StackTrack.ConsoleApp.UserServices;

class Home
{
    private static int userSelection;

    public static void Interface()
    {
        Console.Clear();
        do
        {
            System.Console.WriteLine("==StackTrack==");
            System.Console.WriteLine("1. Log In");
            System.Console.WriteLine("2. Create Account");
            System.Console.WriteLine("3. Exit");
            System.Console.WriteLine("--");
            System.Console.Write("Selection > ");
            int.TryParse(Console.ReadLine(), out userSelection);

            HomeLogic(userSelection);
        }
        while (userSelection != 3);
    }

    public static void HomeLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                Console.Clear();
                UserIdentification.Interface();
                break;
            case 2:
                Console.Clear();
                AccountCreation.Interface();
                break;
            case 3:
                return;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                return;
        }
    }
}