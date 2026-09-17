using BackendCommerceApp.Components;
using BackendCommerceApp.Data;
using BackendCommerceApp.Models;
using BackendCommerceApp.Services;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("DefaultConnection")))
{
    builder.Configuration.AddDotNetEnv(".env", DotNetEnv.LoadOptions.TraversePath());
}

// Configure data protection to persist keys
builder.Services.AddDataProtection()
    .SetApplicationName("BackendCommerceApp");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Entity Framework Core with PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? BuildConnectionStringFromEnvironment(builder.Configuration);

var connectionLogger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger("DatabaseConfiguration");
var connectionBuilder = new Npgsql.NpgsqlConnectionStringBuilder(connectionString);
connectionLogger.LogInformation(
    "Database configuration loaded. Host: {Host}, Port: {Port}, Database: {Database}, User: {User}",
    connectionBuilder.Host,
    connectionBuilder.Port,
    connectionBuilder.Database,
    connectionBuilder.Username);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

    if (!await dbContext.Products.AnyAsync())
    {
        dbContext.Products.AddRange(
            new Product { Name = "Wireless Headphones", Description = "Noise-cancelling over-ear headphones with Bluetooth connectivity.", Category = "electronics", Price = 79.99m },
            new Product { Name = "Smart Watch", Description = "Fitness-focused smartwatch with heart-rate monitoring and GPS.", Category = "electronics", Price = 129.99m },
            new Product { Name = "Running Shoes", Description = "Lightweight cushioned shoes designed for daily road running.", Category = "clothing", Price = 64.50m },
            new Product { Name = "Denim Jacket", Description = "Classic mid-weight denim jacket with a comfortable regular fit.", Category = "clothing", Price = 58.00m },
            new Product { Name = "Coffee Maker", Description = "Programmable drip coffee maker with a twelve-cup glass carafe.", Category = "home-garden", Price = 44.95m },
            new Product { Name = "Garden Tool Set", Description = "Five-piece stainless steel hand tool set for everyday gardening.", Category = "home-garden", Price = 32.49m },
            new Product { Name = "Yoga Mat", Description = "Non-slip, six-millimeter exercise mat with a textured surface.", Category = "sports-outdoors", Price = 24.99m },
            new Product { Name = "Camping Tent", Description = "Two-person waterproof tent with a quick-pitch frame.", Category = "sports-outdoors", Price = 89.00m },
            new Product { Name = "Building Blocks Set", Description = "Creative building set with 500 colorful pieces for ages six and up.", Category = "toys-games", Price = 29.99m },
            new Product { Name = "Board Game Bundle", Description = "Family game bundle containing three strategy and party games.", Category = "toys-games", Price = 39.95m },
            new Product { Name = "Mystery Novel", Description = "A fast-paced detective mystery set in a remote coastal town.", Category = "books", Price = 14.99m },
            new Product { Name = "Cookbook", Description = "A practical collection of simple seasonal recipes for home cooks.", Category = "books", Price = 22.50m }
        );

        await dbContext.SaveChangesAsync();
    }

    const string adminEmail = "admin@backendcommerce.local";
    if (!await dbContext.LoginInformation.AnyAsync(user => user.Email == adminEmail))
    {
        dbContext.LoginInformation.Add(new LoginInformation
        {
            FirstName = "Admin",
            LastName = "User",
            Email = adminEmail,
            Password = "Admin123!",
            Admin = true
        });

        await dbContext.SaveChangesAsync();
    }

    var seedCoupons = new[]
    {
        new Coupon { PromoCode = "SAVE10", PercentOff = 10m },
        new Coupon { PromoCode = "WELCOME20", PercentOff = 20m }
    };

    foreach (var coupon in seedCoupons)
    {
        if (!await dbContext.Coupons.AnyAsync(existingCoupon => existingCoupon.PromoCode == coupon.PromoCode))
        {
            dbContext.Coupons.Add(coupon);
        }
    }

    await dbContext.SaveChangesAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static string BuildConnectionStringFromEnvironment(IConfiguration configuration)
{
    var host = configuration["DB_HOST"];
    var port = configuration["DB_PORT"];
    var database = configuration["DB_NAME"];
    var user = configuration["DB_USER"];
    var password = configuration["DB_PASSWORD"];

    if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(port) ||
        string.IsNullOrWhiteSpace(database) || string.IsNullOrWhiteSpace(user) ||
        string.IsNullOrWhiteSpace(password))
    {
        throw new InvalidOperationException(
            "Database configuration is missing. Set ConnectionStrings__DefaultConnection or DB_HOST, DB_PORT, DB_NAME, DB_USER, and DB_PASSWORD.");
    }

    return $"Server={host};Port={port};Database={database};User Id={user};Password={password};SSL Mode=Require";
}
