using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class AssignProductCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE "BackendCommerceApp"."product"
                SET "category" = CASE "name"
                    WHEN 'Wireless Headphones' THEN 'electronics'
                    WHEN 'Smart Watch' THEN 'electronics'
                    WHEN 'Running Shoes' THEN 'clothing'
                    WHEN 'Denim Jacket' THEN 'clothing'
                    WHEN 'Coffee Maker' THEN 'home-garden'
                    WHEN 'Garden Tool Set' THEN 'home-garden'
                    WHEN 'Yoga Mat' THEN 'sports-outdoors'
                    WHEN 'Camping Tent' THEN 'sports-outdoors'
                    WHEN 'Building Blocks Set' THEN 'toys-games'
                    WHEN 'Board Game Bundle' THEN 'toys-games'
                    WHEN 'Mystery Novel' THEN 'books'
                    WHEN 'Cookbook' THEN 'books'
                END
                WHERE ("category" IS NULL OR "category" = '')
                  AND "name" IN (
                      'Wireless Headphones', 'Smart Watch', 'Running Shoes',
                      'Denim Jacket', 'Coffee Maker', 'Garden Tool Set',
                      'Yoga Mat', 'Camping Tent', 'Building Blocks Set',
                      'Board Game Bundle', 'Mystery Novel', 'Cookbook'
                  );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE "BackendCommerceApp"."product"
                SET "category" = ''
                WHERE "name" IN (
                    'Wireless Headphones', 'Smart Watch', 'Running Shoes',
                    'Denim Jacket', 'Coffee Maker', 'Garden Tool Set',
                    'Yoga Mat', 'Camping Tent', 'Building Blocks Set',
                    'Board Game Bundle', 'Mystery Novel', 'Cookbook'
                );
                """);
        }
    }
}