using BackendCommerceApp.Components;
using BackendCommerceApp.Data;
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
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
