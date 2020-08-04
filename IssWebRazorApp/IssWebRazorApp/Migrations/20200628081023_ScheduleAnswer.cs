using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class ScheduleAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleAnswers",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleAnswers", x => new { x.ScheduleId, x.UserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
