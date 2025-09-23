using Microsoft.EntityFrameworkCore;
using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.Models;
using StackTrack.ConsoleApp.Repositories;

namespace StackTrack.ConsoleApp.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly StackTrackDbContext _context;

    public UserRepository(StackTrackDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
    }

    public async Task<User?> GetByNameAsync(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(string userId)
    {
        var user = await GetByIdAsync(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(string userId)
    {
        return await _context.Users.AnyAsync(u => u.UserID == userId);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
    {
        return await _context.Users.Where(u => u.UserAccess == role).ToListAsync();
    }
}