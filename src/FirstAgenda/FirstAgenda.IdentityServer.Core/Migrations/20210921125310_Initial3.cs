using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureStorageBucket",
                table: "AccountProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureStorageKey",
                table: "AccountProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmsNotificationMobileNumber",
                table: "AccountProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "AccountProfile",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoFactorAuthenticationMobilePhone",
                table: "Account",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureStorageBucket",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "ProfilePictureStorageKey",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "SmsNotificationMobileNumber",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "TwoFactorAuthenticationMobilePhone",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "Account",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }
    }
}
