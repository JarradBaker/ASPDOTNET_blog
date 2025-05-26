using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_DIARIES.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Blogposts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostedDate",
                value: new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Blogposts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostedDate",
                value: new DateTime(2025, 5, 14, 17, 49, 38, 151, DateTimeKind.Local).AddTicks(8543));
        }
    }
}
