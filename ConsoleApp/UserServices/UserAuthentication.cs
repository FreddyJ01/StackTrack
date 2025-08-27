using StackTrack.ConsoleApp.Menus;
namespace StackTrack.ConsoleApp.UserServices;

public class UserAuthentication
{
    public static string? passwordAttempt;
    public static bool validPassword;
    public static int currentUserIndex;

    public static void AuthenticationInterface(int userIndex)
    {
        // 1. Prompts user for a passwordAttempt
        System.Console.Write("Password: ");
        passwordAttempt = Console.ReadLine();

        // 2. Passes the passwordAttempt and userIndex into the AuthenticationLogic
        AuthenticationLogic(passwordAttempt, userIndex);
    }

    public static void AuthenticationLogic(string passwordAttempt, int userIndex)
    {
        // 1. Compares provided passwordAttempt to the password stored for given userIndex in the userDatabase
        validPassword = UserCreation.userDatabase[userIndex].userPassword == passwordAttempt ? true : false;

        // 2. Handles true/false password | True -> Directs to HomeScreen | False -> Informs user that authentication has failed and redirects them to the Identification Interface
        if (validPassword)
        {
            // Stores current users index for their session
            currentUserIndex = userIndex;

            // Take User To ServiceDashboard
            Console.Clear();
            ServiceDashboard.ServiceDashboardDisplay();
        }
        else
        {
            // Inform user that password attempt has failed
            Console.Clear();
            System.Console.WriteLine("> Log In Failed - Invalid Password\n");
            return;
        }
    }
}