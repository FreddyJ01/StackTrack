namespace StackTrack.ConsoleApp.Models;

public class Users
{
    public string userName { get; set; }
    public string userPassword { get; set; }
    public string userID { get; set; }
    public Dictionary<string, DateTime> userBookStack { get; set; } = new Dictionary<string, DateTime>();
    public double userBalance { get; set; }
}