using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddContinentToCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Continent",
                table: "Countries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Continent",
                table: "Countries");
        }
    }
}
