using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.UserServices;

namespace Program;

public class Program
{
    static void Main(string[] Args)
    {
        DatabaseHelper.InitializeDbTables();
        
        Home.Interface();
        
        Console.Clear();
        System.Console.WriteLine("> Application Closed");
    }
}