using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Subscriptiosn_AddAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Area",
                schema: "Subscriptions",
                table: "Subscriptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_City",
                schema: "Subscriptions",
                table: "Subscriptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_StreetName",
                schema: "Subscriptions",
                table: "Subscriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_StreetNumber",
                schema: "Subscriptions",
                table: "Subscriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "Subscriptions",
                table: "Subscriptions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "Subscriptions",
                table: "Subscriptions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Area",
                schema: "Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_City",
                schema: "Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_StreetName",
                schema: "Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_StreetNumber",
                schema: "Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "Subscriptions",
                table: "Subscriptions");
        }
    }
}
