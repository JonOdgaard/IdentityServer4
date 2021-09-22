using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    public partial class Initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountProfile_Account_AccountId1",
                table: "AccountProfile");

            migrationBuilder.DropIndex(
                name: "IX_AccountProfile_AccountId1",
                table: "AccountProfile");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "AccountProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "AccountProfile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfile_AccountId1",
                table: "AccountProfile",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountProfile_Account_AccountId1",
                table: "AccountProfile",
                column: "AccountId1",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
