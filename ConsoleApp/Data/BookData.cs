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
        command.CommandText =
        @"
        SELECT BookID, BookTitle, BookAuthor, BookGenre, CheckedOutByID, CheckedOutAt 
        FROM Books
        ";

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

    public static List<Book> QueryBooksByFilter(string column, string filter)
    {
        List<Book> books = new List<Book>();

        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @$"
        SELECT BookID, BookTitle, BookAuthor, BookGenre, CheckedOutByID, CheckedOutAt 
        FROM Books 
        WHERE LOWER({column}) = LOWER($filter)
        ";
        command.Parameters.AddWithValue("$filter", filter);

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

    public static List<Book> QueryBooksByCheckedOutByID(string currentUserID)
    {
        List<Book> books = new List<Book>();
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT BookID, BookTitle, BookAuthor, BookGenre, CheckedOutByID, CheckedOutAt FROM Books WHERE CheckedOutByID = $currentUserID
        ";
        command.Parameters.AddWithValue("$currentUserID", currentUserID);
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

    public static ActionResult TryCheckoutBook(string bookTitle)
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
                return ActionResult.Failure;
            }
        }
        else
        {
            // Book With That Title Not Found
            return ActionResult.InvalidSelection;
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
        return ActionResult.Success;
    }

    public static ActionResult TryReturnBook (string bookTitle)
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
            if (string.IsNullOrEmpty(checkedOutById))
            {
                // Book is not checked out
                return ActionResult.Failure;
            }
        }
        else
        {
            // Book With That Title Not Found
            return ActionResult.InvalidSelection;
        }
        reader.Close();

        // Book is indeed checked out
        var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
        @"
        UPDATE Books 
        SET CheckedOutByID = $checkedOutById, CheckedOutAt = $checkedOutAt
        WHERE BookTitle = $bookTitle;
        ";
        updateCommand.Parameters.AddWithValue("$checkedOutById", string.Empty);
        updateCommand.Parameters.AddWithValue("$checkedOutAt", DBNull.Value);
        updateCommand.Parameters.AddWithValue("$bookTitle", bookTitle);

        updateCommand.ExecuteNonQuery();
        return ActionResult.Success;
    }

    public static bool TryAddBook(string bookID, string bookTitle, string bookAuthor, string bookGenre)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        INSERT INTO Books
        (BookID, BookTitle, BookAuthor, BookGenre)
        VALUES ($bookID, $bookTitle, $bookAuthor, $bookGenre)
        ";
        command.Parameters.AddWithValue("$bookID", bookID);
        command.Parameters.AddWithValue("$bookTitle", bookTitle);
        command.Parameters.AddWithValue("$bookAuthor", bookAuthor);
        command.Parameters.AddWithValue("$bookGenre", bookGenre);

        if (command.ExecuteNonQuery() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool TryRemoveBook(string bookId)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        DELETE FROM Books
        WHERE LOWER(BookID) = LOWER($bookId)
        ";
        command.Parameters.AddWithValue("$bookId", bookId);

        if (command.ExecuteNonQuery() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public enum ActionResult
    {
        Success,
        InvalidSelection,
        Failure
    }
}