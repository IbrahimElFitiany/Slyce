using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitFoodDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Food");

            migrationBuilder.CreateTable(
                name: "Foods",
                schema: "Food",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Source = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foods",
                schema: "Food");
        }
    }
}
