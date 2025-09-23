using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackTrack.RefactoredApp.Configuration;
using StackTrack.RefactoredApp.DTOs;
using StackTrack.RefactoredApp.Models;
using StackTrack.RefactoredApp.Repositories;
using StackTrack.RefactoredApp.Services;
using BCrypt.Net;
using FluentValidation;

namespace StackTrack.RefactoredApp.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly AppSettings _appSettings;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<LoginDto> _loginValidator;
    private UserDto? _currentUser;

    public AuthenticationService(
        IUserRepository userRepository,
        ILogger<AuthenticationService> logger,
        IOptions<AppSettings> appSettings,
        IValidator<CreateUserDto> createUserValidator,
        IValidator<LoginDto> loginValidator)
    {
        _userRepository = userRepository;
        _logger = logger;
        _appSettings = appSettings.Value;
        _createUserValidator = createUserValidator;
        _loginValidator = loginValidator;
    }

    public async Task<(bool Success, UserDto? User, string Message)> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return (false, null, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var user = await _userRepository.GetByNameAsync(loginDto.UserName);
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt for user: {UserName}", loginDto.UserName);
                return (false, null, "Invalid username or password");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var userDto = MapToDto(user);
            SetCurrentUser(userDto);

            _logger.LogInformation("User {UserName} logged in successfully", user.UserName);
            return (true, userDto, "Login successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user: {UserName}", loginDto.UserName);
            return (false, null, "An error occurred during login");
        }
    }

    public async Task<(bool Success, UserDto? User, string Message)> RegisterAsync(CreateUserDto createUserDto)
    {
        try
        {
            var validationResult = await _createUserValidator.ValidateAsync(createUserDto);
            if (!validationResult.IsValid)
            {
                return (false, null, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var existingUser = await _userRepository.GetByNameAsync(createUserDto.UserName);
            if (existingUser != null)
            {
                return (false, null, "Username already exists");
            }

            var userId = Guid.NewGuid().ToString();
            var user = new User
            {
                UserID = userId,
                UserName = createUserDto.UserName,
                PasswordHash = HashPassword(createUserDto.Password),
                UserBalance = 0.0,
                UserAccess = Enum.Parse<UserRole>(createUserDto.UserAccess),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);
            var userDto = MapToDto(user);

            _logger.LogInformation("New user registered: {UserName}", user.UserName);
            return (true, userDto, "Registration successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for user: {UserName}", createUserDto.UserName);
            return (false, null, "An error occurred during registration");
        }
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public UserDto? GetCurrentUser() => _currentUser;

    public void SetCurrentUser(UserDto? user) => _currentUser = user;

    public void Logout()
    {
        if (_currentUser != null)
        {
            _logger.LogInformation("User {UserName} logged out", _currentUser.UserName);
        }
        _currentUser = null;
    }

    public bool IsLoggedIn() => _currentUser != null;

    public bool HasRole(UserRole requiredRole)
    {
        if (_currentUser == null) return false;
        
        var currentRole = Enum.Parse<UserRole>(_currentUser.UserAccess);
        return currentRole >= requiredRole;
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