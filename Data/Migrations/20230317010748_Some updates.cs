using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class Someupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade21",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade22",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade23",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationQuestions_QuestionId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade21",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade22",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade23",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_QuestionId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Grade21",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade22",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade23",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Evaluations");

            migrationBuilder.AlterColumn<int>(
                name: "Position",
                table: "Staffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CoverImg",
                table: "Events",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EventID",
                table: "Events",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Events",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Position",
                table: "Staffs",
                column: "Position");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Positions_Position",
                table: "Staffs",
                column: "Position",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Positions_Position",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_Position",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CoverImg",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Staffs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Grade21",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Grade22",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Grade23",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade21",
                table: "Evaluations",
                column: "Grade21");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade22",
                table: "Evaluations",
                column: "Grade22");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade23",
                table: "Evaluations",
                column: "Grade23");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_QuestionId",
                table: "Evaluations",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade21",
                table: "Evaluations",
                column: "Grade21",
                principalTable: "EvaluationGrades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade22",
                table: "Evaluations",
                column: "Grade22",
                principalTable: "EvaluationGrades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade23",
                table: "Evaluations",
                column: "Grade23",
                principalTable: "EvaluationGrades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationQuestions_QuestionId",
                table: "Evaluations",
                column: "QuestionId",
                principalTable: "EvaluationQuestions",
                principalColumn: "Id");
        }
    }
}
