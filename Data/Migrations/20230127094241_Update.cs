using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Evaluations_StudentId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_CourseAllocations_LecturerId",
                table: "CourseAllocations");

            migrationBuilder.AddColumn<string>(
                name: "ToDoId",
                table: "Todos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Results",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Results",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ResultId",
                table: "Results",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EvaluationId",
                table: "Evaluations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "Courses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CourseRegId",
                table: "CourseRegistrations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CourseAllocationId",
                table: "CourseAllocations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeTableId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Venue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_TimeTables", x => x.Id);
                table.ForeignKey(
                    name: "FK_TimeTables_CourseAllocations_CourseId",
                    column: x => x.CourseId,
                    principalTable: "CourseAllocations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_TimeTables_CourseAllocations_LecturerId",
                    column: x => x.LecturerId,
                    principalTable: "CourseAllocations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Results_SessionId",
                table: "Results",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentId_CourseId_SessionId",
                table: "Results",
                columns: new[] { "StudentId", "CourseId", "SessionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentId_LecturerId_SessionId_CourseId",
                table: "Evaluations",
                columns: new[] { "StudentId", "LecturerId", "SessionId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseAllocations_LecturerId_CourseId",
                table: "CourseAllocations",
                columns: new[] { "LecturerId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_CourseId",
                table: "TimeTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_LecturerId",
                table: "TimeTables",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Sessions_SessionId",
                table: "Results",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Sessions_SessionId",
                table: "Results");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_Results_SessionId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_StudentId_CourseId_SessionId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_StudentId_LecturerId_SessionId_CourseId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_CourseAllocations_LecturerId_CourseId",
                table: "CourseAllocations");

            migrationBuilder.DropColumn(
                name: "ToDoId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseRegId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "CourseAllocationId",
                table: "CourseAllocations");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Results",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Results",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentId",
                table: "Evaluations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAllocations_LecturerId",
                table: "CourseAllocations",
                column: "LecturerId");
        }
    }
}
