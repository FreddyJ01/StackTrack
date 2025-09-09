using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.AdminServices;

class ManageUsers
{
    public static void Interface()
    {
        System.Console.WriteLine("==Manage Users==");
        System.Console.WriteLine("1. View All Users");
        System.Console.WriteLine("2. Delete User");
        System.Console.WriteLine("3. User Access");
        System.Console.WriteLine("4. User Balance");
        System.Console.WriteLine("5. Exit");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");

        int.TryParse(Console.ReadLine() ?? "", out int selection);

        switch (selection)
        {
            case 1:
                Console.Clear();
                ViewAllUsers();
                break;
            case 2:
                Console.Clear();
                DeleteUser();
                break;
            case 3:
                Console.Clear();
                UserAccess();
                break;
            case 4:
                Console.Clear();
                UserBalance();
                break;
            case 5:
                Console.Clear();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection.\n");
                break;
        }
    }

    public static void ViewAllUsers()
    {
        List<User> users = new List<User>();
        users = UserData.QueryAllUsers();

        foreach (User user in users)
        {
            System.Console.WriteLine($"{user.userID}, {user.userName} : {user.userPassword} | Balance: {user.userBalance:C}, Access: {user.userAccess}");
        }
        System.Console.WriteLine("--");
        Console.ReadLine();
        Console.Clear();
        return;
    }

    public static void DeleteUser()
    {
        List<User> users = new List<User>();
        users = UserData.QueryAllUsers();

        foreach (User user in users)
        {
            System.Console.WriteLine($"{user.userID}, {user.userName} : {user.userPassword} | Balance: {user.userBalance}, Access: {user.userAccess}");
        }
        System.Console.WriteLine("--");
        System.Console.Write("Username > ");
        string username = Console.ReadLine();
        System.Console.Write("Password > ");
        string password = Console.ReadLine();

        if (UserData.TryDeleteUser(username, password))
        {
            Console.Clear();
            System.Console.WriteLine($"> Succesfully Deleted User!\n");
            return;
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine($"> Invalid Selection.\n");
            return;
        }
    }

    public static void UserAccess()
    {
        List<User> users = new List<User>();
        users = UserData.QueryAllUsers();

        foreach (User user in users)
        {
            System.Console.WriteLine($"{user.userID}, {user.userName} : {user.userPassword} | Balance: {user.userBalance}, Access: {user.userAccess}");
        }
        System.Console.WriteLine("--");
        System.Console.Write("User ID > ");
        string userID = Console.ReadLine();
        System.Console.Write("User Access > ");
        string userAccess = Console.ReadLine();

        if (userAccess.ToLower().Trim() != "admin" && userAccess.ToLower().Trim() != "user")
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Access.\n");
            return;
        }

        if (UserData.TryUpdateUserAccess(userID, userAccess))
        {
            Console.Clear();
            System.Console.WriteLine($"> Successfully Changed {UserData.QueryUserByFilter("Id", userID).userName}'s access to {UserData.QueryUserByFilter("Id", userID).userAccess}\n");
            return;
        }

        Console.Clear();
        System.Console.WriteLine("> User not found.\n");


    }

    public static void UserBalance()
    {
        List<User> users = new List<User>();
        users = UserData.QueryAllUsers();

        foreach (User user in users)
        {
            System.Console.WriteLine($"{user.userID}, {user.userName} : {user.userPassword} | Balance: {user.userBalance:C}, Access: {user.userAccess}");
        }
        System.Console.WriteLine("--");
        System.Console.Write("User ID > ");
        string userID = Console.ReadLine();
        System.Console.Write("Set User Balance > ");
        bool validBalance = double.TryParse(Console.ReadLine(), out double userBalance);

        if (!validBalance)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Balance\n");
            return;
        }

        if (UserData.TryUpdateUserBalance(userID, userBalance))
        {
            Console.Clear();
            System.Console.WriteLine($"> Successfully Changed {UserData.QueryUserByFilter("Id", userID).userName}'s balance to {UserData.QueryUserByFilter("Id", userID).userBalance:C}\n");
            return;
        }

        Console.Clear();
        System.Console.WriteLine("> User not found.\n");
    }
}