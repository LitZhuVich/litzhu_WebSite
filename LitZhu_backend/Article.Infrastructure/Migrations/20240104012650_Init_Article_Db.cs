using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_Article_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Articles",
                 columns: table => new
                 {
                     Id = table.Column<Guid>(type: "char(36)", nullable: false),
                     Title = table.Column<string>(type: "LONGTEXT", nullable: false),
                     Content = table.Column<string>(type: "LONGTEXT", nullable: false),
                     IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                     CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                     DeletionTime = table.Column<DateTime>(type: "datetime", nullable: true),
                     LastModificationTime = table.Column<DateTime>(type: "datetime", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Articles", x => x.Id);
                 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
