using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class AddFootballNote_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballNotes",
                columns: table => new
                {
                    NodeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Context = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    CreateUserId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateUserId = table.Column<int>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    TargetNoteId = table.Column<int>(nullable: false),
                    TargetSession = table.Column<string>(nullable: true),
                    TargetCategoryCode = table.Column<string>(nullable: true),
                    TargetPlaybookId = table.Column<int>(nullable: false),
                    TargetPositionId = table.Column<int>(nullable: false),
                    TargetScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballNotes", x => x.NodeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballNotes");
        }
    }
}
