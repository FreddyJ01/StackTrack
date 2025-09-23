namespace StackTrack.RefactoredApp.DTOs;

public class BookDto
{
    public string BookID { get; set; } = string.Empty;
    public string BookTitle { get; set; } = string.Empty;
    public string BookAuthor { get; set; } = string.Empty;
    public string BookGenre { get; set; } = string.Empty;
    public string? CheckedOutByID { get; set; }
    public DateTime? CheckedOutAt { get; set; }
    public bool IsCheckedOut { get; set; }
    public string CheckedOutByName { get; set; } = string.Empty;
}

public class CreateBookDto
{
    public string BookTitle { get; set; } = string.Empty;
    public string BookAuthor { get; set; } = string.Empty;
    public string BookGenre { get; set; } = string.Empty;
}

public class CheckoutBookDto
{
    public string BookID { get; set; } = string.Empty;
    public string UserID { get; set; } = string.Empty;
}