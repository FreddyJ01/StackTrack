using StackTrack.RefactoredApp.Services;
using StackTrack.RefactoredApp.UI;

namespace StackTrack.RefactoredApp.Application;

public interface IApplicationService
{
    Task RunAsync();
}

public class ApplicationService : IApplicationService
{
    private readonly IConsoleService _console;
    private readonly IAuthenticationService _authService;
    private readonly IUserService _userService;
    private readonly IBookService _bookService;

    public ApplicationService(
        IConsoleService console,
        IAuthenticationService authService,
        IUserService userService,
        IBookService bookService)
    {
        _console = console;
        _authService = authService;
        _userService = userService;
        _bookService = bookService;
    }

    public async Task RunAsync()
    {
        _console.Clear();
        _console.WriteTitle("Welcome to StackTrack Library Management System");

        while (true)
        {
            if (!_authService.IsLoggedIn())
            {
                await ShowAuthenticationMenuAsync();
            }
            else
            {
                await ShowMainMenuAsync();
            }
        }
    }

    private async Task ShowAuthenticationMenuAsync()
    {
        _console.WriteTitle("Authentication");
        _console.WriteMenu(new[] { "Login", "Register", "Exit" });

        var choice = _console.GetMenuChoice(3);
        switch (choice)
        {
            case 1:
                await HandleLoginAsync();
                break;
            case 2:
                await HandleRegisterAsync();
                break;
            case 3:
                Environment.Exit(0);
                break;
        }
    }

    private async Task ShowMainMenuAsync()
    {
        var currentUser = _authService.GetCurrentUser()!;
        _console.WriteTitle($"Welcome, {currentUser.UserName}!");
        _console.WriteLine($"Balance: {currentUser.UserBalance:C}");

        var menuItems = new List<string>
        {
            "Browse Books",
            "My Books",
            "My Account"
        };

        if (_authService.HasRole(Models.UserRole.Admin))
        {
            menuItems.Add("Admin Dashboard");
        }

        menuItems.Add("Logout");

        _console.WriteMenu(menuItems);
        var choice = _console.GetMenuChoice(menuItems.Count);

        switch (choice)
        {
            case 1:
                await ShowBrowseBooksAsync();
                break;
            case 2:
                await ShowMyBooksAsync();
                break;
            case 3:
                await ShowMyAccountAsync();
                break;
            case 4 when _authService.HasRole(Models.UserRole.Admin):
                await ShowAdminDashboardAsync();
                break;
            case var logoutChoice when logoutChoice == menuItems.Count:
                _authService.Logout();
                _console.WriteSuccess("Logged out successfully");
                break;
        }
    }

    private async Task HandleLoginAsync()
    {
        _console.WriteTitle("Login");
        var userName = _console.GetInput("Username");
        var password = _console.GetPassword();

        var loginDto = new DTOs.LoginDto { UserName = userName, Password = password };
        var result = await _authService.LoginAsync(loginDto);

        if (result.Success)
        {
            _console.WriteSuccess(result.Message);
        }
        else
        {
            _console.WriteError(result.Message);
            _console.GetInput("Press Enter to continue", false);
        }
    }

