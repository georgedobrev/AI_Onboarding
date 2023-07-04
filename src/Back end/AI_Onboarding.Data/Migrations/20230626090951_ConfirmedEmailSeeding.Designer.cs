﻿// <auto-generated />
using System;
using AI_Onboarding.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AI_Onboarding.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230626090951_ConfirmedEmailSeeding")]
    partial class ConfirmedEmailSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AI_Onboarding.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6020),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6030),
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        });
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ModifiedById");

                    b.HasIndex("NormalizedEmail")
                        .IsUnique()
                        .HasDatabaseName("EmailIndex")
                        .HasFilter("[NormalizedEmail] IS NOT NULL");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "db322c9d-4fc5-4b24-a556-9ff9782cebc8",
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6100),
                            Email = "admin1@admin.com",
                            EmailConfirmed = true,
                            FirstName = "Admin1",
                            LastName = "Admin1",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN1@ADMIN.COM",
                            NormalizedUserName = "ADMIN1@ADMIN.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEK7Spx3gg8lNkg1VfPtjcdnZtCRTcq1NXNPnDmI/iOGmr1xOjebVn0oEA9hD3wZu/w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e30a8931-bda3-4825-93f7-90651a298379",
                            TwoFactorEnabled = false,
                            UserName = "admin1@admin.com"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0d308e54-3037-45d6-9da5-2acb7f07e7c9",
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6130),
                            Email = "admin2@admin.com",
                            EmailConfirmed = true,
                            FirstName = "Admin2",
                            LastName = "Admin2",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN2@ADMIN.COM",
                            NormalizedUserName = "ADMIN2@ADMIN.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEEBGTFVnMf4np76jvoSRVWVnNYSjmmgUPsC4mI8x7ByNaZX8ctVYxIna71TaeNEwiw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3ad6ddbe-52f4-4a7a-a21c-bb2f646f5321",
                            TwoFactorEnabled = false,
                            UserName = "admin2@admin.com"
                        },
                        new
                        {
                            Id = 3,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "846f1eca-fd9f-4da3-9cac-1034a5f2853a",
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6140),
                            Email = "user@mail.com",
                            EmailConfirmed = true,
                            FirstName = "User",
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@MAIL.COM",
                            NormalizedUserName = "USER@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGErH06Mw6zX11AB++A8X6ukFL36FPHnJXzLfEO7N5+i2QYDWqXQK3H1vxmWvmJDZw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6c88fcda-539e-4ee4-9bca-b2bf9f0c7559",
                            TwoFactorEnabled = false,
                            UserName = "user@mail.com"
                        });
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1,
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 510, DateTimeKind.Utc).AddTicks(3160)
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 1,
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 510, DateTimeKind.Utc).AddTicks(3160)
                        },
                        new
                        {
                            UserId = 3,
                            RoleId = 2,
                            CreatedAt = new DateTime(2023, 6, 26, 9, 9, 51, 510, DateTimeKind.Utc).AddTicks(3170)
                        });
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.HasIndex("ModifiedById");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.Role", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedRoles")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.RoleClaim", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedRoleClaims")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AI_Onboarding.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.User", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedUsers")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AI_Onboarding.Data.Models.Role", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserClaim", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedUserClaims")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AI_Onboarding.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserLogin", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedUserLogins")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AI_Onboarding.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserRole", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedUserRoles")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AI_Onboarding.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AI_Onboarding.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.UserToken", b =>
                {
                    b.HasOne("AI_Onboarding.Data.Models.User", "ModifiedBy")
                        .WithMany("ModifiedUserTokens")
                        .HasForeignKey("ModifiedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AI_Onboarding.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AI_Onboarding.Data.Models.User", b =>
                {
                    b.Navigation("ModifiedRoleClaims");

                    b.Navigation("ModifiedRoles");

                    b.Navigation("ModifiedUserClaims");

                    b.Navigation("ModifiedUserLogins");

                    b.Navigation("ModifiedUserRoles");

                    b.Navigation("ModifiedUserTokens");

                    b.Navigation("ModifiedUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
