using Microsoft.EntityFrameworkCore.Migrations;

namespace IssWebRazorApp.Migrations
{
    public partial class add_20200408 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionType = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(nullable: true),
                    FirstNameKanji = table.Column<string>(nullable: true),
                    LastNameKanji = table.Column<string>(nullable: true),
                    FirstNameRoman = table.Column<string>(nullable: true),
                    LastNameRoman = table.Column<string>(nullable: true),
                    LoginPassword = table.Column<string>(nullable: true),
                    UniformNumber = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    UserType = table.Column<string>(nullable: true),
                    SystemRole = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    Height = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
