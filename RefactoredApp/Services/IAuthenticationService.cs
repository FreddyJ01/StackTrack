using StackTrack.RefactoredApp.DTOs;
using StackTrack.RefactoredApp.Models;

namespace StackTrack.RefactoredApp.Services;

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