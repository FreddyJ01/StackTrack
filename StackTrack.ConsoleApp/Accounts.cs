using ConsoleUI;
using Program;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Collections;

namespace Accounts;

public class User
{
    public static List<User> userDatabase = new List<User>(); // This is the database of all users
    public string userName { get; set; } // The name the user input upon account creation.
    public string userPassword { get; set; }
    public string userID { get; set; } // A One-Time randomly generated userID that stays with the user forever.
    public Dictionary<string, DateTime> userBookStack { get; set; } = new Dictionary<string, DateTime>(); // Dynamic User Book Repo (Checkout, Check In, Late Fees, Extensions)
    public double userBalance { get; set; } // Balance that the user owes
}

public class UserCreation
{
    public void AccountCreation()
    {
        Console.Clear();
        System.Console.WriteLine("======Account Creation======");
        System.Console.WriteLine("Name:");
        string name = Console.ReadLine();
        System.Console.WriteLine("Create a Password:");
        string password = Console.ReadLine();
        User newUser = new User { userName = name, userID = Guid.NewGuid().ToString(), userPassword = password };
        User.userDatabase.Add(newUser);

        System.Console.WriteLine($"Welcome {User.userDatabase[User.userDatabase.Count - 1].userName}, your unique ID is {User.userDatabase[User.userDatabase.Count - 1].userID}");
        System.Console.WriteLine("You may now check out books!");
    }
}

public class UserAuthentication
{
    HomeScreen homeScreen = new HomeScreen();
    public void UserIdentification()
    {
        string? userInput;
        int userIndex = -1;
        System.Console.WriteLine("======Login======");
        System.Console.Write("Username: ");
        userInput = Console.ReadLine();
        userIndex = User.userDatabase.FindIndex(u => u.userName == userInput); // Finds the users index in userDatabase, if no user is found returns -1.

        if (userIndex == -1) // Tests if user exists
        {
            Console.Clear();
            System.Console.WriteLine("===NO SUCH USER===");
            UserIdentification();
        }
        else
        {
            AuthenticationHandler(userIndex);
        }
    }

    void AuthenticationHandler(int userIndex)
    {
        System.Console.Write("Password: "); // Prompts for password
        String userInputPassword = Console.ReadLine(); // Takes password attempt
        bool validPassword = User.userDatabase[userIndex].userPassword == userInputPassword ? true : false; // Validates password
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