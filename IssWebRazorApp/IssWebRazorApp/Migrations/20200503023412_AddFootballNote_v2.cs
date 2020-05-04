using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class AddFootballNote_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FootballNotes",
                table: "FootballNotes");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "FootballNotes");

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "FootballNotes",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FootballNotes",
                table: "FootballNotes",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FootballNotes",
                table: "FootballNotes");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "FootballNotes");

            migrationBuilder.AddColumn<int>(
                name: "NodeId",
                table: "FootballNotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FootballNotes",
                table: "FootballNotes",
                column: "NodeId");
        }
    }
}
