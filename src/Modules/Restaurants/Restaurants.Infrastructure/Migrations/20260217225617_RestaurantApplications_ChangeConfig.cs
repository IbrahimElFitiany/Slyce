using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantApplications_ChangeConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RestaurantApplications_MobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications",
                newName: "OwnerMobileNumber");

            migrationBuilder.RenameColumn(
                name: "Branches",
                schema: "restaurants",
                table: "RestaurantApplications",
                newName: "BranchCount");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyMobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                schema: "restaurants",
                table: "RestaurantApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantApplications_BrandName",
                schema: "restaurants",
                table: "RestaurantApplications",
                column: "BrandName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RestaurantApplications_BrandName",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "Area",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "CompanyMobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "StreetName",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                schema: "restaurants",
                table: "RestaurantApplications");

            migrationBuilder.RenameColumn(
                name: "OwnerMobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications",
                newName: "MobileNumber");

            migrationBuilder.RenameColumn(
                name: "BranchCount",
                schema: "restaurants",
                table: "RestaurantApplications",
                newName: "Branches");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantApplications_MobileNumber",
                schema: "restaurants",
                table: "RestaurantApplications",
                column: "MobileNumber",
                unique: true);
        }
    }
}
