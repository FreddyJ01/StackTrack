using StackTrack.ConsoleApp.Models;
namespace StackTrack.ConsoleApp.UserServices;

public class UserCreation
{
    // Database of all users
    public static List<Users> userDatabase = new List<Users>();

    // Placeholder strings for user input
    public static string? username;
    public static string? password;

    public static void AccountCreationInterface()
    {
        // 1. Interface Header
        System.Console.WriteLine("==Account Creation==");

        // 2. Takes Username
        System.Console.WriteLine("Create a Username:");
        username = Console.ReadLine();

        // 3. Takes Password
        System.Console.WriteLine("Create a Password:");
        password = Console.ReadLine();

        // 4. Passes user input to AccountCreationLogic()
        AccountCreationLogic(username, password);

        // 5. Welcomes the newly created user to the application
        System.Console.WriteLine($"Welcome {userDatabase[userDatabase.Count - 1].userName}, your unique ID is {userDatabase[userDatabase.Count - 1].userID}");
    }

    public static void AccountCreationLogic(string username, string password)
    {
        // 1. Creates a new user
        Users newUser = new Users { userName = username, userID = Guid.NewGuid().ToString(), userPassword = password };

        // 2. Adds the user to the user database
        userDatabase.Add(newUser);
    }
}