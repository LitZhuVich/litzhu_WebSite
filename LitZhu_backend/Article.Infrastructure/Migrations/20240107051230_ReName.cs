using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Tags_Articles_ArticleId",
                table: "Article_Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Tags_Tags_TagId",
                table: "Article_Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article_Tags",
                table: "Article_Tags");

            migrationBuilder.RenameTable(
                name: "Article_Tags",
                newName: "Articles_Tags");

            migrationBuilder.RenameIndex(
                name: "IX_Article_Tags_TagId",
                table: "Articles_Tags",
                newName: "IX_Articles_Tags_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "Tags",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles_Tags",
                table: "Articles_Tags",
                columns: new[] { "ArticleId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Tags_Articles_ArticleId",
                table: "Articles_Tags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Tags_Tags_TagId",
                table: "Articles_Tags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Tags_Articles_ArticleId",
                table: "Articles_Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Tags_Tags_TagId",
                table: "Articles_Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles_Tags",
                table: "Articles_Tags");

            migrationBuilder.RenameTable(
                name: "Articles_Tags",
                newName: "Article_Tags");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_Tags_TagId",
                table: "Article_Tags",
                newName: "IX_Article_Tags_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article_Tags",
                table: "Article_Tags",
                columns: new[] { "ArticleId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Tags_Articles_ArticleId",
                table: "Article_Tags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Tags_Tags_TagId",
                table: "Article_Tags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
