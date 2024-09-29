using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditEnhance2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "AuditEntries",
                newName: "StartTimeUTC");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "AuditEntries",
                newName: "EndTimeUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTimeUTC",
                table: "AuditEntries",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndTimeUTC",
                table: "AuditEntries",
                newName: "EndTime");
        }
    }
}
