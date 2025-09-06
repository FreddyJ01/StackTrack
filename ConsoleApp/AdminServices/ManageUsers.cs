namespace StackTrack.ConsoleApp.AdminServices;

class ManageUsers
{
    public static void Interface()
    {
        System.Console.WriteLine("==Manage Users==");
        System.Console.WriteLine("1. View All Users");
        System.Console.WriteLine("2. Delete User");
        System.Console.WriteLine("3. User Access");
        System.Console.WriteLine("4. User Balance");
        System.Console.WriteLine("5. Exit");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");

        int.TryParse(Console.ReadLine() ?? "", out int selection);

        switch (selection)
        {
            case 1:
                Console.Clear();
                // ViewAllUsers();
                break;
            case 2:
                Console.Clear();
                // DeleteUser();
                break;
            case 3:
                Console.Clear();
                UserAccess();
                break;
            case 4:
                Console.Clear();
                UserBalance();
                break;
            case 5:
                Console.Clear();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection.\n");
                break;
        }
    }

    public static void UserAccess()
    {
        // string user
    }

    public static void UserBalance()
    {

    }
}