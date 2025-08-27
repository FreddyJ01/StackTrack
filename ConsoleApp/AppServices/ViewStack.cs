using StackTrack.ConsoleApp.UserServices;

namespace StackTrack.ConsoleApp.AppServices;

class ViewStack
{
    public static void PrintUserStack()
    {
        var userStack = UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBookStack;

        // 1. Interface Header
        System.Console.WriteLine("Current User Stack:");

        // 2. Prints all books in current users book stack
        foreach (var pair in userStack)
        {
            System.Console.WriteLine($"{pair.Key}, {pair.Value}");
        }

        // 3. Allows user to decide to make a return
        System.Console.WriteLine("--");
        System.Console.WriteLine("\nWould you like to make a return?");
        System.Console.Write("Selection > ");
        ViewStackLogic(Console.ReadLine());
    }

    public static void ViewStackLogic(string userInput)
    {
        switch (userInput.ToLower().Trim())
        {
            case "yes":
                Console.Clear();
                BookReturn.BookReturnInterface();
                break;
            case "no":
                Console.Clear();
                return;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                return;
        }
    }
}