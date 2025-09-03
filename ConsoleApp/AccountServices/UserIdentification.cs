using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.AdminServices;
using StackTrack.ConsoleApp.Menus;

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
            AdminServiceDashboard.AdminServiceDashboardDisplay();
        }
        else
        {
            Console.Clear();
            ServiceDashboard.Interface();
        }
    }

    public static bool Identification(string usernameAttempt)
    {
        currentUserID = UserData.QueryIdByUsername(usernameAttempt);

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
        passwordIsValid = passwordAttempt == "" ? false : UserData.QueryPasswordById(currentUserID) == passwordAttempt ? true : false;

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