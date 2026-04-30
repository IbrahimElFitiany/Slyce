using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Menus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFoodFromMenuModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealSizes_MenuMeals_MenuItemId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropTable(
                name: "Foods",
                schema: "menus");

            migrationBuilder.DropIndex(
                name: "IX_MealSizes_MenuItemId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuMealId",
                schema: "menus",
                table: "MealSizes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealSizes_MenuMealId",
                schema: "menus",
                table: "MealSizes",
                column: "MenuMealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealSizes_MenuMeals_MenuMealId",
                schema: "menus",
                table: "MealSizes",
                column: "MenuMealId",
                principalSchema: "menus",
                principalTable: "MenuMeals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealSizes_MenuMeals_MenuMealId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropIndex(
                name: "IX_MealSizes_MenuMealId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "MenuMealId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuItemId",
                schema: "menus",
                table: "MealSizes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Foods",
                schema: "menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Source = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CalciumMg = table.Column<decimal>(type: "numeric", nullable: false),
                    Calories = table.Column<decimal>(type: "numeric", nullable: false),
                    Cholesterol = table.Column<decimal>(type: "numeric", nullable: false),
                    DietaryFiber = table.Column<decimal>(type: "numeric", nullable: false),
                    IronMg = table.Column<decimal>(type: "numeric", nullable: false),
                    PotassiumMg = table.Column<decimal>(type: "numeric", nullable: false),
                    Protein = table.Column<decimal>(type: "numeric", nullable: false),
                    SaturatedFat = table.Column<decimal>(type: "numeric", nullable: false),
                    SodiumMg = table.Column<decimal>(type: "numeric", nullable: false),
                    SugarGrams = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCarbohydrate = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalFat = table.Column<decimal>(type: "numeric", nullable: false),
                    TransFat = table.Column<decimal>(type: "numeric", nullable: false),
                    VitaminAMcg = table.Column<decimal>(type: "numeric", nullable: false),
                    VitaminCMg = table.Column<decimal>(type: "numeric", nullable: false),
                    VitaminD = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealSizes_MenuItemId",
                schema: "menus",
                table: "MealSizes",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealSizes_MenuMeals_MenuItemId",
                schema: "menus",
                table: "MealSizes",
                column: "MenuItemId",
                principalSchema: "menus",
                principalTable: "MenuMeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
