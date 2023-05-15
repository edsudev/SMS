using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class ConversionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentStatus",
                table: "PostGraduateStudents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConversionStudent",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConversionProjectProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Program = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    Ranking = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionProjectProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversionProjectProgresses_ConversionPrograms_Program",
                        column: x => x.Program,
                        principalTable: "ConversionPrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionProjectProgresses_ConversionStudentSupervisors_Stu~",
                        column: x => x.StudentId,
                        principalTable: "ConversionStudentSupervisors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversionProjectProgresses_ConversionStudentSupervisors_Sup~",
                        column: x => x.SupervisorId,
                        principalTable: "ConversionStudentSupervisors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConversionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CA = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Exam = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Total = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Upgrade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversionResults", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConversionStudent",
                table: "AspNetUsers",
                column: "ConversionStudent");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionProjectProgresses_Program",
                table: "ConversionProjectProgresses",
                column: "Program");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionProjectProgresses_StudentId",
                table: "ConversionProjectProgresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionProjectProgresses_SupervisorId",
                table: "ConversionProjectProgresses",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ConversionStudents_ConversionStudent",
                table: "AspNetUsers",
                column: "ConversionStudent",
                principalTable: "ConversionStudents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ConversionStudents_ConversionStudent",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ConversionProjectProgresses");

            migrationBuilder.DropTable(
                name: "ConversionResults");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConversionStudent",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentStatus",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ConversionStudent",
                table: "AspNetUsers");
        }
    }
}
