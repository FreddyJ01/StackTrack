using StackTrack.ConsoleApp.UserServices;

namespace StackTrack.ConsoleApp.AppServices;

class ViewStack
{
    public static void PrintUserStack()
    {
        var userStack = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack;

        // 1. Interface Header
        Console.Clear();
        System.Console.WriteLine("Current User Stack:");

        // 2. Prints all books in current users book stack
        foreach (var pair in userStack)
        {
            System.Console.WriteLine($"{pair.Key}, {pair.Value}");
        }

        // 3. Waits for the user to press a key to exit the interface
        Console.ReadLine();
    }
}