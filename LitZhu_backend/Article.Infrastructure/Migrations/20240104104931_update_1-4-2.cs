using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_142 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Articles_Title",
                table: "Articles",
                newName: "IX_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_IsDeleted",
                table: "Articles",
                newName: "IX_IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Title",
                table: "Articles",
                newName: "IX_Articles_Title");

            migrationBuilder.RenameIndex(
                name: "IX_IsDeleted",
                table: "Articles",
                newName: "IX_Articles_IsDeleted");
        }
    }
}
