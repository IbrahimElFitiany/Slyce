using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Subscription_initConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeFrame_From = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    TimeFrame_To = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    BillingCycle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TotalPrice_Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice_Currency = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.CheckConstraint("CK_DeliveryTime", "\"EndDate\" > \"StartDate\"");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDeliveryDays",
                schema: "Subscriptions",
                columns: table => new
                {
                    Day = table.Column<string>(type: "text", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDeliveryDays", x => new { x.Day, x.SubscriptionId });
                    table.ForeignKey(
                        name: "FK_SubscriptionDeliveryDays_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "Subscriptions",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionMeals",
                schema: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MealId = table.Column<Guid>(type: "uuid", nullable: false),
                    SizeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    PriceAtSubscription_Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceAtSubscription_Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMeals", x => new { x.MealId, x.SizeId, x.SubscriptionId });
                    table.ForeignKey(
                        name: "FK_SubscriptionMeals_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "Subscriptions",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDeliveryDays_SubscriptionId",
                schema: "Subscriptions",
                table: "SubscriptionDeliveryDays",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionMeals_SubscriptionId",
                schema: "Subscriptions",
                table: "SubscriptionMeals",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_BranchId",
                schema: "Subscriptions",
                table: "Subscriptions",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CustomerId",
                schema: "Subscriptions",
                table: "Subscriptions",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionDeliveryDays",
                schema: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionMeals",
                schema: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "Subscriptions");
        }
    }
}
