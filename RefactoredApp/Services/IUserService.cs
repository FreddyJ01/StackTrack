using StackTrack.RefactoredApp.DTOs;

namespace StackTrack.RefactoredApp.Services;

public interface IUserService
{
    Task<(bool Success, string Message)> CreateUserAsync(CreateUserDto createUserDto);
    Task<(bool Success, string Message)> UpdateUserBalanceAsync(UpdateUserBalanceDto updateBalanceDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string userId);
    Task<UserDto?> GetUserByNameAsync(string userName);
    Task<(bool Success, string Message)> DeleteUserAsync(string userId);
    Task<double> GetUserBalanceAsync(string userId);
}