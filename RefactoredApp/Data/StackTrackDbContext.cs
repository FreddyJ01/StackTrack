using Microsoft.EntityFrameworkCore;
using StackTrack.RefactoredApp.Models;

namespace StackTrack.RefactoredApp.Data;

public class StackTrackDbContext : DbContext
{
    public StackTrackDbContext(DbContextOptions<StackTrackDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID);
            entity.HasIndex(e => e.UserName).IsUnique();
            entity.Property(e => e.UserID).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.UserAccess).HasConversion<int>();
        });

        // Book configuration
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookID);
            entity.Property(e => e.BookID).HasMaxLength(50);
            entity.Property(e => e.BookTitle).HasMaxLength(200).IsRequired();
            entity.Property(e => e.BookAuthor).HasMaxLength(100).IsRequired();
            entity.Property(e => e.BookGenre).HasMaxLength(50).IsRequired();
            entity.Property(e => e.CheckedOutByID).HasMaxLength(50);

            // Foreign key relationship
            entity.HasOne(b => b.CheckedOutBy)
                  .WithMany()
                  .HasForeignKey(b => b.CheckedOutByID)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}