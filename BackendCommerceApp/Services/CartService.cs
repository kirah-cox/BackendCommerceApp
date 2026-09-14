using BackendCommerceApp.Models;

namespace BackendCommerceApp.Services;

public class CartItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public decimal TotalPrice => Product.Price * Quantity;
}

public class CartService
{
    private readonly List<CartItem> _items = new();

    public event Action? CartChanged;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public int TotalItems => _items.Sum(item => item.Quantity);

    public decimal Subtotal => _items.Sum(item => item.TotalPrice);

    public void AddToCart(Product product, int quantity = 1)
    {
        if (product is null || quantity <= 0) return;

        var existingItem = _items.FirstOrDefault(item => item.Product.Id == product.Id);
        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem { Product = product, Quantity = quantity });
        }

        NotifyCartChanged();
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item is not null)
        {
            if (quantity <= 0)
            {
                _items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            NotifyCartChanged();
        }
    }

    public void RemoveFromCart(int productId)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item is not null)
        {
            _items.Remove(item);
            NotifyCartChanged();
        }
    }

    public void ClearCart()
    {
        _items.Clear();
        NotifyCartChanged();
    }

    private void NotifyCartChanged() => CartChanged?.Invoke();
}
