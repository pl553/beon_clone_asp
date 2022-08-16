using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beon_clone_asp.Migrations
{
    public partial class BoardOwnerNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_AspNetUsers_OwnerId",
                table: "Diaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_Boards_BoardId",
                table: "Diaries");

            migrationBuilder.DropIndex(
                name: "IX_Diaries_BoardId",
                table: "Diaries");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Diaries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Diaries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_BoardId",
                table: "Diaries",
                column: "BoardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_OwnerName",
                table: "Boards",
                column: "OwnerName");

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_AspNetUsers_OwnerId",
                table: "Diaries",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_Boards_BoardId",
                table: "Diaries",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_AspNetUsers_OwnerId",
                table: "Diaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_Boards_BoardId",
                table: "Diaries");

            migrationBuilder.DropIndex(
                name: "IX_Diaries_BoardId",
                table: "Diaries");

            migrationBuilder.DropIndex(
                name: "IX_Boards_OwnerName",
                table: "Boards");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Diaries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Diaries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_BoardId",
                table: "Diaries",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_AspNetUsers_OwnerId",
                table: "Diaries",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_Boards_BoardId",
                table: "Diaries",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId");
        }
    }
}
