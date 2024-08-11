using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using StoreDAL.Entities;

#nullable disable
namespace StoreDAL.Data.Migrations
{
    public partial class FixImageNaming : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainImgaeURL",
                table: "Countries",
                newName: "MainImageURL");

            InsertRolesData(migrationBuilder);
        }
        private void InsertRolesData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Name" },
                values: new object[] { "Viewer" }
            );
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainImageURL",
                table: "Countries",
                newName: "MainImgaeURL");
        }
    }
}
