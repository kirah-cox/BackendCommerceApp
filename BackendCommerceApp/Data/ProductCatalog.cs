namespace BackendCommerceApp.Data;

public record CatalogCategory(string Name, string Slug);

public static class ProductCatalog
{
    public static IReadOnlyList<CatalogCategory> Categories { get; } = new List<CatalogCategory>
    {
        new("Electronics", "electronics"),
        new("Clothing", "clothing"),
        new("Home & Garden", "home-garden"),
        new("Sports & Outdoors", "sports-outdoors"),
        new("Toys & Games", "toys-games"),
        new("Books", "books"),
    };

    public static CatalogCategory? GetCategory(string slug) => Categories.FirstOrDefault(c => c.Slug == slug);
}
