using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackTrack.RefactoredApp.Configuration;
using StackTrack.RefactoredApp.DTOs;
using StackTrack.RefactoredApp.Models;
using StackTrack.RefactoredApp.Repositories;
using StackTrack.RefactoredApp.Services;
using FluentValidation;

namespace StackTrack.RefactoredApp.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<BookService> _logger;
    private readonly AppSettings _appSettings;
    private readonly IValidator<CreateBookDto> _createBookValidator;
    private readonly IValidator<CheckoutBookDto> _checkoutValidator;

    public BookService(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        ILogger<BookService> logger,
        IOptions<AppSettings> appSettings,
        IValidator<CreateBookDto> createBookValidator,
        IValidator<CheckoutBookDto> checkoutValidator)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _logger = logger;
        _appSettings = appSettings.Value;
        _createBookValidator = createBookValidator;
        _checkoutValidator = checkoutValidator;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        try
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all books");
            return Enumerable.Empty<BookDto>();
        }
    }

    public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
    {
        try
        {
            var books = await _bookRepository.GetAvailableAsync();
            return books.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving available books");
            return Enumerable.Empty<BookDto>();
        }
    }

    public async Task<IEnumerable<BookDto>> GetUserBooksAsync(string userId)
    {
        try
        {
            var books = await _bookRepository.GetCheckedOutByUserAsync(userId);
            return books.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving books for user: {UserId}", userId);
            return Enumerable.Empty<BookDto>();
        }
    }

    public async Task<BookDto?> GetBookByIdAsync(string bookId)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            return book != null ? MapToDto(book) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving book: {BookId}", bookId);
            return null;
        }
    }

    public async Task<(bool Success, string Message)> CreateBookAsync(CreateBookDto createBookDto)
    {
        try
        {
            var validationResult = await _createBookValidator.ValidateAsync(createBookDto);
            if (!validationResult.IsValid)
            {
                return (false, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var bookId = Guid.NewGuid().ToString();
            var book = new Book
            {
                BookID = bookId,
                BookTitle = createBookDto.BookTitle,
                BookAuthor = createBookDto.BookAuthor,
                BookGenre = createBookDto.BookGenre,
                CreatedAt = DateTime.UtcNow
            };

            await _bookRepository.CreateAsync(book);
            _logger.LogInformation("Book created: {BookTitle} ({BookId})", book.BookTitle, book.BookID);
            return (true, "Book created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book: {BookTitle}", createBookDto.BookTitle);
            return (false, "An error occurred while creating the book");
        }
    }

    public async Task<(bool Success, string Message)> CheckoutBookAsync(CheckoutBookDto checkoutDto)
    {
        try
        {
            var validationResult = await _checkoutValidator.ValidateAsync(checkoutDto);
            if (!validationResult.IsValid)
            {
                return (false, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var book = await _bookRepository.GetByIdAsync(checkoutDto.BookID);
            if (book == null)
            {
                return (false, "Book not found");
            }

            if (book.IsCheckedOut)
            {
                return (false, "Book is already checked out");
            }

            var user = await _userRepository.GetByIdAsync(checkoutDto.UserID);
            if (user == null)
            {
                return (false, "User not found");
            }

            book.CheckedOutByID = checkoutDto.UserID;
            book.CheckedOutAt = DateTime.UtcNow;

            await _bookRepository.UpdateAsync(book);
            _logger.LogInformation("Book checked out: {BookTitle} by {UserName}", book.BookTitle, user.UserName);
            return (true, $"Book '{book.BookTitle}' checked out successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking out book: {BookId} for user: {UserId}", checkoutDto.BookID, checkoutDto.UserID);
            return (false, "An error occurred while checking out the book");
        }
    }

    public async Task<(bool Success, double Fee, string Message)> ReturnBookAsync(string bookId, string userId)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                return (false, 0, "Book not found");
            }

            if (!book.IsCheckedOut || book.CheckedOutByID != userId)
            {
                return (false, 0, "Book is not checked out by this user");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return (false, 0, "User not found");
            }

            // Calculate late fee
            var checkoutDate = book.CheckedOutAt!.Value;
            var daysSinceCheckout = (DateTime.UtcNow - checkoutDate).Days;
            var lateFee = 0.0;

            if (daysSinceCheckout > _appSettings.MaxCheckoutDays)
            {
                var lateDays = daysSinceCheckout - _appSettings.MaxCheckoutDays;
                lateFee = lateDays * _appSettings.LateFeeMultiplier;

                // Deduct from user balance
                user.UserBalance -= lateFee;
                await _userRepository.UpdateAsync(user);
            }

            // Return the book
            book.CheckedOutByID = null;
            book.CheckedOutAt = null;
            await _bookRepository.UpdateAsync(book);

            _logger.LogInformation("Book returned: {BookTitle} by {UserName}, Late Fee: {LateFee:C}", 
                book.BookTitle, user.UserName, lateFee);

            var message = lateFee > 0 
                ? $"Book '{book.BookTitle}' returned. Late fee of {lateFee:C} charged."
                : $"Book '{book.BookTitle}' returned successfully";

            return (true, lateFee, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error returning book: {BookId} for user: {UserId}", bookId, userId);
            return (false, 0, "An error occurred while returning the book");
        }
    }

    public async Task<(bool Success, string Message)> DeleteBookAsync(string bookId)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                return (false, "Book not found");
            }

            if (book.IsCheckedOut)
            {
                return (false, "Cannot delete a book that is currently checked out");
            }

            var success = await _bookRepository.DeleteAsync(bookId);
            if (success)
            {
                _logger.LogInformation("Book deleted: {BookTitle} ({BookId})", book.BookTitle, bookId);
                return (true, "Book deleted successfully");
            }

            return (false, "Failed to delete book");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book: {BookId}", bookId);
            return (false, "An error occurred while deleting the book");
        }
    }

    public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
    {
        try
        {
            var books = await _bookRepository.SearchAsync(searchTerm);
            return books.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching books with term: {SearchTerm}", searchTerm);
            return Enumerable.Empty<BookDto>();
        }
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            BookID = book.BookID,
            BookTitle = book.BookTitle,
            BookAuthor = book.BookAuthor,
            BookGenre = book.BookGenre,
            CheckedOutByID = book.CheckedOutByID,
            CheckedOutAt = book.CheckedOutAt,
            IsCheckedOut = book.IsCheckedOut,
            CheckedOutByName = book.CheckedOutBy?.UserName ?? string.Empty
        };
    }
}