using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackTrack.ConsoleApp.Application;
using StackTrack.ConsoleApp.Configuration;
using StackTrack.ConsoleApp.Data;
using StackTrack.ConsoleApp.DTOs;
using StackTrack.ConsoleApp.Repositories;
using StackTrack.ConsoleApp.Repositories.Implementations;
using StackTrack.ConsoleApp.Services;
using StackTrack.ConsoleApp.Services.Implementations;
using StackTrack.ConsoleApp.UI;
using StackTrack.ConsoleApp.UI.Implementations;
using StackTrack.ConsoleApp.Validators;
using FluentValidation;

namespace StackTrack.ConsoleApp.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStackTrackServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        // Database
        services.AddDbContext<StackTrackDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();

        // Services
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();

        // UI
        services.AddSingleton<IConsoleService, ConsoleService>();

        // Application
        services.AddScoped<IApplicationService, ApplicationService>();

        // Validators
        services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();
        services.AddScoped<IValidator<CheckoutBookDto>, CheckoutBookDtoValidator>();

        // Logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddConfiguration(configuration.GetSection("Logging"));
        });

        return services;
    }
}