using Microsoft.EntityFrameworkCore;
using BackendCommerceApp.Models;

namespace BackendCommerceApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<LoginInformation> LoginInformation { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("BackendCommerceApp");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");
            entity.HasKey(product => product.Id);

            entity.Property(product => product.Id).HasColumnName("id");
            entity.Property(product => product.Name).HasColumnName("name");
            entity.Property(product => product.Description).HasColumnName("description");
            entity.Property(product => product.Category).HasColumnName("category");
            entity.Property(product => product.Price).HasColumnName("price");
        });

        modelBuilder.Entity<LoginInformation>(entity =>
        {
            entity.ToTable("login_information");
            entity.HasKey(loginInformation => loginInformation.UserId);

            entity.Property(loginInformation => loginInformation.UserId).HasColumnName("user_id");
            entity.Property(loginInformation => loginInformation.FirstName).HasColumnName("first_name");
            entity.Property(loginInformation => loginInformation.LastName).HasColumnName("last_name");
            entity.Property(loginInformation => loginInformation.Email).HasColumnName("email");
            entity.Property(loginInformation => loginInformation.Password).HasColumnName("password");
            entity.Property(loginInformation => loginInformation.Admin).HasColumnName("admin");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("order");
            entity.HasKey(order => order.OrderId);

            entity.Property(order => order.OrderId).HasColumnName("order_id");
            entity.Property(order => order.Date).HasColumnName("date");
            entity.Property(order => order.FirstName).HasColumnName("first_name");
            entity.Property(order => order.LastName).HasColumnName("last_name");
            entity.Property(order => order.TotalPrice).HasColumnName("total_price");
            entity.Property(order => order.Fulfilled).HasColumnName("fulfilled");
        });
    }
}
