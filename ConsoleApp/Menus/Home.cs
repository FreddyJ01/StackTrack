using StackTrack.ConsoleApp.Services;
namespace StackTrack.ConsoleApp.Menus;

class Home
{
    public static string? userInput;
    public static int userSelection;

    public static void HomeDisplay()
    {
        do
        {
            // 1. Interface Header
            Console.Clear();
            System.Console.WriteLine("==Home==");

            // 2. Display user options
            System.Console.WriteLine("1. Login");
            System.Console.WriteLine("2. Create an Account");

            // 3. Takes user input, trims it, and puts it to lower case
            userInput = Console.ReadLine().ToLower().Trim();

            // 4. Parses user input for a number choice | Potentially need to handle if user doesn't input a valid menu choice.
            int.TryParse(userInput, out userSelection);

            // 5. Passes user input number choice to HomeLogic to route the user
            HomeLogic(userSelection);
        } while (userInput != "exit");
    }

    public static void HomeLogic(int userSelection)
    {
        switch (userSelection)
        {
            // -> Takes user to the Identification Page
            case 1:
                Console.Clear();
                UserIdentification.IdentificationInterface();
                break;
            // -> Takes user to the UserCreation Page
            case 2:
                Console.Clear();
                UserCreation.AccountCreationInterface();
                break;
        }
    }
}