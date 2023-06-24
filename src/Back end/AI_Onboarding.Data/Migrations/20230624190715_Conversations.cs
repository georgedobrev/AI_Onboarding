using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Onboarding.Data.Migrations
{
    /// <inheritdoc />
    public partial class Conversations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Conversation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => new { x.ConversationId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 24, 19, 7, 15, 282, DateTimeKind.Utc).AddTicks(5910));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 24, 19, 7, 15, 282, DateTimeKind.Utc).AddTicks(5910));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 24, 19, 7, 15, 387, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 24, 19, 7, 15, 387, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 24, 19, 7, 15, 387, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b294fd9-a00c-4cb2-a370-195f7b0a3fc4", new DateTime(2023, 6, 24, 19, 7, 15, 282, DateTimeKind.Utc).AddTicks(5970), "AQAAAAIAAYagAAAAEEM4Cd43AJBjXTG/eLE0Ccm/y9G88JYBQ5bdID2Ev22JbNIl3Ev81euB41/IfhmWkg==", "39bf2397-e172-4870-8dca-cf7346384cdf" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52dba547-5176-4164-83f8-03927be77dd7", new DateTime(2023, 6, 24, 19, 7, 15, 282, DateTimeKind.Utc).AddTicks(6030), "AQAAAAIAAYagAAAAEGL6vtF/O6oW/LDWW7AoAkx5Vu9HVuMy09j8dyfUOBJvq3KPCz4v7OJgMoryhGsIsg==", "298d75c8-347d-450c-873b-30f8b2a166fd" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fa9d193-4af2-4dee-9119-0bb024b1d890", new DateTime(2023, 6, 24, 19, 7, 15, 282, DateTimeKind.Utc).AddTicks(6040), "AQAAAAIAAYagAAAAEHQQHbFqgeT5D7MAdKhF0I3xBsZDfeRGXCVo3dszVs19hxdHYDLBbfCSgpcZ/E9m/w==", "5e781c6c-737f-43c1-bb5c-887f01ee4346" });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ModifiedById",
                table: "Conversation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_UserId",
                table: "Conversation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropTable(
                name: "Conversation");

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
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "102cd9d7-6457-491f-a8fb-c2c326f25f29", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6680), "AQAAAAIAAYagAAAAEL27a7laPsuyR+EKWSK0iPnq28crSIz35Xl/jeQ+TEuOJgETPc1qFgyrukbMpSQjdg==", "a84a1148-0305-45c6-98f4-f0b62a13ba20" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20fa6a59-b5b8-4374-8e97-e06b85d39815", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6730), "AQAAAAIAAYagAAAAEK83NYJHaV38AaGJyxXj+uYDebQp54GpfifeQ5nxrIFMYUZ5rjdSYiHst6dNmzNYvw==", "d07978d9-ccc8-4657-b2d4-44b632705871" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5914449-540f-4eaf-a9d6-69d1b0c76b4e", new DateTime(2023, 6, 14, 11, 12, 10, 548, DateTimeKind.Utc).AddTicks(6740), "AQAAAAIAAYagAAAAEHzPH3iHEMYkqDWn7HFgyKJ6TvIrY1ulZak3Y/z84cxcMQ+ifFuAcFr6lFg0gsDaAw==", "843c6244-7118-4d62-853d-fa67ac8172bd" });
        }
    }
}
