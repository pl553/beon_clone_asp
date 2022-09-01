using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beon_clone_asp.Migrations
{
    public partial class avatarfilename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "AspNetUsers");
        }
    }
}
