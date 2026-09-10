using BackendCommerceApp.Data;
using BackendCommerceApp.Models;
using Microsoft.Extensions.Logging;

namespace BackendCommerceApp.Services;

public class ProductService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ProductService> _logger;

    public ProductService(AppDbContext dbContext, ILogger<ProductService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Product> AddProductAsync(string name, string description, decimal price)
    {
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price
        };

        _logger.LogInformation("Adding product {ProductName} with price {Price}.", name, price);

        try
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Product {ProductId} saved successfully.", product.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save product {ProductName}.", name);
            throw;
        }

        return product;
    }
}
