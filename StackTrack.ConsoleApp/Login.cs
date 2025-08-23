using MainMenu;
using StackTrack.ConsoleApp;
using Users;

namespace Login;

class Logins
{
    HomeScreen homeScreen = new HomeScreen();
    public void UserIdentification()
    {
        string? userInput;
        int userIndex = -1;
        System.Console.WriteLine("======Login======");
        System.Console.Write("Username: ");
        userInput = Console.ReadLine();
        userIndex = Program.userDatabase.FindIndex(u => u.userName == userInput); // Finds the users index in userDatabase, if no user is found returns -1.

        if (userIndex == -1) // Tests if user exists
        {
            Console.Clear();
            System.Console.WriteLine("===NO SUCH USER===");
            UserIdentification();
        }
        else
        {
            UserAuthentication(userIndex);
        }
    }

    void UserAuthentication(int userIndex)
    {
        System.Console.Write("Password: "); // Prompts for password
        String userInputPassword = Console.ReadLine(); // Takes password attempt
        bool validPassword = Program.userDatabase[userIndex].userPassword == userInputPassword ? true : false; // Validates password
        if (validPassword)
        {
            Console.Clear();
            homeScreen.HomeScreenDisplay();
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine("Authorization Failed - Try Again!");
            UserIdentification();
        }
    }
}