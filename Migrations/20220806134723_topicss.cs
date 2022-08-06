using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beon_clone_asp.Migrations
{
    public partial class topicss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Boards_BoardId",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameIndex(
                name: "IX_Topic_BoardId",
                table: "Topics",
                newName: "IX_Topics_BoardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Boards_BoardId",
                table: "Topics",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Boards_BoardId",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_BoardId",
                table: "Topic",
                newName: "IX_Topic_BoardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Boards_BoardId",
                table: "Topic",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId");
        }
    }
}
