using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Orders_subsStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "DeliveryTimeFrame_From",
                schema: "orders",
                table: "Orders",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "DeliveryTimeFrame_To",
                schema: "orders",
                table: "Orders",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                schema: "orders",
                table: "Orders",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryTimeFrame_From",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeFrame_To",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                schema: "orders",
                table: "Orders");
        }
    }
}
