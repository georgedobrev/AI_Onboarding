using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Onboarding.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameConversationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_Users_ModifiedById",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_Users_UserId",
                table: "Conversation");

            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_UserId",
                table: "Conversations",
                newName: "IX_Conversations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_ModifiedById",
                table: "Conversations",
                newName: "IX_Conversations_ModifiedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
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
                    table.PrimaryKey("PK_QuestionAnswers", x => new { x.ConversationId, x.Id });
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 28, 6, 33, 29, 366, DateTimeKind.Utc).AddTicks(7590));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 6, 28, 6, 33, 29, 366, DateTimeKind.Utc).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 28, 6, 33, 29, 472, DateTimeKind.Utc).AddTicks(1230));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 28, 6, 33, 29, 472, DateTimeKind.Utc).AddTicks(1230));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 },
                column: "CreatedAt",
                value: new DateTime(2023, 6, 28, 6, 33, 29, 472, DateTimeKind.Utc).AddTicks(1230));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e87d6f0c-216f-4b94-a45b-2b76fb4a4a25", new DateTime(2023, 6, 28, 6, 33, 29, 366, DateTimeKind.Utc).AddTicks(7660), "AQAAAAIAAYagAAAAEAdBgsSUVgxR2MEeca3h++bbdYYGhMvuY/WIV9d9UyMLZ3j8XFB+wKnGPmlsBiOugg==", "63c68360-6d2a-4406-9e40-a433e7314b4f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cc2375c-3632-42ea-a9c7-173247129406", new DateTime(2023, 6, 28, 6, 33, 29, 366, DateTimeKind.Utc).AddTicks(7740), "AQAAAAIAAYagAAAAEBjZE9uVn3WTqug1B5urEWtMP/BMKlC86JPZKtHOEXgRZJEK/b12avgukrA1coEQ1Q==", "ba771a65-4064-43d5-a801-bbfbf789be22" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "580b80e0-1ae5-4dc0-a478-8519b6f16a02", new DateTime(2023, 6, 28, 6, 33, 29, 366, DateTimeKind.Utc).AddTicks(7750), "AQAAAAIAAYagAAAAELSNDkb74ELNDw+MlneYFLWOWbQ/qZMuw19NolFofhUWUt+V9BbHs8pJxpUc4+PtuQ==", "3f3a7203-a2db-4354-8aa3-fec56e587261" });

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_ModifiedById",
                table: "Conversations",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Users_UserId",
                table: "Conversations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_ModifiedById",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Users_UserId",
                table: "Conversations");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversation");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_UserId",
                table: "Conversation",
                newName: "IX_Conversation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_ModifiedById",
                table: "Conversation",
                newName: "IX_Conversation_ModifiedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_Users_ModifiedById",
                table: "Conversation",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_Users_UserId",
                table: "Conversation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
