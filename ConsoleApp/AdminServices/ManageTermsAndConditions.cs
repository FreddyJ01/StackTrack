using StackTrack.ConsoleApp.UserServices;
namespace StackTrack.ConsoleApp.AdminServices;

class ManageTermsAndConditions
{
    public static void Interface()
    {
        System.Console.WriteLine("Terms and Conditions");
        System.Console.WriteLine($"> Books Must Be Checked In {BookReturn.timeBeforeLate} Seconds After Checking Out To Avoid Late Fees");
        System.Console.WriteLine($"> {BookReturn.lateFeeMultiplier:C} Per Second A Book Is Late");
        System.Console.WriteLine("--");
        System.Console.Write("Update Duration > ");
        bool isDurationValid = double.TryParse(Console.ReadLine(), out double Duration);
        System.Console.Write("Update Multiplier > ");
        bool isMultiplierValid = double.TryParse(Console.ReadLine(), out double Multiplier);

        if (!isDurationValid && !isMultiplierValid)
        {
            Console.Clear();
            System.Console.WriteLine("> Invalid Selection.");
            return;
        }

        UpdateTermsAndConditions(Duration, Multiplier);

        Console.Clear();
        System.Console.WriteLine("> Succesfully Updated Terms and Conditions.\n");

    }

    public static void UpdateTermsAndConditions(double Duration, double Multiplier)
    {
        BookReturn.lateFeeMultiplier = Multiplier;
        BookReturn.timeBeforeLate = Duration;
    }
}