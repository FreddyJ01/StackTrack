namespace StackTrack.ConsoleApp.Configuration;

public class AppSettings
{
    public double LateFeeMultiplier { get; set; }
    public int MaxCheckoutDays { get; set; }
    public string OwnerUserId { get; set; } = string.Empty;
    public int SessionTimeoutMinutes { get; set; }
}