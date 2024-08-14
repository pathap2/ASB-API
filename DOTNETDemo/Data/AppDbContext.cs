using DOTNETDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOTNETDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(s => s.Id);

            // Seed Data
            entity.HasData(
                new User
                {
                    Id = 1,
                    Name = "John Doe",
                    CardNumber = "4987654321098769",
                    CVC = "123",
                    ExpiryDate = new DateTime(2025, 12, 31)
                },
                new User
                {
                    Id = 2,
                    Name = "Jane Smith",
                    CardNumber = "5123456789012346",
                    CVC = "456",
                    ExpiryDate = new DateTime(2024, 11, 30)
                }
            );
        });
    }
}
