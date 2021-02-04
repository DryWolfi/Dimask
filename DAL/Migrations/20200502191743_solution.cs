using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class solution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "UserAssignment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "UserAssignment");
        }
    }
}
