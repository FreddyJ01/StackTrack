using System.ComponentModel.DataAnnotations;

namespace StackTrack.RefactoredApp.Models;

public class User
{
    [Key]
    public string UserID { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Range(0, double.MaxValue)]
    public double UserBalance { get; set; }
    
    [Required]
    public UserRole UserAccess { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
}

public enum UserRole
{
    User = 0,
    Admin = 1,
    Owner = 2
}