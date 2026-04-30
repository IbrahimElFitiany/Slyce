using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Restaurant_Application_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "restaurants",
                table: "Restaurants",
                newName: "BrandName");

            migrationBuilder.CreateTable(
                name: "RestaurantApplications",
                schema: "restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OwnerLastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CompanyEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MobileNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RestaurantType = table.Column<string>(type: "text", nullable: false),
                    Branches = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    RejectionReason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantApplications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantApplications",
                schema: "restaurants");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                schema: "restaurants",
                table: "Restaurants",
                newName: "Name");
        }
    }
}
