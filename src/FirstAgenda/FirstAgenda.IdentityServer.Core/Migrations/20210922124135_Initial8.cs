using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountProfile_ProfileId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ProfileId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "AccountProfile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "AccountProfile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfile_AccountId",
                table: "AccountProfile",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfile_AccountId1",
                table: "AccountProfile",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountProfile_Account_AccountId",
                table: "AccountProfile",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountProfile_Account_AccountId1",
                table: "AccountProfile",
                column: "AccountId1",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountProfile_Account_AccountId",
                table: "AccountProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountProfile_Account_AccountId1",
                table: "AccountProfile");

            migrationBuilder.DropIndex(
                name: "IX_AccountProfile_AccountId",
                table: "AccountProfile");

            migrationBuilder.DropIndex(
                name: "IX_AccountProfile_AccountId1",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "AccountProfile");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Account",
                type: "int",
                nullable: true);

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
    }
}
