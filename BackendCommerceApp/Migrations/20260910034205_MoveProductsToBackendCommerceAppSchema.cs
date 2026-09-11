using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class MoveProductsToBackendCommerceAppSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BackendCommerceApp");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "BackendCommerceApp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                schema: "BackendCommerceApp",
                newName: "Products");
        }
    }
}
