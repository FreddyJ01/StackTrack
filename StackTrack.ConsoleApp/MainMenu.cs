using Login;
using StackTrack.ConsoleApp;
namespace MainMenu;
// TODO:
// 1. Make it look prettier

class MainMenu
{
    Program program = new Program();
    Logins login = new Logins();
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
            case 1: // Login
                Console.Clear();
                login.UserIdentification();
                break;
            case 2: // Create new account
                program.AccountCreation();
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
}