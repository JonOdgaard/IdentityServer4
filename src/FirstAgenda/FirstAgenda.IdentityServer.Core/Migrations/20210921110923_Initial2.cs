using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasProfilePicture",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "LoginId",
                table: "Account",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "Account",
                maxLength: 300,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(nullable: false),
                    TimeZoneId = table.Column<string>(maxLength: 10, nullable: true),
                    HasProfilePicture = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ProfileId",
                table: "Account",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AccountProfile_ProfileId",
                table: "Account",
                column: "ProfileId",
                principalTable: "AccountProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountProfile_ProfileId",
                table: "Account");

            migrationBuilder.DropTable(
                name: "AccountProfile");

            migrationBuilder.DropIndex(
                name: "IX_Account_ProfileId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "Account");

            migrationBuilder.AddColumn<bool>(
                name: "HasProfilePicture",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                table: "Account",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Account",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
