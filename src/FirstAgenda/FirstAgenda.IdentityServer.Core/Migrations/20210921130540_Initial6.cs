using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustChangedPassword",
                table: "Account");

            migrationBuilder.AddColumn<bool>(
                name: "MustUseTwoFactorAuthentication",
                table: "Account",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PasswordExpired",
                table: "Account",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustUseTwoFactorAuthentication",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PasswordExpired",
                table: "Account");

            migrationBuilder.AddColumn<bool>(
                name: "MustChangedPassword",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
