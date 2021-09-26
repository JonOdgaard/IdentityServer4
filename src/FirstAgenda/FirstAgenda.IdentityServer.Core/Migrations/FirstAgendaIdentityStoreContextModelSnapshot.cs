﻿// <auto-generated />
using System;
using FirstAgenda.IdentityServer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FirstAgenda.IdentityServer.Core.Migrations
{
    [DbContext(typeof(FirstAgendaIdentityStoreContext))]
    partial class FirstAgendaIdentityStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FirstAgenda.IdentityServer.Core.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedDateUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ExternalProviderName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ExternalProviderSubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastPasswordChangeDateUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LoginId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("MustUseTwoFactorAuthentication")
                        .HasColumnType("bit");

                    b.Property<bool>("PasswordExpired")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("TwoFactorAuthenticationMobilePhone")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("FirstAgenda.IdentityServer.Core.Models.AccountProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<bool>("HasProfilePicture")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePictureStorageBucket")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureStorageKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmsNotificationMobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeZoneId")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("UserFullName")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountProfile");
                });

            modelBuilder.Entity("FirstAgenda.IdentityServer.Core.Models.ExternalProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HomeRealm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetadataUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExternalProvider");
                });

            modelBuilder.Entity("FirstAgenda.IdentityServer.Core.Models.AccountProfile", b =>
                {
                    b.HasOne("FirstAgenda.IdentityServer.Core.Models.Account", "Account")
                        .WithMany("AccountProfiles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}