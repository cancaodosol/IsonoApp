using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class ScheduleAnswer_addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ScheduleAnswers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ScheduleAnswers");
        }
    }
}
