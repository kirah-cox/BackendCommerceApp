using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductTableAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "BackendCommerceApp",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "BackendCommerceApp",
                newName: "product",
                newSchema: "BackendCommerceApp");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "BackendCommerceApp",
                table: "product",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "BackendCommerceApp",
                table: "product",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "BackendCommerceApp",
                table: "product",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Category",
                schema: "BackendCommerceApp",
                table: "product",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "BackendCommerceApp",
                table: "product",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                schema: "BackendCommerceApp",
                table: "product",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                schema: "BackendCommerceApp",
                table: "product");

            migrationBuilder.RenameTable(
                name: "product",
                schema: "BackendCommerceApp",
                newName: "Products",
                newSchema: "BackendCommerceApp");

            migrationBuilder.RenameColumn(
                name: "price",
                schema: "BackendCommerceApp",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "BackendCommerceApp",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "BackendCommerceApp",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "category",
                schema: "BackendCommerceApp",
                table: "Products",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "BackendCommerceApp",
                table: "Products",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "BackendCommerceApp",
                table: "Products",
                column: "Id");
        }
    }
}
