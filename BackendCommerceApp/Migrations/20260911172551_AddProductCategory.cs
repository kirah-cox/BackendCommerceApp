using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"BackendCommerceApp\".\"Products\";");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "BackendCommerceApp",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "BackendCommerceApp",
                table: "Products");
        }
    }
}
