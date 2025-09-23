namespace StackTrack.ConsoleApp.DTOs;

public class CreateUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserAccess { get; set; } = "User";
}

public class LoginDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserDto
{
    public string UserID { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public double UserBalance { get; set; }
    public string UserAccess { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class UpdateUserBalanceDto
{
    public string UserID { get; set; } = string.Empty;
    public double Amount { get; set; }
}