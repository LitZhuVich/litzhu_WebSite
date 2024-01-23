using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_gender_UserDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessFails_Users_UserId",
                table: "UserAccessFails");

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessFails_Users_UserId",
                table: "UserAccessFails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccessFails_Users_UserId",
                table: "UserAccessFails");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccessFails_Users_UserId",
                table: "UserAccessFails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
