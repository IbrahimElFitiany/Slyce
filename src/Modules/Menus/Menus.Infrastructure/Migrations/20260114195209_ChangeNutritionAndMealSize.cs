using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Menus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNutritionAndMealSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbGrams",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "FatGrams",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "ProteinGrams",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.AlterColumn<decimal>(
                name: "SugarGrams",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "SodiumMg",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "CalciumMg",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cholesterol",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DietaryFiber",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IronMg",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PotassiumMg",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Protein",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaturatedFat",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCarbohydrate",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalFat",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransFat",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VitaminAMcg",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VitaminCMg",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VitaminD",
                schema: "menus",
                table: "MealSizes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalciumMg",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "Cholesterol",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "DietaryFiber",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "IronMg",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "PotassiumMg",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "Protein",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "SaturatedFat",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "TotalCarbohydrate",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "TotalFat",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "TransFat",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "VitaminAMcg",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "VitaminCMg",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropColumn(
                name: "VitaminD",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.AlterColumn<int>(
                name: "SugarGrams",
                schema: "menus",
                table: "MealSizes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "SodiumMg",
                schema: "menus",
                table: "MealSizes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                schema: "menus",
                table: "MealSizes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "CarbGrams",
                schema: "menus",
                table: "MealSizes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatGrams",
                schema: "menus",
                table: "MealSizes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProteinGrams",
                schema: "menus",
                table: "MealSizes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
