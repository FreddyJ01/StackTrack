using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackTrack.RefactoredApp.Application;
using StackTrack.RefactoredApp.Data;
using StackTrack.RefactoredApp.Infrastructure;
using StackTrack.RefactoredApp.Models;

namespace StackTrack.RefactoredApp;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();

            // Ensure database is created and seeded
            await EnsureDatabaseCreatedAsync(host.Services);

            var app = host.Services.GetRequiredService<IApplicationService>();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddStackTrackServices(context.Configuration);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            });

    static async Task EnsureDatabaseCreatedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<StackTrackDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Database ensured created");

            // Seed default admin user if no users exist
            if (!await context.Users.AnyAsync())
            {
                var adminUser = new User
                {
                    UserID = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    UserBalance = 0.0,
                    UserAccess = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                };

                var testBook = new Book
                {
                    BookID = Guid.NewGuid().ToString(),
                    BookTitle = "The Great Gatsby",
                    BookAuthor = "F. Scott Fitzgerald",
                    BookGenre = "Classic Literature",
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(adminUser);
                context.Books.Add(testBook);
                await context.SaveChangesAsync();
                
                logger.LogInformation("Default admin user and sample book created");
                logger.LogInformation("Login with username: admin, password: admin123");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error ensuring database created");
            throw;
        }
    }
}
