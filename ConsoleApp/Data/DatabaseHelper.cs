using Microsoft.Data.Sqlite;
namespace StackTrack.ConsoleApp.Data;

public static class DatabaseHelper
{
    public static string connectionString = "Data Source=/Users/freddy/Desktop/PARA/1Projects/Code/StackTrack/ConsoleApp/Data/StackTrack.db";
    public static void PushStart()
    {
        InitializeUsers();
        InitializeBooks();
    }

    // Creates user table in our db if it doesn't already exist
    public static void InitializeUsers()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            CREATE TABLE IF NOT EXISTS Users (
            Id TEXT NOT NULL PRIMARY KEY,
            Name TEXT NOT NULL,
            Password TEXT NOT NULL,
            Balance REAL NOT NULL
        );
    ";
        command.ExecuteNonQuery();
    }

    public static void InitializeBooks()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            CREATE TABLE IF NOT EXISTS Books (
            BookID TEXT NOT NULL PRIMARY KEY,
            BookTitle TEXT NOT NULL,
            BookAuthor TEXT NOT NULL,
            BookGenre TEXT NOT NULL,
            CheckedOutByID TEXT,
            CheckedOutAt DATETIME
        );
    ";
        command.ExecuteNonQuery();
    }
}