using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Food_addExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                schema: "Food",
                table: "Foods",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "Food",
                table: "Foods");
        }
    }
}
