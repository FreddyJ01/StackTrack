namespace MainMenu;

using StackTrack.ConsoleApp;

public class MainMenu
{
    public void MainMenuDisplay()
    {
        string? userInput;
        int userChoice;
        do
        {
            System.Console.WriteLine("1. Create New Account");
            userInput = Console.ReadLine().ToLower().Trim();
            int.TryParse(userInput, out userChoice);
            MainMenuDisplayLogic(userChoice);

        } while (userInput != "exit");
    }

    public void MainMenuDisplayLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1: // Create new account
                StackTrack.ConsoleApp.Program.AccountCreation();
                break;
        }
    }
}