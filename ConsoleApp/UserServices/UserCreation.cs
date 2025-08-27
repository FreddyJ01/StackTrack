using StackTrack.ConsoleApp.Models;
namespace StackTrack.ConsoleApp.UserServices;

public class UserCreation
{
    // Database of all users
    public static List<Users> userDatabase = new List<Users>();

    // Placeholder strings for user input
    public static string? username;
    public static string? password;

    public static bool usernameIsUnique;
    public static bool passwordIsStrong;

    public static void AccountCreationInterface()
    {
        // 1. Interface Header
        System.Console.WriteLine("==Account Creation==");

        // 2. Takes Username
        System.Console.Write("Create a Username: ");
        username = Console.ReadLine();

        // 4. Verifies the username is unique to the database AND not an empty string
        if (!VerifyUniqueUsername(username))
        {
            Console.Clear();
            System.Console.WriteLine("> Creation Failed - Username Already Taken\n");
            return;
        }
        else
        {

            // 5. Takes Password
            System.Console.Write("Create a Password: ");
            password = Console.ReadLine();
            // password = "";

            // Ensures password meets criteria.
            if (!PasswordValidation(password))
            {
                Console.Clear();
                System.Console.WriteLine("> Creation Failed - Invalid Password\n");
                return;
            }

            // 6. Passes user input to AccountCreationLogic()
            AccountCreationLogic(username, password);

            // 7. Welcomes the newly created user to the application
            Console.Clear();

            System.Console.WriteLine($"> Welcome {userDatabase[userDatabase.Count - 1].userName}, your unique ID is {userDatabase[userDatabase.Count - 1].userID}\n");
        }
    }

    public static void AccountCreationLogic(string username, string password)
    {
        // 1. Creates a new user
        Users newUser = new Users { userName = username, userID = Guid.NewGuid().ToString(), userPassword = password };

        // 2. Adds the user to the user database
        userDatabase.Add(newUser);
    }

    // Validates username is not taken or an empty string
    public static bool VerifyUniqueUsername(string username)
    {
        usernameIsUnique = username == "" ? false : userDatabase.Any(u => u.userName == username) ? false : true;

        return usernameIsUnique;
    }

    // Validates password meets criteria (Not an Empty String)
    public static bool PasswordValidation(string password)
    {
        passwordIsStrong = password == "" ? false : true;
        return passwordIsStrong;
    }
}