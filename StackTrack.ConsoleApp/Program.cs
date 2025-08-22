using Users;
using MainMenu;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;

namespace StackTrack.ConsoleApp;

class Program
{
    // Stores the users that are created in AccountCreation().
    List<User> users = new List<User>();
    static void Main(string[] Args)
    {
        Program program = new Program();
        MainMenu.MainMenuDisplay();
        // program.AccountCreation();
        // program.BookCheckout(0, "Diary of A Wimpy Kid");

        // System.Console.WriteLine($"Welcome {program.users[0].userName}, your unique ID is {program.users[0].userID}");
        // System.Console.WriteLine("You may now check out books!");
    }

    // Take user input (Name) and Create an Instance of User (Name, ID)
    public void AccountCreation()
    {
        System.Console.WriteLine("Name:");
        string name = Console.ReadLine();
        User newUser = new User { userName = name, userID = Guid.NewGuid().ToString() };
        users.Add(newUser);

        System.Console.WriteLine($"Welcome {users[users.Count - 1].userName}, your unique ID is {users[users.Count - 1].userID}");
        System.Console.WriteLine("You may now check out books!");
    }

    void BookCheckout(int user, string bookTitle) // Log user book checkout time.
    {
        // users[user].bookList.Add(bookTitle, DateTime.Now);
    }

    void ConsoleOutput(User users)
    {
        
    }
}