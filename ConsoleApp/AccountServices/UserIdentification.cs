using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.AdminServices;
using StackTrack.ConsoleApp.UserServices;

namespace StackTrack.ConsoleApp.AccountServices;

class UserIdentification
{
    private static string? usernameAttempt;
    private static string? passwordAttempt;
    public static string? currentUserID;
    private static bool passwordIsValid;
    private static string? adminID = "d5784a02-00fb-4c39-b623-7962e3018b0a";

    public static void Interface()
    {
        System.Console.WriteLine("==Log In==");
        System.Console.Write("Username: ");
        usernameAttempt = Console.ReadLine();
        System.Console.Write("Password: ");
        passwordAttempt = Console.ReadLine();

        if (!Identification(usernameAttempt ?? ""))
        {
            Console.Clear();
            System.Console.WriteLine("> Log In Failed - Invalid Username\n");
            return;
        }

        if (!Authentication(passwordAttempt ?? ""))
        {
            Console.Clear();
            System.Console.WriteLine("> Log In Failed - Invalid Password\n");
            return;
        }

        if (currentUserID == adminID)
        {
            Console.Clear();
            AdminServiceDashboard.Interface();
        }
        else
        {
            Console.Clear();
            ServiceDashboard.Interface();
        }
    }

    public static bool Identification(string usernameAttempt)
    {
        currentUserID = UserData.QueryUserByFilter("Name",usernameAttempt)[0].userID;

        if (currentUserID == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool Authentication(string passwordAttempt)
    {
        passwordIsValid = passwordAttempt == "" ? false : UserData.QueryUserByFilter("Password",currentUserID)[0].userPassword == passwordAttempt ? true : false;

        if (passwordIsValid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}