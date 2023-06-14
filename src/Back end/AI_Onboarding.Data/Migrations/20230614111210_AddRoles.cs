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
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "ModifiedAt", "ModifiedById", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6570), null, null, "Administrator", "ADMINISTRATOR" },
                    { 2, null, new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6590), null, null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "ModifiedById", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "102cd9d7-6457-491f-a8fb-c2c326f25f29", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6680), "admin1@admin.com", false, "Admin1", "Admin1", false, null, null, null, "ADMIN1@ADMIN.COM", "ADMIN1@ADMIN.COM", "AQAAAAIAAYagAAAAEL27a7laPsuyR+EKWSK0iPnq28crSIz35Xl/jeQ+TEuOJgETPc1qFgyrukbMpSQjdg==", null, false, null, null, null, "a84a1148-0305-45c6-98f4-f0b62a13ba20", false, "admin1@admin.com" },
                    { 2, 0, "20fa6a59-b5b8-4374-8e97-e06b85d39815", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6730), "admin2@admin.com", false, "Admin2", "Admin2", false, null, null, null, "ADMIN2@ADMIN.COM", "ADMIN2@ADMIN.COM", "AQAAAAIAAYagAAAAEK83NYJHaV38AaGJyxXj+uYDebQp54GpfifeQ5nxrIFMYUZ5rjdSYiHst6dNmzNYvw==", null, false, null, null, null, "d07978d9-ccc8-4657-b2d4-44b632705871", false, "admin2@admin.com" },
                    { 3, 0, "c5914449-540f-4eaf-a9d6-69d1b0c76b4e", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6740), "user@mail.com", false, "User", "User", false, null, null, null, "USER@MAIL.COM", "USER@MAIL.COM", "AQAAAAIAAYagAAAAEHzPH3iHEMYkqDWn7HFgyKJ6TvIrY1ulZak3Y/z84cxcMQ+ifFuAcFr6lFg0gsDaAw==", null, false, null, null, null, "843c6244-7118-4d62-853d-fa67ac8172bd", false, "user@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedAt", "ModifiedAt", "ModifiedById" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 14, 11, 12, 10, 660, DateTimeKind.Utc).AddTicks(2680), null, null },
                    { 1, 2, new DateTime(2023, 6, 14, 11, 12, 10, 660, DateTimeKind.Utc).AddTicks(2680), null, null },
                    { 2, 3, new DateTime(2023, 6, 14, 11, 12, 10, 660, DateTimeKind.Utc).AddTicks(2750), null, null }
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
                principalColumn: "Id");
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
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
