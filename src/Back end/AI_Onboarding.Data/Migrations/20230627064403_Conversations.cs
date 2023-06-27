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
                value: new DateTime(2023, 6, 27, 6, 44, 3, 10, DateTimeKind.Utc).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 27, 6, 44, 3, 10, DateTimeKind.Utc).AddTicks(570));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 27, 6, 44, 3, 116, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 27, 6, 44, 3, 116, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 27, 6, 44, 3, 116, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d22220cd-c95e-4e27-8243-d3ebb267f519", new DateTime(2023, 6, 27, 6, 44, 3, 10, DateTimeKind.Utc).AddTicks(630), "AQAAAAIAAYagAAAAELlnwR3GDZ4izljiQjyd9ep0IcvzpicscFc3eLoVL83+3t+bzwkzxc9Zs+DZf1FptA==", "cafd87bf-c2f8-4264-90ef-9726336038cc" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8818a77-99ff-45bf-9e6d-52a4b1b7f96e", new DateTime(2023, 6, 27, 6, 44, 3, 10, DateTimeKind.Utc).AddTicks(710), "AQAAAAIAAYagAAAAEN63fE0JmfM9oKc+0SicQ9tkl1BiFwkHl9RkavgTSu8gG7hZ2QlKx8p5JuIvet3bcQ==", "037aef5c-42fc-4701-904c-4d852365e5cb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aaa72e89-5493-4f68-9b97-adfff96fa918", new DateTime(2023, 6, 27, 6, 44, 3, 10, DateTimeKind.Utc).AddTicks(720), "AQAAAAIAAYagAAAAEOANsfVUWVkk3KIp7UKx4qsR4J7oH4B4kaMpmT1FtZP/2icO6ScAByMZR1sQVWt+tQ==", "715ea76a-aa33-44b5-975e-7a7fc7f6b2ea" });

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
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db322c9d-4fc5-4b24-a556-9ff9782cebc8", new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6100), "AQAAAAIAAYagAAAAEK7Spx3gg8lNkg1VfPtjcdnZtCRTcq1NXNPnDmI/iOGmr1xOjebVn0oEA9hD3wZu/w==", "e30a8931-bda3-4825-93f7-90651a298379" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d308e54-3037-45d6-9da5-2acb7f07e7c9", new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6130), "AQAAAAIAAYagAAAAEEBGTFVnMf4np76jvoSRVWVnNYSjmmgUPsC4mI8x7ByNaZX8ctVYxIna71TaeNEwiw==", "3ad6ddbe-52f4-4a7a-a21c-bb2f646f5321" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "846f1eca-fd9f-4da3-9cac-1034a5f2853a", new DateTime(2023, 6, 26, 9, 9, 51, 401, DateTimeKind.Utc).AddTicks(6140), "AQAAAAIAAYagAAAAEGErH06Mw6zX11AB++A8X6ukFL36FPHnJXzLfEO7N5+i2QYDWqXQK3H1vxmWvmJDZw==", "6c88fcda-539e-4ee4-9bca-b2bf9f0c7559" });
        }
    }
}
