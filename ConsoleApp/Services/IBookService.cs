using StackTrack.ConsoleApp.DTOs;

namespace StackTrack.ConsoleApp.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<IEnumerable<BookDto>> GetAvailableBooksAsync();
    Task<IEnumerable<BookDto>> GetUserBooksAsync(string userId);
    Task<BookDto?> GetBookByIdAsync(string bookId);
    Task<(bool Success, string Message)> CreateBookAsync(CreateBookDto createBookDto);
    Task<(bool Success, string Message)> CheckoutBookAsync(CheckoutBookDto checkoutDto);
    Task<(bool Success, double Fee, string Message)> ReturnBookAsync(string bookId, string userId);
    Task<(bool Success, string Message)> DeleteBookAsync(string bookId);
    Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
}