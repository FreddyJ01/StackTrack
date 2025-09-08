using StackTrack.ConsoleApp.AccountServices;
using StackTrack.ConsoleApp.Data;

namespace StackTrack.ConsoleApp.UserServices;

class MyBalance
{
    public static void Interface()
    {
        Console.Clear();
        DisplayUserBalance(GetUserBalance());

        switch (Console.ReadLine().ToLower().Trim())
        {
            case "yes":
                Payment();
                break;
            case "no":
                Console.Clear();
                break;
            default:
                Console.Clear();
                System.Console.WriteLine("> Invalid Selection.\n");
                break;
        }
    }

    public static double GetUserBalance()
    {
        double userBalance = UserData.QueryUserByFilter("Id", UserIdentification.currentUserID ?? "").userBalance;

        return userBalance;
    }

    public static void DisplayUserBalance(double userBalance)
    {
        System.Console.WriteLine("Current Balance:");
        System.Console.WriteLine($"{userBalance:C}\n");
        System.Console.WriteLine("Would you like to make a payment?");
        System.Console.WriteLine("--");
        System.Console.Write("Selection > ");
    }

    public static void Payment()
    {
        System.Console.Write("Payment Amount > ");
        bool validPayment = double.TryParse(Console.ReadLine(), out double userPayment);

        if (!validPayment)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Payment\n");
            return;
        }

        if (userPayment > GetUserBalance())
        {
            Console.Clear();
            System.Console.WriteLine("> Payment Failed - Payment Exceeds Current Balance\n");
            return;
        }

        UserData.UpdateUserBalance(-userPayment);
        Console.Clear();
        System.Console.WriteLine($"> Payment of {userPayment:C} Successful.");
        System.Console.WriteLine($"> New Balance: {GetUserBalance():C}\n");
        return;
    }
}