    private async Task HandleRegisterAsync()
    {
        _console.WriteTitle("Register");
        var userName = _console.GetInput("Username");
        var password = _console.GetPassword();
        
        var createUserDto = new DTOs.CreateUserDto 
        { 
            UserName = userName, 
            Password = password, 
            UserAccess = "User" 
        };
        
        var result = await _authService.RegisterAsync(createUserDto);

        if (result.Success)
        {
            _console.WriteSuccess(result.Message);
        }
        else
        {
            _console.WriteError(result.Message);
        }
        
        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowBrowseBooksAsync()
    {
        _console.WriteTitle("Browse Books");
        var books = await _bookService.GetAvailableBooksAsync();
        var bookList = books.ToList();

        if (!bookList.Any())
        {
            _console.WriteLine("No books available for checkout.");
            _console.GetInput("Press Enter to continue", false);
            return;
        }

        _console.WriteLine($"{"ID",-10} {"Title",-30} {"Author",-20} {"Genre",-15}");
        _console.WriteLine(new string('-', 80));

        foreach (var book in bookList)
        {
            _console.WriteLine($"{book.BookID,-10} {book.BookTitle,-30} {book.BookAuthor,-20} {book.BookGenre,-15}");
        }

        _console.WriteLine();
        var bookId = _console.GetInput("Enter Book ID to checkout (or press Enter to go back)", false);
        
        if (!string.IsNullOrWhiteSpace(bookId))
        {
            var currentUser = _authService.GetCurrentUser()!;
            var checkoutDto = new DTOs.CheckoutBookDto { BookID = bookId, UserID = currentUser.UserID };
            var result = await _bookService.CheckoutBookAsync(checkoutDto);

            if (result.Success)
            {
                _console.WriteSuccess(result.Message);
            }
            else
            {
                _console.WriteError(result.Message);
            }
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowMyBooksAsync()
    {
        var currentUser = _authService.GetCurrentUser()!;
        _console.WriteTitle("My Checked Out Books");
        
        var books = await _bookService.GetUserBooksAsync(currentUser.UserID);
        var bookList = books.ToList();

        if (!bookList.Any())
        {
            _console.WriteLine("You have no books checked out.");
            _console.GetInput("Press Enter to continue", false);
            return;
        }

        _console.WriteLine($"{"ID",-10} {"Title",-30} {"Author",-20} {"Checked Out",-12}");
        _console.WriteLine(new string('-', 75));

        foreach (var book in bookList)
        {
            var checkoutDate = book.CheckedOutAt?.ToString("MM/dd/yyyy") ?? "Unknown";
            _console.WriteLine($"{book.BookID,-10} {book.BookTitle,-30} {book.BookAuthor,-20} {checkoutDate,-12}");
        }

        _console.WriteLine();
        var bookId = _console.GetInput("Enter Book ID to return (or press Enter to go back)", false);
        
        if (!string.IsNullOrWhiteSpace(bookId))
        {
            var result = await _bookService.ReturnBookAsync(bookId, currentUser.UserID);

            if (result.Success)
            {
                _console.WriteSuccess(result.Message);
                if (result.Fee > 0)
                {
                    _console.WriteWarning($"Late fee of {result.Fee:C} has been charged to your account.");
                }
            }
            else
            {
                _console.WriteError(result.Message);
            }
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowMyAccountAsync()
    {
        var currentUser = _authService.GetCurrentUser()!;
        _console.WriteTitle("My Account");
        
        _console.WriteLine($"Username: {currentUser.UserName}");
        _console.WriteLine($"User ID: {currentUser.UserID}");
        _console.WriteLine($"Balance: {currentUser.UserBalance:C}");
        _console.WriteLine($"Role: {currentUser.UserAccess}");
        _console.WriteLine($"Member Since: {currentUser.CreatedAt:MM/dd/yyyy}");
        
        if (currentUser.LastLoginAt.HasValue)
        {
            _console.WriteLine($"Last Login: {currentUser.LastLoginAt:MM/dd/yyyy HH:mm}");
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowAdminDashboardAsync()
    {
        _console.WriteTitle("Admin Dashboard");
        _console.WriteMenu(new[] 
        { 
            "Manage Books", 
            "Manage Users", 
            "View All Checkouts", 
            "Back to Main Menu" 
        });

        var choice = _console.GetMenuChoice(4);
        switch (choice)
        {
            case 1:
                await ShowManageBooksAsync();
                break;
            case 2:
                await ShowManageUsersAsync();
                break;
            case 3:
                await ShowAllCheckoutsAsync();
                break;
            case 4:
                return;
        }
    }

    private async Task ShowManageBooksAsync()
    {
        _console.WriteTitle("Manage Books");
        _console.WriteMenu(new[] { "Add Book", "Delete Book", "View All Books", "Back" });

        var choice = _console.GetMenuChoice(4);
        switch (choice)
        {
            case 1:
                await HandleAddBookAsync();
                break;
            case 2:
                await HandleDeleteBookAsync();
                break;
            case 3:
                await ShowAllBooksAsync();
                break;
            case 4:
                return;
        }
    }

    private async Task HandleAddBookAsync()
    {
        _console.WriteTitle("Add New Book");
        var title = _console.GetInput("Book Title");
        var author = _console.GetInput("Author");
        var genre = _console.GetInput("Genre");

        var createBookDto = new DTOs.CreateBookDto
        {
            BookTitle = title,
            BookAuthor = author,
            BookGenre = genre
        };

        var result = await _bookService.CreateBookAsync(createBookDto);
        if (result.Success)
        {
            _console.WriteSuccess(result.Message);
        }
        else
        {
            _console.WriteError(result.Message);
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task HandleDeleteBookAsync()
    {
        _console.WriteTitle("Delete Book");
        var bookId = _console.GetInput("Book ID to delete");

        if (_console.GetYesNo($"Are you sure you want to delete book '{bookId}'?"))
        {
            var result = await _bookService.DeleteBookAsync(bookId);
            if (result.Success)
            {
                _console.WriteSuccess(result.Message);
            }
            else
            {
                _console.WriteError(result.Message);
            }
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowAllBooksAsync()
    {
        _console.WriteTitle("All Books");
        var books = await _bookService.GetAllBooksAsync();
        var bookList = books.ToList();

        if (!bookList.Any())
        {
            _console.WriteLine("No books in the library.");
        }
        else
        {
            _console.WriteLine($"{"ID",-10} {"Title",-30} {"Author",-20} {"Genre",-15} {"Status",-15}");
            _console.WriteLine(new string('-', 95));

            foreach (var book in bookList)
            {
                var status = book.IsCheckedOut ? $"Out ({book.CheckedOutByName})" : "Available";
                _console.WriteLine($"{book.BookID,-10} {book.BookTitle,-30} {book.BookAuthor,-20} {book.BookGenre,-15} {status,-15}");
            }
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowManageUsersAsync()
    {
        _console.WriteTitle("Manage Users");
        var users = await _userService.GetAllUsersAsync();
        var userList = users.ToList();

        if (!userList.Any())
        {
            _console.WriteLine("No users found.");
        }
        else
        {
            _console.WriteLine($"{"ID",-15} {"Username",-20} {"Role",-10} {"Balance",-12} {"Created",-12}");
            _console.WriteLine(new string('-', 75));

            foreach (var user in userList)
            {
                _console.WriteLine($"{user.UserID,-15} {user.UserName,-20} {user.UserAccess,-10} {user.UserBalance,-12:C} {user.CreatedAt,-12:MM/dd/yyyy}");
            }
        }

        _console.GetInput("Press Enter to continue", false);
    }

    private async Task ShowAllCheckoutsAsync()
    {
        _console.WriteTitle("All Checked Out Books");
        var books = await _bookService.GetAllBooksAsync();
        var checkedOutBooks = books.Where(b => b.IsCheckedOut).ToList();

        if (!checkedOutBooks.Any())
        {
            _console.WriteLine("No books are currently checked out.");
        }
        else
        {
            _console.WriteLine($"{"Book ID",-10} {"Title",-30} {"User",-20} {"Checked Out",-12}");
            _console.WriteLine(new string('-', 75));

            foreach (var book in checkedOutBooks)
            {
                var checkoutDate = book.CheckedOutAt?.ToString("MM/dd/yyyy") ?? "Unknown";
                _console.WriteLine($"{book.BookID,-10} {book.BookTitle,-30} {book.CheckedOutByName,-20} {checkoutDate,-12}");
            }
        }

        _console.GetInput("Press Enter to continue", false);
    }
}