namespace Login;

class Logins
{
    public void LoginMenu()
    {
        string? userInput;
        int userChoice;
        do
        {
            System.Console.Write("Username: ");
            Console.ReadLine();
            userInput = Console.ReadLine();
        } while (userInput != "exit"); // This needs to be a different condition
    }

    // void UserAuthentication()
    // {
    //     foreach (string user in userDatabase)
    //     {
            
    //     }
    // }
}