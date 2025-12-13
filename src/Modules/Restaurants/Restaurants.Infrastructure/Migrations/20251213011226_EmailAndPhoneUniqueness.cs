using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmailAndPhoneUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RestaurantApplications_CompanyEmail",
                schema: "restaurants",
                table: "RestaurantApplications",
                column: "CompanyEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantApplications_MobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications",
                column: "MobileNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RestaurantApplications_CompanyEmail",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantApplications_MobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications");
        }
    }
}
