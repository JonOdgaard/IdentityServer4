using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ExternalProviderName = table.Column<string>(maxLength: 200, nullable: true),
                    ExternalProviderSubjectId = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Salt = table.Column<string>(maxLength: 100, nullable: true),
                    LastPasswordChangeDateUtc = table.Column<DateTimeOffset>(nullable: false),
                    CreatedDateUtc = table.Column<DateTimeOffset>(nullable: false),
                    Uid = table.Column<Guid>(nullable: false),
                    MustChangedPassword = table.Column<bool>(nullable: false),
                    FailedLoginAttempts = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    TimeZoneId = table.Column<string>(maxLength: 10, nullable: true),
                    HasProfilePicture = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
