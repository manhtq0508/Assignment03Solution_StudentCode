using BussinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObject;
public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("eStoreDB");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'eStoreDB' not found.");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderId, od.ProductId });

        modelBuilder.Entity<OrderDetail>()
            .HasOne<Order>()
            .WithMany(o => o.OrderDetails)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Beverages" },
            new Category { Id = 2, Name = "Condiments" },
            new Category { Id = 3, Name = "Confections" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Chai", CategoryId = 1, Weight = 0.2f, UnitPrice = 18.00m, UnitsInStock = 10 },
            new Product { Id = 2, Name = "Chang", CategoryId = 2, Weight = 1.2f, UnitPrice = 100.00m, UnitsInStock = 10 },
            new Product { Id = 3, Name = "Aniseed Syrup", CategoryId = 3, Weight = 0.25f, UnitPrice = 18.50m, UnitsInStock = 10 }
        );
    }
}
