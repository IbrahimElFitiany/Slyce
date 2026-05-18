using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerAllergensAndDietPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<Guid>>(
                name: "allergen_ids",
                schema: "customers",
                table: "Customers",
                type: "uuid[]",
                nullable: false,
                defaultValue: new List<Guid>());

            migrationBuilder.AddColumn<List<Guid>>(
                name: "diet_preference_ids",
                schema: "customers",
                table: "Customers",
                type: "uuid[]",
                nullable: false,
                defaultValue: new List<Guid>());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "allergen_ids",
                schema: "customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "diet_preference_ids",
                schema: "customers",
                table: "Customers");
        }
    }
}
