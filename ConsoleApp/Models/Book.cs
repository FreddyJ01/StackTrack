namespace StackTrack.ConsoleApp.Models;

public class Book
{
    public string? BookID { get; set; }
    public string? BookTitle { get; set; }
    public string? BookAuthor { get; set; }
    public string? BookGenre { get; set; }
    public string? CheckedOutByID { get; set; }
    public DateTime? CheckedOutAt { get; set; }
}