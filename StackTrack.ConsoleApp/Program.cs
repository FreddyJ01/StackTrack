using Users;
using MainMenu;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;

namespace StackTrack.ConsoleApp;

class Program
{
    // Stores the users that are created in AccountCreation().
    List<User> userDatabase = new List<User>();
    static MainMenu.MainMenu mainMenu = new MainMenu.MainMenu();

    static void Main(string[] Args)
    {
        mainMenu.MainMenuDisplay();
        
        
    }

    // Take user input (Name) and Create an Instance of User (Name, ID)
    public void AccountCreation()
    {
        System.Console.WriteLine("Name:");
        string name = Console.ReadLine();
        User newUser = new User { userName = name, userID = Guid.NewGuid().ToString() };
        userDatabase.Add(newUser);

        System.Console.WriteLine($"Welcome {userDatabase[userDatabase.Count - 1].userName}, your unique ID is {userDatabase[userDatabase.Count - 1].userID}");
        System.Console.WriteLine("You may now check out books!");
    }

    void BookCheckout(int user, string bookTitle) // Log user book checkout time.
    {
        // users[user].bookList.Add(bookTitle, DateTime.Now);
    }
}