using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaybookRepository",
                columns: table => new
                {
                    PlaybookSystemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaybookId = table.Column<int>(nullable: false),
                    OffenseFormationId = table.Column<int>(nullable: false),
                    DefenceFormationId = table.Column<int>(nullable: false),
                    PlayFullName = table.Column<string>(nullable: true),
                    PlayShortName = table.Column<string>(nullable: true),
                    PlayCallName = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    IntroduceStatus = table.Column<string>(nullable: true),
                    PlayDesignUrl = table.Column<string>(nullable: true),
                    Context = table.Column<string>(nullable: true),
                    CreateUserId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateUserId = table.Column<int>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaybookRepository", x => x.PlaybookSystemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaybookRepository");
        }
    }
}
