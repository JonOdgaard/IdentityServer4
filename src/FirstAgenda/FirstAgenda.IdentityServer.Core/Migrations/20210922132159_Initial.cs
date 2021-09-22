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
                    LoginId = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    ExternalProviderName = table.Column<string>(maxLength: 200, nullable: true),
                    ExternalProviderSubjectId = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Salt = table.Column<string>(maxLength: 100, nullable: true),
                    LastPasswordChangeDateUtc = table.Column<DateTimeOffset>(nullable: true),
                    CreatedDateUtc = table.Column<DateTimeOffset>(nullable: false),
                    Uid = table.Column<Guid>(nullable: false),
                    PasswordExpired = table.Column<bool>(nullable: false),
                    FailedLoginAttempts = table.Column<int>(nullable: false),
                    MustUseTwoFactorAuthentication = table.Column<bool>(nullable: false),
                    TwoFactorAuthenticationMobilePhone = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalProvider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProviderId = table.Column<string>(nullable: true),
                    HomeRealm = table.Column<string>(nullable: true),
                    MetadataUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFullName = table.Column<string>(maxLength: 300, nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    TimeZoneId = table.Column<string>(maxLength: 10, nullable: true),
                    SmsNotificationMobileNumber = table.Column<string>(nullable: true),
                    HasProfilePicture = table.Column<bool>(nullable: false),
                    ProfilePictureStorageBucket = table.Column<string>(nullable: true),
                    ProfilePictureStorageKey = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountProfile_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfile_AccountId",
                table: "AccountProfile",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountProfile");

            migrationBuilder.DropTable(
                name: "ExternalProvider");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
