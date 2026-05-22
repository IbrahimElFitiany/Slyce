using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Restaurant_addBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                schema: "restaurants",
                table: "Restaurants",
                newName: "Logo");

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                schema: "restaurants",
                table: "Restaurants",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banner",
                schema: "restaurants",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "Logo",
                schema: "restaurants",
                table: "Restaurants",
                newName: "Image");
        }
    }
}
