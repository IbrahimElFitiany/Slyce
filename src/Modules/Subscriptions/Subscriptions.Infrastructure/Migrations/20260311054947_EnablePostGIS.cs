using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnablePostGIS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS postgis;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP EXTENSION IF EXISTS postgis;");
        }
    }
}
