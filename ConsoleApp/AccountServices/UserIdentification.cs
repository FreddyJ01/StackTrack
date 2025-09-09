using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.AdminServices;
using StackTrack.ConsoleApp.UserServices;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.AccountServices;

class UserIdentification
{
    private static string? usernameAttempt;
    private static string? passwordAttempt;
    public static string? currentUserID;
    private static bool passwordIsValid;
    private static string? ownerID = "ceecb710-fe80-4ac5-b925-c69220785188";

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

        if (currentUserID == ownerID || UserData.QueryUserByFilter("Id", currentUserID).userAccess == "Admin")
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
        User queryUser = UserData.QueryUserByFilter("Name", usernameAttempt);

        if (queryUser == null)
        {
            return false;
        }

        currentUserID = UserData.QueryUserByFilter("Name",usernameAttempt).userID ?? string.Empty;

        if (string.IsNullOrEmpty(currentUserID))
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
        passwordIsValid = passwordAttempt == "" ? false : UserData.QueryUserByFilter("Id",currentUserID).userPassword == passwordAttempt ? true : false;

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