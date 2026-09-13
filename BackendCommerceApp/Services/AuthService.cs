using BackendCommerceApp.Data;
using BackendCommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCommerceApp.Services;

public class AuthService
{
    private readonly AppDbContext _dbContext;

    public LoginInformation? CurrentUser { get; private set; }
    public event Action? CurrentUserChanged;

    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoginInformation?> LoginAsync(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        CurrentUser = await _dbContext.LoginInformation
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail && user.Password == password);
        CurrentUserChanged?.Invoke();

        return CurrentUser;
    }

    public async Task<LoginInformation?> SignUpAsync(string firstName, string lastName, string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var emailExists = await _dbContext.LoginInformation
            .AnyAsync(user => user.Email == normalizedEmail);

        if (emailExists)
        {
            return null;
        }

        var user = new LoginInformation
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = normalizedEmail,
            Password = password,
            Admin = false
        };

        _dbContext.LoginInformation.Add(user);

        await _dbContext.SaveChangesAsync();
        CurrentUser = user;
        CurrentUserChanged?.Invoke();
        return CurrentUser;
    }

    public void Logout()
    {
        CurrentUser = null;
        CurrentUserChanged?.Invoke();
    }
}
