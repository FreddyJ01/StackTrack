using StackTrack.ConsoleApp.DTOs;
using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.Services;

public interface IAuthenticationService
{
    Task<(bool Success, UserDto? User, string Message)> LoginAsync(LoginDto loginDto);
    Task<(bool Success, UserDto? User, string Message)> RegisterAsync(CreateUserDto createUserDto);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    UserDto? GetCurrentUser();
    void SetCurrentUser(UserDto? user);
    void Logout();
    bool IsLoggedIn();
    bool HasRole(UserRole requiredRole);
}