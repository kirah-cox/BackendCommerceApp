using BackendCommerceApp.Data;
using BackendCommerceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendCommerceApp.Services;

public class CouponService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<CouponService> _logger;

    public CouponService(AppDbContext dbContext, ILogger<CouponService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Coupon>> GetCouponsAsync()
    {
        return await _dbContext.Coupons
            .AsNoTracking()
            .OrderBy(coupon => coupon.PromoCode)
            .ToListAsync();
    }

    public async Task<Coupon> CreateCouponAsync(string promoCode, decimal percentOff)
    {
        var normalizedCode = promoCode.Trim().ToUpperInvariant();

        var codeExists = await _dbContext.Coupons.AnyAsync(coupon => coupon.PromoCode == normalizedCode);
        if (codeExists)
        {
            throw new InvalidOperationException($"A coupon with promo code '{normalizedCode}' already exists.");
        }

        var coupon = new Coupon
        {
            PromoCode = normalizedCode,
            PercentOff = percentOff
        };

        _logger.LogInformation("Creating coupon {PromoCode} with {PercentOff}% off.", normalizedCode, percentOff);

        try
        {
            _dbContext.Coupons.Add(coupon);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Coupon {PromoCode} saved successfully.", normalizedCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save coupon {PromoCode}.", normalizedCode);
            throw;
        }

        return coupon;
    }

    public async Task DeleteCouponAsync(string promoCode)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.PromoCode == promoCode);
        if (coupon is null)
        {
            return;
        }

        _dbContext.Coupons.Remove(coupon);
        await _dbContext.SaveChangesAsync();
    }
}
