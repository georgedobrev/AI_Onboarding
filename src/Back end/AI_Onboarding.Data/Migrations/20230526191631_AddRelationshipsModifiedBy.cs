using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Onboarding.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipsModifiedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "RoleClaims");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "UserTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "UserLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "UserClaims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "RoleClaims",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_ModifiedById",
                table: "UserTokens",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedById",
                table: "Users",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_ModifiedById",
                table: "UserRoles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_ModifiedById",
                table: "UserLogins",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_ModifiedById",
                table: "UserClaims",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModifiedById",
                table: "Roles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_ModifiedById",
                table: "RoleClaims",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Users_ModifiedById",
                table: "RoleClaims",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_ModifiedById",
                table: "Roles",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_ModifiedById",
                table: "UserClaims",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_ModifiedById",
                table: "UserLogins",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_ModifiedById",
                table: "UserRoles",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ModifiedById",
                table: "Users",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_ModifiedById",
                table: "UserTokens",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Users_ModifiedById",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_ModifiedById",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_ModifiedById",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_ModifiedById",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_ModifiedById",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ModifiedById",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_ModifiedById",
                table: "UserTokens");

            migrationBuilder.DropIndex(
                name: "IX_UserTokens_ModifiedById",
                table: "UserTokens");

            migrationBuilder.DropIndex(
                name: "IX_Users_ModifiedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_ModifiedById",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserLogins_ModifiedById",
                table: "UserLogins");

            migrationBuilder.DropIndex(
                name: "IX_UserClaims_ModifiedById",
                table: "UserClaims");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ModifiedById",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaims_ModifiedById",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "RoleClaims");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserLogins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "RoleClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
