using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Menus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MealSize_AddIngredientsQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealSizes_MealId",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealIngredient",
                schema: "menus",
                table: "MealIngredient");

            migrationBuilder.DropIndex(
                name: "IX_MealIngredient_MealId",
                schema: "menus",
                table: "MealIngredient");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "menus",
                table: "MealIngredient");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "menus",
                table: "MealIngredient",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "foodId",
                schema: "menus",
                table: "MealIngredient",
                newName: "FoodId");

            migrationBuilder.AddColumn<bool>(
                name: "Reviewed",
                schema: "menus",
                table: "MenuMeals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealIngredient",
                schema: "menus",
                table: "MealIngredient",
                columns: new[] { "MealId", "FoodId" });

            migrationBuilder.CreateTable(
                name: "IngredientQuantities",
                schema: "menus",
                columns: table => new
                {
                    MealSizeId = table.Column<Guid>(type: "uuid", nullable: false),
                    MealIngredientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientQuantities", x => new { x.MealSizeId, x.MealIngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientQuantities_MealSizes_MealSizeId",
                        column: x => x.MealSizeId,
                        principalSchema: "menus",
                        principalTable: "MealSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealSizes_MealId_Name",
                schema: "menus",
                table: "MealSizes",
                columns: new[] { "MealId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealSizes_MealId_SortOrder",
                schema: "menus",
                table: "MealSizes",
                columns: new[] { "MealId", "SortOrder" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientQuantities",
                schema: "menus");

            migrationBuilder.DropIndex(
                name: "IX_MealSizes_MealId_Name",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropIndex(
                name: "IX_MealSizes_MealId_SortOrder",
                schema: "menus",
                table: "MealSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealIngredient",
                schema: "menus",
                table: "MealIngredient");

            migrationBuilder.DropColumn(
                name: "Reviewed",
                schema: "menus",
                table: "MenuMeals");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "menus",
                table: "MealIngredient",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                schema: "menus",
                table: "MealIngredient",
                newName: "foodId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "menus",
                table: "MealIngredient",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealIngredient",
                schema: "menus",
                table: "MealIngredient",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MealSizes_MealId",
                schema: "menus",
                table: "MealSizes",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredient_MealId",
                schema: "menus",
                table: "MealIngredient",
                column: "MealId");
        }
    }
}
