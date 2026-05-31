using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Orders_branchId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                schema: "orders",
                table: "Orders",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                schema: "orders",
                table: "Carts",
                newName: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BranchId",
                schema: "orders",
                table: "Orders",
                newName: "RestaurantId");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                schema: "orders",
                table: "Carts",
                newName: "RestaurantId");
        }
    }
}
