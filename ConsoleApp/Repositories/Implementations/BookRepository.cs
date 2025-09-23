using Microsoft.EntityFrameworkCore;
using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;
using StackTrack.ConsoleApp.Repositories;

namespace StackTrack.ConsoleApp.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly StackTrackDbContext _context;

    public BookRepository(StackTrackDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(string bookId)
    {
        return await _context.Books
            .Include(b => b.CheckedOutBy)
            .FirstOrDefaultAsync(b => b.BookID == bookId);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.CheckedOutBy)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetAvailableAsync()
    {
        return await _context.Books
            .Where(b => b.CheckedOutByID == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetCheckedOutByUserAsync(string userId)
    {
        return await _context.Books
            .Include(b => b.CheckedOutBy)
            .Where(b => b.CheckedOutByID == userId)
            .ToListAsync();
    }

    public async Task<Book> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteAsync(string bookId)
    {
        var book = await GetByIdAsync(bookId);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(string bookId)
    {
        return await _context.Books.AnyAsync(b => b.BookID == bookId);
    }

    public async Task<IEnumerable<Book>> SearchAsync(string searchTerm)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        return await _context.Books
            .Include(b => b.CheckedOutBy)
            .Where(b => b.BookTitle.ToLower().Contains(lowerSearchTerm) ||
                       b.BookAuthor.ToLower().Contains(lowerSearchTerm) ||
                       b.BookGenre.ToLower().Contains(lowerSearchTerm))
            .ToListAsync();
    }
}