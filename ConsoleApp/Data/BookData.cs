using Microsoft.Data.Sqlite;
using StackTrack.ConsoleApp.AccountServices;
using StackTrack.ConsoleApp.Models;
namespace StackTrack.ConsoleApp.Data;

class BookData
{
    public static List<Book> QueryAllBooks()
    {
        List<Book> books = new List<Book>();
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT BookID, BookTitle, BookAuthor, BookGenre, CheckedOutByID, CheckedOutAt FROM Books";
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            books.Add(new Book
            {
                BookID = reader.GetString(0),
                BookTitle = reader.GetString(1),
                BookAuthor = reader.GetString(2),
                BookGenre = reader.GetString(3),
                CheckedOutByID = reader.IsDBNull(4) ? "" : reader.GetString(4),
                CheckedOutAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
            });
        }
        return books;
    }
    public static List<Book> QueryBooksByGenre(string genre)
    {
        List<Book> books = new List<Book>();
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT BookID, BookTitle, BookAuthor, BookGenre, CheckedOutByID, CheckedOutAt FROM Books WHERE LOWER(BookGenre) = LOWER($genre)
        ";
        command.Parameters.AddWithValue("$genre", genre);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            books.Add(new Book
            {
                BookID = reader.GetString(0),
                BookTitle = reader.GetString(1),
                BookAuthor = reader.GetString(2),
                BookGenre = reader.GetString(3),
                CheckedOutByID = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                CheckedOutAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
            });
        }

        return books;
    }
    public static List<Book> QueryBooksByAuthor(string author)
    {
        List<Book> books = new List<Book>();
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT BookID, BookTitle, BookAuthor, BookGenre, CheckedOutByID, CheckedOutAt FROM Books WHERE LOWER(BookAuthor) = LOWER($author)
        ";
        command.Parameters.AddWithValue("$author", author);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            books.Add(new Book
            {
                BookID = reader.GetString(0),
                BookTitle = reader.GetString(1),
                BookAuthor = reader.GetString(2),
                BookGenre = reader.GetString(3),
                CheckedOutByID = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                CheckedOutAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
            });
        }

        return books;
    }
    public static CheckoutResult TryCheckoutBook(string bookTitle)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var checkCommand = connection.CreateCommand();
        checkCommand.CommandText =
        @"
        SELECT CheckedOutByID FROM Books WHERE BookTitle = $bookTitle
        ";
        checkCommand.Parameters.AddWithValue("$bookTitle", bookTitle);

        using var reader = checkCommand.ExecuteReader();
        if (reader.Read())
        {
            string checkedOutById = reader.IsDBNull(0) ? "" : reader.GetString(0);
            if (!string.IsNullOrEmpty(checkedOutById))
            {
                // Book already checked out
                return CheckoutResult.AlreadyCheckedOut;
            }
        }
        else
        {
            // Book With That Title Not Found
            return CheckoutResult.NotFound;
        }
        reader.Close();

        // Book available, Check Out
        var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
        @"
        UPDATE Books 
        SET CheckedOutByID = $checkedOutById, CheckedOutAt = $checkedOutAt
        WHERE BookTitle = $bookTitle;
        ";
        updateCommand.Parameters.AddWithValue("$checkedOutById", UserIdentification.currentUserID);
        updateCommand.Parameters.AddWithValue("$checkedOutAt", DateTime.Now);
        updateCommand.Parameters.AddWithValue("$bookTitle", bookTitle);

        updateCommand.ExecuteNonQuery();
        return CheckoutResult.Success;
    }
    public enum CheckoutResult
    {
        Success,
        NotFound,
        AlreadyCheckedOut
    } 
}