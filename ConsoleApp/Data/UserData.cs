using Microsoft.Data.Sqlite;
using StackTrack.ConsoleApp.AccountServices;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.Data;

public class UserData
{
    public static List<User> QueryAllUsers()
    {
        List<User> users = new List<User>();

        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        SELECT Id, Name, Password, Balance, Access
        FROM Users
        ";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new User
            {
                userID = reader.GetString(0),
                userName = reader.GetString(1),
                userPassword = reader.GetString(2),
                userBalance = reader.GetDouble(3),
                userAccess = reader.GetString(4)
            });
        }
        return users;
    }

    public static List<User> QueryUserByFilter(string column, string filter)
    {
        List<User> users = new List<User>();
        using var connection = new SqliteConnection(DatabaseHelper.connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @$"
        SELECT Id, Name, Password, Balance, Access
        FROM Users 
        WHERE {column} = $filter LIMIT 1;
        ";
        command.Parameters.AddWithValue("$filter", filter);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new User
            {
                userID = reader.GetString(0),
                userName = reader.GetString(1),
                userPassword = reader.GetString(2),
                userBalance = reader.GetDouble(3),
                userAccess = reader.GetString(4)
            });
        }

        return users;
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
        INSERT INTO Users (Id, Name, Password, Balance, Access)
        VALUES ($id, $name, $password, $balance, $access)
        ";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$password", password);
        command.Parameters.AddWithValue("$balance", balance);
        command.Parameters.AddWithValue("$access", "User");

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