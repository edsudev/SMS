using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class EvaluationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade",
                table: "Evaluations");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Evaluations",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Evaluations_Grade",
                table: "Evaluations",
                newName: "IX_Evaluations_SessionId");

            migrationBuilder.AddColumn<int>(
                name: "Grade1",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade10",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade11",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade12",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade13",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade14",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade15",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade16",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade17",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade18",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade19",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade2",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade20",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade21",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade22",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade23",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade3",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade4",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade5",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade6",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade7",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade8",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade9",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade1",
                table: "Evaluations",
                column: "Grade1");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade10",
                table: "Evaluations",
                column: "Grade10");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade11",
                table: "Evaluations",
                column: "Grade11");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade12",
                table: "Evaluations",
                column: "Grade12");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade13",
                table: "Evaluations",
                column: "Grade13");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade14",
                table: "Evaluations",
                column: "Grade14");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade15",
                table: "Evaluations",
                column: "Grade15");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade16",
                table: "Evaluations",
                column: "Grade16");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade17",
                table: "Evaluations",
                column: "Grade17");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade18",
                table: "Evaluations",
                column: "Grade18");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade19",
                table: "Evaluations",
                column: "Grade19");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade2",
                table: "Evaluations",
                column: "Grade2");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade20",
                table: "Evaluations",
                column: "Grade20");

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
                name: "IX_Evaluations_Grade3",
                table: "Evaluations",
                column: "Grade3");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade4",
                table: "Evaluations",
                column: "Grade4");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade5",
                table: "Evaluations",
                column: "Grade5");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade6",
                table: "Evaluations",
                column: "Grade6");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade7",
                table: "Evaluations",
                column: "Grade7");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade8",
                table: "Evaluations",
                column: "Grade8");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Grade9",
                table: "Evaluations",
                column: "Grade9");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_SemesterId",
                table: "Evaluations",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade1",
                table: "Evaluations",
                column: "Grade1",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade10",
                table: "Evaluations",
                column: "Grade10",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade11",
                table: "Evaluations",
                column: "Grade11",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade12",
                table: "Evaluations",
                column: "Grade12",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade13",
                table: "Evaluations",
                column: "Grade13",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade14",
                table: "Evaluations",
                column: "Grade14",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade15",
                table: "Evaluations",
                column: "Grade15",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade16",
                table: "Evaluations",
                column: "Grade16",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade17",
                table: "Evaluations",
                column: "Grade17",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade18",
                table: "Evaluations",
                column: "Grade18",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade19",
                table: "Evaluations",
                column: "Grade19",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade2",
                table: "Evaluations",
                column: "Grade2",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade20",
                table: "Evaluations",
                column: "Grade20",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade21",
                table: "Evaluations",
                column: "Grade21",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade22",
                table: "Evaluations",
                column: "Grade22",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade23",
                table: "Evaluations",
                column: "Grade23",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade3",
                table: "Evaluations",
                column: "Grade3",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade4",
                table: "Evaluations",
                column: "Grade4",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade5",
                table: "Evaluations",
                column: "Grade5",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade6",
                table: "Evaluations",
                column: "Grade6",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade7",
                table: "Evaluations",
                column: "Grade7",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade8",
                table: "Evaluations",
                column: "Grade8",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade9",
                table: "Evaluations",
                column: "Grade9",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Semesters_SemesterId",
                table: "Evaluations",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Sessions_SessionId",
                table: "Evaluations",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade1",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade10",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade11",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade12",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade13",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade14",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade15",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade16",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade17",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade18",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade19",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade2",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade20",
                table: "Evaluations");

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
                name: "FK_Evaluations_EvaluationGrades_Grade3",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade4",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade5",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade6",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade7",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade8",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade9",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Semesters_SemesterId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Sessions_SessionId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade1",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade10",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade11",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade12",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade13",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade14",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade15",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade16",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade17",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade18",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade19",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade2",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade20",
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
                name: "IX_Evaluations_Grade3",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade4",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade5",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade6",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade7",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade8",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_Grade9",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_SemesterId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade1",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade10",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade11",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade12",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade13",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade14",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade15",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade16",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade17",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade18",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade19",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade2",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade20",
                table: "Evaluations");

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
                name: "Grade3",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade4",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade5",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade6",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade7",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade8",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Grade9",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Evaluations");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Evaluations",
                newName: "Grade");

            migrationBuilder.RenameIndex(
                name: "IX_Evaluations_SessionId",
                table: "Evaluations",
                newName: "IX_Evaluations_Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_EvaluationGrades_Grade",
                table: "Evaluations",
                column: "Grade",
                principalTable: "EvaluationGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
