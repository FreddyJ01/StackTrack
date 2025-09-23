using Microsoft.Extensions.Logging;
using StackTrack.RefactoredApp.DTOs;
using StackTrack.RefactoredApp.Models;
using StackTrack.RefactoredApp.Repositories;
using StackTrack.RefactoredApp.Services;
using FluentValidation;

namespace StackTrack.RefactoredApp.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IValidator<CreateUserDto> _createUserValidator;

    public UserService(
        IUserRepository userRepository,
        ILogger<UserService> logger,
        IValidator<CreateUserDto> createUserValidator)
    {
        _userRepository = userRepository;
        _logger = logger;
        _createUserValidator = createUserValidator;
    }

    public async Task<(bool Success, string Message)> CreateUserAsync(CreateUserDto createUserDto)
    {
        try
        {
            var validationResult = await _createUserValidator.ValidateAsync(createUserDto);
            if (!validationResult.IsValid)
            {
                return (false, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var existingUser = await _userRepository.GetByNameAsync(createUserDto.UserName);
            if (existingUser != null)
            {
                return (false, "Username already exists");
            }

            var userId = Guid.NewGuid().ToString();
            var user = new User
            {
                UserID = userId,
                UserName = createUserDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                UserBalance = 0.0,
                UserAccess = Enum.Parse<UserRole>(createUserDto.UserAccess),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);
            _logger.LogInformation("User created: {UserName} ({UserId})", user.UserName, user.UserID);
            return (true, "User created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user: {UserName}", createUserDto.UserName);
            return (false, "An error occurred while creating the user");
        }
    }

    public async Task<(bool Success, string Message)> UpdateUserBalanceAsync(UpdateUserBalanceDto updateBalanceDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(updateBalanceDto.UserID);
            if (user == null)
            {
                return (false, "User not found");
            }

            var newBalance = user.UserBalance + updateBalanceDto.Amount;
            if (newBalance < 0)
            {
                return (false, "Insufficient balance");
            }

            user.UserBalance = newBalance;
            await _userRepository.UpdateAsync(user);

            _logger.LogInformation("Balance updated for user {UserName}: {Amount}", user.UserName, updateBalanceDto.Amount);
            return (true, $"Balance updated. New balance: {newBalance:C}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for user: {UserId}", updateBalanceDto.UserID);
            return (false, "An error occurred while updating the balance");
        }
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all users");
            return Enumerable.Empty<UserDto>();
        }
    }

    public async Task<UserDto?> GetUserByIdAsync(string userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null ? MapToDto(user) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user: {UserId}", userId);
            return null;
        }
    }

    public async Task<UserDto?> GetUserByNameAsync(string userName)
    {
        try
        {
            var user = await _userRepository.GetByNameAsync(userName);
            return user != null ? MapToDto(user) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user: {UserName}", userName);
            return null;
        }
    }

    public async Task<(bool Success, string Message)> DeleteUserAsync(string userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found");
            }

            var success = await _userRepository.DeleteAsync(userId);
            if (success)
            {
                _logger.LogInformation("User deleted: {UserName} ({UserId})", user.UserName, userId);
                return (true, "User deleted successfully");
            }

            return (false, "Failed to delete user");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user: {UserId}", userId);
            return (false, "An error occurred while deleting the user");
        }
    }

    public async Task<double> GetUserBalanceAsync(string userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user?.UserBalance ?? 0.0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving balance for user: {UserId}", userId);
            return 0.0;
        }
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            UserID = user.UserID,
            UserName = user.UserName,
            UserBalance = user.UserBalance,
            UserAccess = user.UserAccess.ToString(),
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt
        };
    }
}