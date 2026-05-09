using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Customer_RemoveUserRelatedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                schema: "customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Fname",
                schema: "customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Lname",
                schema: "customers",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "customers",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                schema: "customers",
                table: "Customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lname",
                schema: "customers",
                table: "Customers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                schema: "customers",
                table: "Customers",
                column: "Email",
                unique: true);
        }
    }
}
