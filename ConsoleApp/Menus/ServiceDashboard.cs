namespace StackTrack.ConsoleApp.Menus;

class ServiceDashboard
{
    public static string? userInput;
    public static int userSelection;

    public static void ServiceDashboardDisplay()
    {
        do
        {
            // 1. Interface Header
            System.Console.WriteLine("==Home Screen==");

            // 2. Display user options
            System.Console.WriteLine("1. Check Out Books");
            System.Console.WriteLine("2. Return Books");
            System.Console.WriteLine("3. View Current Stack of Books");
            System.Console.WriteLine("4. View Current Balance");
            System.Console.WriteLine("5. View Terms and Conditions");
            System.Console.WriteLine("6. Exit");

            // 3. Take user input, trim it, and put it in lower case
            userInput = Console.ReadLine().ToLower().Trim();

            // 4. Parse user input for a number choice and store it in userSelection
            int.TryParse(userInput, out userSelection);

            // 5. Pass userSelection into ServiceDashBoardLogic to route the user
            ServiceDashboardLogic(userSelection);
        } while (userInput != "exit");
    }

    public static void ServiceDashboardLogic(int userSelection)
    {
        switch (userSelection)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }
}