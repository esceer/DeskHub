using DeskHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskHub.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Desk> Desks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set primary key
        modelBuilder.Entity<Desk>().HasKey(desk => desk.Id);

        // Seed some initial data
        modelBuilder.Entity<Desk>().HasData(
            new Desk
            {
                Id = Guid.Parse("f4cc7e3c-97a6-490a-bd0d-679c1f951be9"),
                Code = "1A",
                IsActive = true
            },
            new Desk
            {
                Id = Guid.Parse("5f360979-6d13-4bde-b501-8b1c502a3036"),
                Code = "2B",
                IsActive = false
            }
        );
    }
}
