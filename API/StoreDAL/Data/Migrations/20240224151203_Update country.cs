using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatecountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BackroundImgaeURL",
                table: "Countries",
                newName: "SecondaryImageURL");

            migrationBuilder.AddColumn<string>(
                name: "MainImgaeURL",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImgaeURL",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "SecondaryImageURL",
                table: "Countries",
                newName: "BackroundImgaeURL");
        }
    }
}
