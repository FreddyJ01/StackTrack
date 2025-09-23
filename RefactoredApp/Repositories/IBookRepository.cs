using StackTrack.RefactoredApp.Models;

namespace StackTrack.RefactoredApp.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(string bookId);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> GetAvailableAsync();
    Task<IEnumerable<Book>> GetCheckedOutByUserAsync(string userId);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<bool> DeleteAsync(string bookId);
    Task<bool> ExistsAsync(string bookId);
    Task<IEnumerable<Book>> SearchAsync(string searchTerm);
}