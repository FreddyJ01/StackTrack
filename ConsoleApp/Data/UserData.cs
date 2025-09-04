using Microsoft.Data.Sqlite;
using StackTrack.ConsoleApp.AccountServices;
namespace StackTrack.ConsoleApp.Data;

public class UserData
{
    public static string? QueryIdByUsername(string usernameAttempt)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id FROM Users WHERE Name = $name LIMIT 1;";
        command.Parameters.AddWithValue("$name", usernameAttempt);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return reader.GetString(0);
        }
        else
        {
            return null;
        }
    }

    public static string? QueryPasswordById(string userID)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Password FROM Users WHERE Id = $id LIMIT 1;";
        command.Parameters.AddWithValue("$id", userID);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return reader.GetString(0);
        }
        else
        {
            return null;
        }
    }

    public static string QueryUsernameById(string id)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Name FROM Users WHERE Id = $id LIMIT 1;";
        command.Parameters.AddWithValue("$id", id);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return reader.GetString(0);
        }
        else
        {
            return null;
        }
    }

    public static string? QueryUserField(string field, string column, string value)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @$"
        SELECT {field} FROM Users WHERE {column} = $value LIMIT 1;
        ";
        command.Parameters.AddWithValue("$value", value);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return reader.GetString(0);
        }
        else
        {
            return null;
        }
    }

    public static bool VerifyUniqueUsername(string username)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT 1 FROM Users WHERE Name = $name LIMIT 1;";
        command.Parameters.AddWithValue("$name", username);
        using var reader = command.ExecuteReader();
        return reader.Read();
    }

    public static void AddUserToDb(string id, string name, string password, double balance)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
        INSERT INTO Users (Id, Name, Password, Balance)
        VALUES ($id, $name, $password, $balance)
        ";

        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$password", password);
        command.Parameters.AddWithValue("$balance", balance);
        command.ExecuteNonQuery();
    }

    public static void DeleteUserByNameAndPassword(string name, string password)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText =
        @"
        DELETE FROM Users
        WHERE Name = $name AND Password = $password
        ";
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$password", password);
        command.ExecuteNonQuery();
    }

    public static void UpdateUserBalance(double accruedBalance)
    {
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        UPDATE Users
        SET Balance = Balance + $accruedBalance 
        WHERE Id = $id
        ;";
        
        command.Parameters.AddWithValue("$id", UserIdentification.currentUserID);
        command.Parameters.AddWithValue("$accruedBalance", accruedBalance);

        command.ExecuteNonQuery();
    }
}