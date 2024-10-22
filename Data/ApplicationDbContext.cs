// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Models; // Adjust the namespace if needed

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
     public DbSet<AdminUser> AdminUsers { get; set; }
     public DbSet<Product> Products { get; set; }
     public DbSet<Order> Orders { get; set; }
     public DbSet<OrderItem> OrderItems { get; set; }

    


    // You can add DbSet properties for other entities here if needed
    // public DbSet<OtherEntity> OtherEntities { get; set; }
}
