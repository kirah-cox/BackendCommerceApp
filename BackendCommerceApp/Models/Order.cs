namespace BackendCommerceApp.Models;

public class Order
{
    public int OrderId { get; set; }
    public int? UserId { get; set; }
    public DateTime Date { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public bool Fulfilled { get; set; }
}