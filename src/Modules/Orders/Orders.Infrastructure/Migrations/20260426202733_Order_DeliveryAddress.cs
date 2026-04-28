using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Order_DeliveryAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddressId",
                schema: "orders",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_Area",
                schema: "orders",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_City",
                schema: "orders",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "DeliveryAddress_Coordinates_Latitude",
                schema: "orders",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DeliveryAddress_Coordinates_Longitude",
                schema: "orders",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_StreetName",
                schema: "orders",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_StreetNumber",
                schema: "orders",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Area",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_City",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Coordinates_Latitude",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_Coordinates_Longitude",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_StreetName",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_StreetNumber",
                schema: "orders",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryAddressId",
                schema: "orders",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
