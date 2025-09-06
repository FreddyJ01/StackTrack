namespace StackTrack.ConsoleApp.UserServices;

class TermsAndConditions
{
    public static void Interface()
    {
        // Display Terms and Conditions
        System.Console.WriteLine("Terms and Conditions");
        System.Console.WriteLine("> Books Must Be Check In 10 Seconds After Checking Out To Avoid Late Fees");
        System.Console.WriteLine("> $10 Per Second A Book Is Late");
        System.Console.WriteLine("--");

        // Allow users to acknowledge and exit
        System.Console.Write("Enter To Exit > ");
        Console.ReadLine();
        Console.Clear();
    }
}