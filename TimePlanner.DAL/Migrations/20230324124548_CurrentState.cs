using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimePlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CurrentState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityTypeEntity_TypeId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTypeEntity_Users_UserId",
                table: "ActivityTypeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTypeEntity",
                table: "ActivityTypeEntity");

            migrationBuilder.RenameTable(
                name: "ActivityTypeEntity",
                newName: "ActivityTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityTypeEntity_UserId",
                table: "ActivityTypes",
                newName: "IX_ActivityTypes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTypes",
                table: "ActivityTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityTypes_TypeId",
                table: "Activities",
                column: "TypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTypes_Users_UserId",
                table: "ActivityTypes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityTypes_TypeId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTypes_Users_UserId",
                table: "ActivityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTypes",
                table: "ActivityTypes");

            migrationBuilder.RenameTable(
                name: "ActivityTypes",
                newName: "ActivityTypeEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityTypes_UserId",
                table: "ActivityTypeEntity",
                newName: "IX_ActivityTypeEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTypeEntity",
                table: "ActivityTypeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityTypeEntity_TypeId",
                table: "Activities",
                column: "TypeId",
                principalTable: "ActivityTypeEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTypeEntity_Users_UserId",
                table: "ActivityTypeEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
