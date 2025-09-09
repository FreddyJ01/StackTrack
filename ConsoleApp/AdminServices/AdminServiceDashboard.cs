namespace StackTrack.ConsoleApp.AdminServices;

class AdminServiceDashboard
{
    public static void Interface()
    {
        int userSelection;
        do
        {
            System.Console.WriteLine("==Admin Service Dashboard==");
            System.Console.WriteLine("1. Manage Library");
            System.Console.WriteLine("2. Manage Users");
            System.Console.WriteLine("3. Manage Terms & Conditions");
            System.Console.WriteLine("4. Log Out");
            System.Console.WriteLine("--");
            System.Console.Write("Selection > ");

            AdminServiceDashboardLogic(int.TryParse(Console.ReadLine(), out userSelection) ? userSelection : 0);
        }
        while (userSelection != 4);
    }

    public static void AdminServiceDashboardLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                Console.Clear();
                ManageLibrary.Interface();
                break;
            case 2:
                Console.Clear();
                ManageUsers.Interface();
                break;
            case 3:
                Console.Clear();
                ManageTermsAndConditions.Interface();
                break;
            case 4:
                Console.Clear();
                System.Console.WriteLine("> Logged Out\n");
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection\n");
                break;
        }
    }
}