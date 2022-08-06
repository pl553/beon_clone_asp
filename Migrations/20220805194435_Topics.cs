using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beon_clone_asp.Migrations
{
    public partial class Topics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    BoardId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topic_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topic_BoardId",
                table: "Topic",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Topic");
        }
    }
}
