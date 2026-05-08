using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Customers_RemovePhoneConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_PhoneNumber",
                schema: "customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "customers",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "customers",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                schema: "customers",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
