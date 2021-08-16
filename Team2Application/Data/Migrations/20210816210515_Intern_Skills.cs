using Microsoft.EntityFrameworkCore.Migrations;

namespace Team2Application.Data.Migrations
{
    public partial class Intern_Skills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Intern_InternId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_InternId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "InternId",
                table: "Skill");

            migrationBuilder.CreateTable(
                name: "Intern_Skill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intern_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intern_Skill_Intern_InternId",
                        column: x => x.InternId,
                        principalTable: "Intern",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intern_Skill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intern_Skill_InternId",
                table: "Intern_Skill",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Intern_Skill_SkillId",
                table: "Intern_Skill",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intern_Skill");

            migrationBuilder.AddColumn<int>(
                name: "InternId",
                table: "Skill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_InternId",
                table: "Skill",
                column: "InternId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Intern_InternId",
                table: "Skill",
                column: "InternId",
                principalTable: "Intern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
