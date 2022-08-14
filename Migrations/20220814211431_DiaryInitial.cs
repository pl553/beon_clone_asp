using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beon_clone_asp.Migrations
{
    public partial class DiaryInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diaries",
                columns: table => new
                {
                    DiaryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardId = table.Column<int>(type: "INTEGER", nullable: true),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diaries", x => x.DiaryId);
                    table.ForeignKey(
                        name: "FK_Diaries_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Diaries_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_BoardId",
                table: "Diaries",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_OwnerId",
                table: "Diaries",
                column: "OwnerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diaries");
        }
    }
}
