using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Menus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditMealAndSizeConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealSizes_MenuMeals_MenuMealId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuMeals_MenuCategories_CategoryId",
                schema: "menus",
                table: "MenuMeals");

            migrationBuilder.RenameColumn(
                name: "MenuMealId",
                schema: "menus",
                table: "MealSizes",
                newName: "MealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealSizes_MenuMealId",
                schema: "menus",
                table: "MealSizes",
                newName: "IX_MealSizes_MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealSizes_MenuMeals_MealId",
                schema: "menus",
                table: "MealSizes",
                column: "MealId",
                principalSchema: "menus",
                principalTable: "MenuMeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuMeals_MenuCategories_CategoryId",
                schema: "menus",
                table: "MenuMeals",
                column: "CategoryId",
                principalSchema: "menus",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealSizes_MenuMeals_MealId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuMeals_MenuCategories_CategoryId",
                schema: "menus",
                table: "MenuMeals");

            migrationBuilder.RenameColumn(
                name: "MealId",
                schema: "menus",
                table: "MealSizes",
                newName: "MenuMealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealSizes_MealId",
                schema: "menus",
                table: "MealSizes",
                newName: "IX_MealSizes_MenuMealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealSizes_MenuMeals_MenuMealId",
                schema: "menus",
                table: "MealSizes",
                column: "MenuMealId",
                principalSchema: "menus",
                principalTable: "MenuMeals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuMeals_MenuCategories_CategoryId",
                schema: "menus",
                table: "MenuMeals",
                column: "CategoryId",
                principalSchema: "menus",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
