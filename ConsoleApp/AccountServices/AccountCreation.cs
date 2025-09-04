using StackTrack.ConsoleApp.Data;
namespace StackTrack.ConsoleApp.AccountServices;

public class AccountCreation
{
    public static void Interface()
    {
        System.Console.WriteLine("==Account Creation==");
        System.Console.Write("Create a Username: ");
        string? username = Console.ReadLine();
        System.Console.Write("Create a Password: ");
        string? password = Console.ReadLine();
        double defaultBalance = 0.00;

        if (username == "" ? true : UserData.VerifyUniqueUsername(username))
        {
            Console.Clear();
            System.Console.WriteLine("> Creation Failed - Username Already Taken\n");
            return;
        }

        if (password == "" ? true : false)
        {
            Console.Clear();
            System.Console.WriteLine("> Creation Failed - Invalid Password\n");
            return;
        }

        try
        {
            UserData.AddUserToDb(Guid.NewGuid().ToString(), username, password, defaultBalance);
        }
        catch
        {
            Console.Clear();
            System.Console.WriteLine("> Failed To Add User To SQLITE DB");
            return;
        }

        Console.Clear();
        System.Console.WriteLine($"Welcome {username}! Your Unique ID is {UserData.QueryIdByUsername(username)}\n");
        return;
    }
}