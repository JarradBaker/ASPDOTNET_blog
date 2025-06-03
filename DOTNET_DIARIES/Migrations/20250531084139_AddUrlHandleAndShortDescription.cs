using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_DIARIES.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlHandleAndShortDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Blogposts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Blogposts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Blogposts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ShortDescription", "UrlHandle" },
                values: new object[] { "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Blogposts");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Blogposts");
        }
    }
}
