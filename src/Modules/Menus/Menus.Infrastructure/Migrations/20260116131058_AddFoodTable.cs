using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Menus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                schema: "menus",
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
                    Dietary_Fiber = table.Column<decimal>(type: "numeric", nullable: false),
                    IronMg = table.Column<decimal>(type: "numeric", nullable: false),
                    PotassiumMg = table.Column<decimal>(type: "numeric", nullable: false),
                    Protein = table.Column<decimal>(type: "numeric", nullable: false),
                    Saturated_Fat = table.Column<decimal>(type: "numeric", nullable: false),
                    SodiumMg = table.Column<decimal>(type: "numeric", nullable: false),
                    SugarGrams = table.Column<decimal>(type: "numeric", nullable: false),
                    Total_Carbohydrate = table.Column<decimal>(type: "numeric", nullable: false),
                    Total_Fat = table.Column<decimal>(type: "numeric", nullable: false),
                    Trans_Fat = table.Column<decimal>(type: "numeric", nullable: false),
                    Vitamin_A_Mcg = table.Column<decimal>(type: "numeric", nullable: false),
                    Vitamin_C_Mg = table.Column<decimal>(type: "numeric", nullable: false),
                    Vitamin_D = table.Column<decimal>(type: "numeric", nullable: false)
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
                schema: "menus");
        }
    }
}
