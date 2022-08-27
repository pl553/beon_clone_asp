using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beon_clone_asp.Migrations
{
    public partial class topiccascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Topics_TopicId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Topics_TopicId",
                table: "Posts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Topics_TopicId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "Posts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Topics_TopicId",
                table: "Posts",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId");
        }
    }
}
