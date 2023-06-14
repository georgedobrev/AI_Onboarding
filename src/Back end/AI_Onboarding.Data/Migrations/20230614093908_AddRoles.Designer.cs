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
    [Migration("20230614093908_AddRoles")]
    partial class AddRoles
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
                            CreatedAt = new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4820),
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4820),
                            Name = "Employee"
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

                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

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
                            ConcurrencyStamp = "cde86daa-eb08-4527-a5a8-11e14e007c31",
                            CreatedAt = new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4920),
                            Email = "admin1@admin.com",
                            EmailConfirmed = false,
                            FirstName = "Admin1",
                            LastName = "Admin1",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN1@ADMIN.COM",
                            NormalizedUserName = "ADMIN1@ADMIN.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEI45hAu44gSjw+pd1w5Gn7wAmUqeUq/uveHh6C/+IpE0buN2WijVNOC6aCtoIGhvsQ==",
                            PhoneNumberConfirmed = false,
                            RoleId = 1,
                            SecurityStamp = "fa5b5723-3d77-467d-b1c7-f975cdd64cc0",
                            TwoFactorEnabled = false,
                            UserName = "admin1@admin.com"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c364e402-28c6-4df5-9a71-b8d375a5e156",
                            CreatedAt = new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4950),
                            Email = "admin2@admin.com",
                            EmailConfirmed = false,
                            FirstName = "Admin2",
                            LastName = "Admin2",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN2@ADMIN.COM",
                            NormalizedUserName = "ADMIN2@ADMIN.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDSaqgQ76WtLLIvYtpKzKFbKj0+m7vvGn68V0wxWF5tOTiI8Owh2kYXbnxk7aA5hog==",
                            PhoneNumberConfirmed = false,
                            RoleId = 1,
                            SecurityStamp = "00ae6971-4d56-4fb0-a9e6-bcc0e93cb592",
                            TwoFactorEnabled = false,
                            UserName = "admin2@admin.com"
                        },
                        new
                        {
                            Id = 3,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5ea48d12-5b71-4efe-a583-4bd35719e0e0",
                            CreatedAt = new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4960),
                            Email = "user@mail.com",
                            EmailConfirmed = false,
                            FirstName = "User",
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@MAIL.COM",
                            NormalizedUserName = "USER@MAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAECMTcSnXKKT/JlFJLyrsutwfUCf4loFchWOsEGOwWXje4jK91ZmYEdLazbuQ8qUV6w==",
                            PhoneNumberConfirmed = false,
                            RoleId = 2,
                            SecurityStamp = "99f023a9-316f-4afc-928e-b6a1793e677b",
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

                    b.HasOne("AI_Onboarding.Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ModifiedBy");

                    b.Navigation("Role");
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
