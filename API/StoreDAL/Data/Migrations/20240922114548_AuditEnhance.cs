using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditEnhance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "AuditEntries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrailType",
                table: "AuditEntries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "AuditEntries");

            migrationBuilder.DropColumn(
                name: "TrailType",
                table: "AuditEntries");
        }
    }
}
