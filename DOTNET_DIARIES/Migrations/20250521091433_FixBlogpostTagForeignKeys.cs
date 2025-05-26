using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNET_DIARIES.Migrations
{
    /// <inheritdoc />
    public partial class FixBlogpostTagForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogpostTags_Tags_BlogpostId",
                table: "BlogpostTags");

            migrationBuilder.CreateIndex(
                name: "IX_BlogpostTags_TagId",
                table: "BlogpostTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogpostTags_Tags_TagId",
                table: "BlogpostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogpostTags_Tags_TagId",
                table: "BlogpostTags");

            migrationBuilder.DropIndex(
                name: "IX_BlogpostTags_TagId",
                table: "BlogpostTags");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogpostTags_Tags_BlogpostId",
                table: "BlogpostTags",
                column: "BlogpostId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
