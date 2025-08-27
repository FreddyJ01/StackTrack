using StackTrack.ConsoleApp.UserServices;

namespace StackTrack.ConsoleApp.AppServices;

class BalanceView
{
    public static string? userSelection;
    public static double userPayment;
    public static bool validPayment;

    public static void BalanceViewInterface()
    {
        // 1. Display the users current balance to them
        System.Console.WriteLine($"Current User Balance: \n${(UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance):f2}");

        // 2. Asks the user if they would like to make a payment
        System.Console.WriteLine("\nWould you like to make a payment?");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");

        // 3. Takes user Input and passes it to interface logic
        BalanceViewLogic(Console.ReadLine());

    }

    public static void BalanceViewLogic(string userSelection)
    {
        switch (userSelection)
        {
            case "yes":
                Console.Clear();
                BalancePay();
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

    public static void BalancePay()
    {
        // Ask the user how much they would like to pay
        System.Console.WriteLine($"Current Total Balance: \n${UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance}");

        System.Console.WriteLine("\nHow much would you like to pay:");
        System.Console.WriteLine("--");
        System.Console.Write("Amount > ");

        // Take the users input
        validPayment = double.TryParse(Console.ReadLine(), out userPayment);

        if (!validPayment)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Payment\n");
            return;
        }

        if (userPayment > UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance)
        {
            Console.Clear();
            System.Console.WriteLine("> Payment Failed - Payment Exceeds Current Balance\n");
            return;
        }

        // Subtract their input from their balance
        UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance -= userPayment;

        Console.Clear();

        // Display the users new balance and thank them for their payment.
        System.Console.WriteLine($"> Payment Of ${userPayment} Was Successful");

        System.Console.WriteLine($"> New Current Balance: ${UserCreation.userDatabase[UserAuthentication.currentUserIndex].userBalance}\n");

        // return user to servicedashboard
        return;
    }
}