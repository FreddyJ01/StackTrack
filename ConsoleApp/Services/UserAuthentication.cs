using StackTrack.ConsoleApp.Menus;

namespace StackTrack.ConsoleApp.Services;

public class UserAuthentication
{
    // Authentication Interface Variable
    public static string? passwordAttempt;

    // Authentication Logic Variable
    public static bool validPassword;

    public static void AuthenticationInterface(int userIndex)
    {
        // 1. Prompt user for password
        System.Console.Write("Password: ");
        passwordAttempt = Console.ReadLine();

        // 2. Passes the password into the Authentication Logic
        AuthenticationLogic(passwordAttempt, userIndex);
    }

    public static void AuthenticationLogic(string passwordAttempt, int userIndex)
    {
        // 1. Compares provided password to the password stored for given user in the userDatabase
        validPassword = UserCreation.userDatabase[userIndex].userPassword == passwordAttempt ? true : false; // Validates password

        // 2. Handles true/false password | True -> Directs to HomeScreen | False -> Informs user that authentication has failed and redirects them to the Identification Interface
        if (validPassword)
        {
            Console.Clear();
            ServiceDashboard.ServiceDashboardDisplay();
            // Take user to service dashboard
        }
        else
        {
            // Inform user that password attempt has failed
            Console.Clear();
            System.Console.WriteLine("Authentication Failed");
            UserIdentification.IdentificationInterface();
        }
    }
}