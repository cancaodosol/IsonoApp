using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class Initial0320 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "PlaybookRepository");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playbook",
                table: "Playbook");

            migrationBuilder.RenameTable(
                name: "Playbook",
                newName: "Playbooks");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Playbooks",
                table: "Playbooks",
                column: "PlaybookSystemId");*/

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: true),
                    Session = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playbooks",
                table: "Playbooks");

            migrationBuilder.RenameTable(
                name: "Playbooks",
                newName: "Playbook");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playbook",
                table: "Playbook",
                column: "PlaybookSystemId");

            migrationBuilder.CreateTable(
                name: "PlaybookRepository",
                columns: table => new
                {
                    PlaybookSystemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Context = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefenceFormationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IntroduceStatus = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    OffenseFormationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayCallName = table.Column<string>(type: "TEXT", nullable: true),
                    PlayDesignUrl = table.Column<string>(type: "TEXT", nullable: true),
                    PlayFullName = table.Column<string>(type: "TEXT", nullable: true),
                    PlayShortName = table.Column<string>(type: "TEXT", nullable: true),
                    PlaybookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaybookRepository", x => x.PlaybookSystemId);
                });
        }
    }
}
