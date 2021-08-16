using Microsoft.EntityFrameworkCore.Migrations;

namespace Team2Application.Data.Migrations
{
    public partial class Skill_LibraryResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryResource_Skill_SkillId",
                table: "LibraryResource");

            migrationBuilder.DropIndex(
                name: "IX_LibraryResource_SkillId",
                table: "LibraryResource");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "LibraryResource");

            migrationBuilder.AddColumn<int>(
                name: "InternId",
                table: "Skill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Skill_LibraryResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    LibraryResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill_LibraryResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_LibraryResource_LibraryResource_LibraryResourceId",
                        column: x => x.LibraryResourceId,
                        principalTable: "LibraryResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_LibraryResource_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_InternId",
                table: "Skill",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_LibraryResource_LibraryResourceId",
                table: "Skill_LibraryResource",
                column: "LibraryResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_LibraryResource_SkillId",
                table: "Skill_LibraryResource",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Intern_InternId",
                table: "Skill",
                column: "InternId",
                principalTable: "Intern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Intern_InternId",
                table: "Skill");

            migrationBuilder.DropTable(
                name: "Skill_LibraryResource");

            migrationBuilder.DropIndex(
                name: "IX_Skill_InternId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "InternId",
                table: "Skill");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "LibraryResource",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryResource_SkillId",
                table: "LibraryResource",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryResource_Skill_SkillId",
                table: "LibraryResource",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
