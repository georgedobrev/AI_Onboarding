using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AI_Onboarding.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "ModifiedAt", "ModifiedById", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4820), null, null, "Administrator", null },
                    { 2, null, new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4820), null, null, "Employee", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "ModifiedById", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "cde86daa-eb08-4527-a5a8-11e14e007c31", new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4920), "admin1@admin.com", false, "Admin1", "Admin1", false, null, null, null, "ADMIN1@ADMIN.COM", "ADMIN1@ADMIN.COM", "AQAAAAIAAYagAAAAEI45hAu44gSjw+pd1w5Gn7wAmUqeUq/uveHh6C/+IpE0buN2WijVNOC6aCtoIGhvsQ==", null, false, null, null, 1, "fa5b5723-3d77-467d-b1c7-f975cdd64cc0", false, "admin1@admin.com" },
                    { 2, 0, "c364e402-28c6-4df5-9a71-b8d375a5e156", new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4950), "admin2@admin.com", false, "Admin2", "Admin2", false, null, null, null, "ADMIN2@ADMIN.COM", "ADMIN2@ADMIN.COM", "AQAAAAIAAYagAAAAEDSaqgQ76WtLLIvYtpKzKFbKj0+m7vvGn68V0wxWF5tOTiI8Owh2kYXbnxk7aA5hog==", null, false, null, null, 1, "00ae6971-4d56-4fb0-a9e6-bcc0e93cb592", false, "admin2@admin.com" },
                    { 3, 0, "5ea48d12-5b71-4efe-a583-4bd35719e0e0", new DateTime(2023, 6, 14, 9, 39, 8, 613, DateTimeKind.Utc).AddTicks(4960), "user@mail.com", false, "User", "User", false, null, null, null, "USER@MAIL.COM", "USER@MAIL.COM", "AQAAAAIAAYagAAAAECMTcSnXKKT/JlFJLyrsutwfUCf4loFchWOsEGOwWXje4jK91ZmYEdLazbuQ8qUV6w==", null, false, null, null, 2, "99f023a9-316f-4afc-928e-b6a1793e677b", false, "user@mail.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
