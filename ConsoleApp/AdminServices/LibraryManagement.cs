using Microsoft.Data.Sqlite;
using StackTrack.ConsoleApp.Data;
namespace StackTrack.ConsoleApp.AdminServices;

class LibraryManagement
{
    public static void AddBookToLibrary(string bookID, string title, string author, string genre)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
        @"
        INSERT INTO Books (BookID, BookTitle, BookAuthor, BookGenre) VALUES ($bookID, $title, $author, $genre)
        ";

        command.Parameters.AddWithValue("$bookID", bookID);
        command.Parameters.AddWithValue("$title", title);
        command.Parameters.AddWithValue("$author", author);
        command.Parameters.AddWithValue("$genre", genre);

        command.ExecuteNonQuery();
    }
}