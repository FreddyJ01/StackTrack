namespace StackTrack.ConsoleApp.UserServices;

class UserIdentification
{
    // Identification variables
    public static string? usernameAttempt;
    public static int userIndex;

    public static void IdentificationInterface()
    {
        // 1. Interface Header
        System.Console.WriteLine("==Log In==");

        // 2. Takes Username
        System.Console.Write("Username: ");
        usernameAttempt = Console.ReadLine();

        // 4. Passes userInput to Identification Logic if User Doesn't Wish To Go Back
        IdentificationLogic(usernameAttempt);
    }

    public static void IdentificationLogic(string usernameAttempt)
    {
        // 1. Finds userIndex in userDatabase using usernameAttempt
        userIndex = UserCreation.userDatabase.FindIndex(u => u.userName == usernameAttempt);

        // 2. Determines if user exists
        // User Doesn't Exist -> Redirect Back To Identification Interface
        if (userIndex == -1)
        {
            // Informs user, that no user exists with that username
            Console.Clear();
            System.Console.WriteLine("> Log In Failed - Invalid Username\n");
            return;
        }
        // User Exists -> Redirect to Authentication
        else
        {
            UserAuthentication.AuthenticationInterface(userIndex);
        }
    }
}