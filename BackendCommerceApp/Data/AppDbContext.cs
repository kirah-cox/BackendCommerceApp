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
    }
}
