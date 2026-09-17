using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                schema: "BackendCommerceApp",
                table: "order",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_user_id",
                schema: "BackendCommerceApp",
                table: "order",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_order_user_id",
                schema: "BackendCommerceApp",
                table: "order");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "BackendCommerceApp",
                table: "order");
        }
    }
}
