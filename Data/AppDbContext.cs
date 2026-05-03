using Microsoft.EntityFrameworkCore;
using OnlineProdavniceEF.Models;

namespace OnlineProdavniceEF.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineProdavniceEF_Simple;Trusted_Connection=True;MultipleActiveResultSets=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Oprema" },
            new Category { Id = 2, Name = "Laptopovi" },
            new Category { Id = 3, Name = "Telefoni" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Gaming miš", Price = 2490, Quantity = 15, CategoryId = 1, Description = "Osnovni gaming miš.", ImageUrl = "/images/product-placeholder.png" },
            new Product { Id = 2, Name = "Laptop Start", Price = 59990, Quantity = 5, CategoryId = 2, Description = "Laptop za školu i posao.", ImageUrl = "/images/product-placeholder.png" },
            new Product { Id = 3, Name = "Telefon Mini", Price = 29990, Quantity = 8, CategoryId = 3, Description = "Praktičan telefon.", ImageUrl = "/images/product-placeholder.png" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FullName = "Administrator", Email = "admin@prodavnica.com", Password = "admin123", IsAdmin = true }
        );
    }
}
