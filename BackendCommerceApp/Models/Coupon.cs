namespace BackendCommerceApp.Models;

public class Coupon
{
    public string PromoCode { get; set; } = string.Empty;
    public decimal PercentOff { get; set; }
}