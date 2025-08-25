namespace StackTrack.ConsoleApp.Services;

class UserIdentification
{
    // Identification variables
    public static string? userInput;
    public static int userIndex;

    public static void IdentificationInterface()
    {
        // 1. Interface Header
        System.Console.WriteLine("==Login==");

        // 2. Takes Username
        System.Console.Write("Username: ");
        userInput = Console.ReadLine();

        // 3. Passes userInput to Identification Logic
        IdentificationLogic(userInput);
    }

    public static void IdentificationLogic(string username)
    {
        // Resets userIndex to -1
        userIndex = -1;

        // 1. Finds user index in userDatabase using username
        userIndex = UserCreation.userDatabase.FindIndex(u => u.userName == userInput);

        // 2. Determines if user exists | User Exists -> Redirect to Authentication | User Doesn't Exist -> Redirect Back To Identification Interface
        if (userIndex == -1)
        {
            // Informs user no user exists with that username
            Console.Clear();
            System.Console.WriteLine("==NO SUCH USER==");

            // Redirects back to Identification Interface
            IdentificationInterface();
        }
        else
        {
            // Redirects to the Authentication Handler Interface
            UserAuthentication.AuthenticationInterface(userIndex);
        }
    }
}