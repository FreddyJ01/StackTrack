using System.ComponentModel.DataAnnotations;

namespace StackTrack.RefactoredApp.Models;

public class Book
{
    [Key]
    public string BookID { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string BookTitle { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string BookAuthor { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string BookGenre { get; set; } = string.Empty;
    
    public string? CheckedOutByID { get; set; }
    public DateTime? CheckedOutAt { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public User? CheckedOutBy { get; set; }
    
    public bool IsCheckedOut => !string.IsNullOrEmpty(CheckedOutByID);
}