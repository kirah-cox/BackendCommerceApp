namespace BackendCommerceApp.Data;

public record CatalogCategory(string Name, string Slug, string Icon);

public record CatalogProduct(int Id, string Name, string CategorySlug, decimal Price, string Icon, string Description);

public static class ProductCatalog
{
    public static IReadOnlyList<CatalogCategory> Categories { get; } = new List<CatalogCategory>
    {
        new("Electronics", "electronics", "💻"),
        new("Clothing", "clothing", "👕"),
        new("Home & Garden", "home-garden", "🏡"),
        new("Sports & Outdoors", "sports-outdoors", "⚽"),
        new("Toys & Games", "toys-games", "🧸"),
        new("Books", "books", "📚"),
    };

    public static IReadOnlyList<CatalogProduct> Products { get; } = new List<CatalogProduct>
    {
        new(1, "Wireless Headphones", "electronics", 79.99m, "🎧", "Over-ear wireless headphones with noise cancellation and a 30-hour battery life."),
        new(2, "Smart Watch", "electronics", 129.99m, "⌚", "Track your fitness and notifications with this sleek smart watch."),
        new(3, "Running Shoes", "clothing", 59.99m, "👟", "Lightweight running shoes designed for comfort and speed."),
        new(4, "Denim Jacket", "clothing", 49.99m, "🧥", "A classic denim jacket that pairs well with any outfit."),
        new(5, "Coffee Maker", "home-garden", 39.99m, "☕", "Brew the perfect cup every morning with this compact coffee maker."),
        new(6, "Garden Tool Set", "home-garden", 34.99m, "🪴", "A complete set of tools for tending to your garden."),
        new(7, "Yoga Mat", "sports-outdoors", 24.99m, "🧘", "A non-slip yoga mat perfect for home workouts."),
        new(8, "Camping Tent", "sports-outdoors", 149.99m, "⛺", "A spacious 4-person tent for your next camping trip."),
        new(9, "Building Blocks Set", "toys-games", 29.99m, "🧱", "A creative building blocks set for hours of imaginative play."),
        new(10, "Board Game Bundle", "toys-games", 44.99m, "🎲", "A bundle of classic board games for family game night."),
        new(11, "Mystery Novel", "books", 14.99m, "📖", "A gripping mystery novel that will keep you guessing until the end."),
        new(12, "Cookbook", "books", 19.99m, "🍳", "Delicious recipes for every occasion, from breakfast to dinner."),
    };

    public static int ProductCountForCategory(string slug) => Products.Count(p => p.CategorySlug == slug);

    public static CatalogProduct? GetProduct(int id) => Products.FirstOrDefault(p => p.Id == id);

    public static CatalogCategory? GetCategory(string slug) => Categories.FirstOrDefault(c => c.Slug == slug);
}
