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
                login.LoginMenu();
                break;
            case 2: // Create new account
                program.AccountCreation();
                break;
        }
    }
}