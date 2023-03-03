using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class ApplicantIdStudentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "UgSubWallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "UgMainWallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicantionId",
                table: "UgApplicants",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProgrameId",
                table: "UgApplicants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsStillAStudent",
                table: "Students",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProgrameId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "PostGraduateStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsStillAStudent",
                table: "PostGraduateStudents",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "PgSubWallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "PgMainWallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicantionId",
                table: "PgApplicants",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameOfProgram = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UgSubWallets_ApplicantId",
                table: "UgSubWallets",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_UgMainWallets_ApplicantId",
                table: "UgMainWallets",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_UgApplicants_ProgrameId",
                table: "UgApplicants",
                column: "ProgrameId");

            migrationBuilder.CreateIndex(
                name: "IX_UgApplicants_YearOfAdmission",
                table: "UgApplicants",
                column: "YearOfAdmission");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ApplicantId",
                table: "Students",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgrameId",
                table: "Students",
                column: "ProgrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_YearOfAdmission",
                table: "Students",
                column: "YearOfAdmission");

            migrationBuilder.CreateIndex(
                name: "IX_PostGraduateStudents_ApplicantId",
                table: "PostGraduateStudents",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGraduateStudents_YearOfAdmission",
                table: "PostGraduateStudents",
                column: "YearOfAdmission");

            migrationBuilder.CreateIndex(
                name: "IX_PgSubWallets_ApplicantId",
                table: "PgSubWallets",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_PgMainWallets_ApplicantId",
                table: "PgMainWallets",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_PgApplicants_YearOfAdmission",
                table: "PgApplicants",
                column: "YearOfAdmission");

            migrationBuilder.AddForeignKey(
                name: "FK_PgApplicants_Sessions_YearOfAdmission",
                table: "PgApplicants",
                column: "YearOfAdmission",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PgMainWallets_UgApplicants_ApplicantId",
                table: "PgMainWallets",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PgSubWallets_UgApplicants_ApplicantId",
                table: "PgSubWallets",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGraduateStudents_Sessions_YearOfAdmission",
                table: "PostGraduateStudents",
                column: "YearOfAdmission",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGraduateStudents_UgApplicants_ApplicantId",
                table: "PostGraduateStudents",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Programs_ProgrameId",
                table: "Students",
                column: "ProgrameId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sessions_YearOfAdmission",
                table: "Students",
                column: "YearOfAdmission",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_UgApplicants_ApplicantId",
                table: "Students",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UgApplicants_Programs_ProgrameId",
                table: "UgApplicants",
                column: "ProgrameId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UgApplicants_Sessions_YearOfAdmission",
                table: "UgApplicants",
                column: "YearOfAdmission",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UgMainWallets_UgApplicants_ApplicantId",
                table: "UgMainWallets",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UgSubWallets_UgApplicants_ApplicantId",
                table: "UgSubWallets",
                column: "ApplicantId",
                principalTable: "UgApplicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PgApplicants_Sessions_YearOfAdmission",
                table: "PgApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_PgMainWallets_UgApplicants_ApplicantId",
                table: "PgMainWallets");

            migrationBuilder.DropForeignKey(
                name: "FK_PgSubWallets_UgApplicants_ApplicantId",
                table: "PgSubWallets");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGraduateStudents_Sessions_YearOfAdmission",
                table: "PostGraduateStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGraduateStudents_UgApplicants_ApplicantId",
                table: "PostGraduateStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Programs_ProgrameId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sessions_YearOfAdmission",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_UgApplicants_ApplicantId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_UgApplicants_Programs_ProgrameId",
                table: "UgApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_UgApplicants_Sessions_YearOfAdmission",
                table: "UgApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_UgMainWallets_UgApplicants_ApplicantId",
                table: "UgMainWallets");

            migrationBuilder.DropForeignKey(
                name: "FK_UgSubWallets_UgApplicants_ApplicantId",
                table: "UgSubWallets");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_UgSubWallets_ApplicantId",
                table: "UgSubWallets");

            migrationBuilder.DropIndex(
                name: "IX_UgMainWallets_ApplicantId",
                table: "UgMainWallets");

            migrationBuilder.DropIndex(
                name: "IX_UgApplicants_ProgrameId",
                table: "UgApplicants");

            migrationBuilder.DropIndex(
                name: "IX_UgApplicants_YearOfAdmission",
                table: "UgApplicants");

            migrationBuilder.DropIndex(
                name: "IX_Students_ApplicantId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgrameId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_YearOfAdmission",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_PostGraduateStudents_ApplicantId",
                table: "PostGraduateStudents");

            migrationBuilder.DropIndex(
                name: "IX_PostGraduateStudents_YearOfAdmission",
                table: "PostGraduateStudents");

            migrationBuilder.DropIndex(
                name: "IX_PgSubWallets_ApplicantId",
                table: "PgSubWallets");

            migrationBuilder.DropIndex(
                name: "IX_PgMainWallets_ApplicantId",
                table: "PgMainWallets");

            migrationBuilder.DropIndex(
                name: "IX_PgApplicants_YearOfAdmission",
                table: "PgApplicants");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "UgSubWallets");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "UgMainWallets");

            migrationBuilder.DropColumn(
                name: "ApplicantionId",
                table: "UgApplicants");

            migrationBuilder.DropColumn(
                name: "ProgrameId",
                table: "UgApplicants");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsStillAStudent",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgrameId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "IsStillAStudent",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "PgSubWallets");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "PgMainWallets");

            migrationBuilder.DropColumn(
                name: "ApplicantionId",
                table: "PgApplicants");
        }
    }
}
