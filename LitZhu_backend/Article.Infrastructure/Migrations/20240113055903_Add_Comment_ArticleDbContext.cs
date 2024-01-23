using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.Infrastructure.Migrations
{
    public partial class Add_Comment_ArticleDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
              name: "Title",
              table: "Articles",
              type: "nvarchar(100)",
              maxLength: 100,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(50)",
              oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
              name: "Likes",
              table: "Articles",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
              name: "UserId",
              table: "Articles",
              type: "uniqueidentifier",
              nullable: false,
              defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
              name: "Views",
              table: "Articles",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: "");

            migrationBuilder.CreateTable(
              name: "Comments",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                  Likes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  PId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                  Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                  LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Comments", x => x.Id);
                  table.ForeignKey(
              name: "FK_Comments_Articles_ArticleId",
              column: x => x.ArticleId,
              principalTable: "Articles",
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade);
              });

            migrationBuilder.CreateIndex(
              name: "IX_Comments_ArticleId",
              table: "Comments",
              column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Comments");

            migrationBuilder.DropColumn(
              name: "Likes",
              table: "Articles");

            migrationBuilder.DropColumn(
              name: "UserId",
              table: "Articles");

            migrationBuilder.DropColumn(
              name: "Views",
              table: "Articles");

            migrationBuilder.AlterColumn<string>(
              name: "Title",
              table: "Articles",
              type: "nvarchar(50)",
              maxLength: 50,
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(100)",
              oldMaxLength: 100);
        }
    }
}