using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class BursaryCleared : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmetId",
                table: "TimeTables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "TimeTables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BursaryClearedStudents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryClearedStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BursaryClearedStudents_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BursaryClearedStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParentWards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    students = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentWards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentWards_Parent_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParentWards_Students_students",
                        column: x => x.students,
                        principalTable: "Students",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_DepartmetId",
                table: "TimeTables",
                column: "DepartmetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_LevelId",
                table: "TimeTables",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearedStudents_SessionId",
                table: "BursaryClearedStudents",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearedStudents_StudentId",
                table: "BursaryClearedStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_ParentId",
                table: "ParentWards",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_students",
                table: "ParentWards",
                column: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Departments_DepartmetId",
                table: "TimeTables",
                column: "DepartmetId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Levels_LevelId",
                table: "TimeTables",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Departments_DepartmetId",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Levels_LevelId",
                table: "TimeTables");

            migrationBuilder.DropTable(
                name: "BursaryClearedStudents");

            migrationBuilder.DropTable(
                name: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_DepartmetId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_LevelId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "DepartmetId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "TimeTables");
        }
    }
}
