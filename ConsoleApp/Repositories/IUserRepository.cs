using StackTrack.ConsoleApp.Models;

namespace StackTrack.ConsoleApp.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string userId);
    Task<User?> GetByNameAsync(string userName);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(string userId);
    Task<bool> ExistsAsync(string userId);
    Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
}