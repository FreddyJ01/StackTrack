using Users;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;

namespace StackTrack.ConsoleApp;

class Program
{
    List<User> users = new List<User>();
    static void Main(string[] Args)
    {
        Program program = new Program();
        program.AccountCreation();
        program.BookCheckout(0, "Diary of A Wimpy Kid");

        System.Console.WriteLine($"Welcome {program.users[0].Name}, your unique ID is {program.users[0].userID}");
        System.Console.WriteLine("You may now check out books!");
    }

    void AccountCreation() // Take user input (Name) and Create an Instance of User (Name, ID)
    {
        System.Console.WriteLine("Name:");
        string name = Console.ReadLine();
        User newUser = new User { Name = name, userID = Guid.NewGuid().ToString() };
        users.Add(newUser);
    }

    void BookCheckout(int user, string bookTitle) // Log user book checkout time.
    {
        users[user].bookList.Add(bookTitle, DateTime.Now);
    }

    void ConsoleOutput(User users)
    {
        
    }
}