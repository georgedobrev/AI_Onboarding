using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Onboarding.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfirmedEmailSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6020));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6030));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 9, 9, 51, 510, DateTimeKind.Utc).AddTicks(3160));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 9, 9, 51, 510, DateTimeKind.Utc).AddTicks(3160));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 9, 9, 51, 510, DateTimeKind.Utc).AddTicks(3170));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db322c9d-4fc5-4b24-a556-9ff9782cebc8", new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6100), true, "AQAAAAIAAYagAAAAEK7Spx3gg8lNkg1VfPtjcdnZtCRTcq1NXNPnDmI/iOGmr1xOjebVn0oEA9hD3wZu/w==", "e30a8931-bda3-4825-93f7-90651a298379" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d308e54-3037-45d6-9da5-2acb7f07e7c9", new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6130), true, "AQAAAAIAAYagAAAAEEBGTFVnMf4np76jvoSRVWVnNYSjmmgUPsC4mI8x7ByNaZX8ctVYxIna71TaeNEwiw==", "3ad6ddbe-52f4-4a7a-a21c-bb2f646f5321" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "846f1eca-fd9f-4da3-9cac-1034a5f2853a", new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6140), true, "AQAAAAIAAYagAAAAEGErH06Mw6zX11AB++A8X6ukFL36FPHnJXzLfEO7N5+i2QYDWqXQK3H1vxmWvmJDZw==", "6c88fcda-539e-4ee4-9bca-b2bf9f0c7559" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6570));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 11, 12, 10, 660, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 11, 12, 10, 660, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 11, 12, 10, 660, DateTimeKind.Utc).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "102cd9d7-6457-491f-a8fb-c2c326f25f29", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6680), false, "AQAAAAIAAYagAAAAEL27a7laPsuyR+EKWSK0iPnq28crSIz35Xl/jeQ+TEuOJgETPc1qFgyrukbMpSQjdg==", "a84a1148-0305-45c6-98f4-f0b62a13ba20" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20fa6a59-b5b8-4374-8e97-e06b85d39815", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6730), false, "AQAAAAIAAYagAAAAEK83NYJHaV38AaGJyxXj+uYDebQp54GpfifeQ5nxrIFMYUZ5rjdSYiHst6dNmzNYvw==", "d07978d9-ccc8-4657-b2d4-44b632705871" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5914449-540f-4eaf-a9d6-69d1b0c76b4e", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6740), false, "AQAAAAIAAYagAAAAEHzPH3iHEMYkqDWn7HFgyKJ6TvIrY1ulZak3Y/z84cxcMQ+ifFuAcFr6lFg0gsDaAw==", "843c6244-7118-4d62-853d-fa67ac8172bd" });
        }
    }
}
