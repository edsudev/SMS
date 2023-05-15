using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class PgParentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentAddress",
                table: "PostGraduateStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ParentAltPhone",
                table: "PostGraduateStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ParentEmail",
                table: "PostGraduateStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ParentName",
                table: "PostGraduateStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ParentOccupation",
                table: "PostGraduateStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ParentPhone",
                table: "PostGraduateStudents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentAddress",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ParentAltPhone",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ParentEmail",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ParentName",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ParentOccupation",
                table: "PostGraduateStudents");

            migrationBuilder.DropColumn(
                name: "ParentPhone",
                table: "PostGraduateStudents");
        }
    }
}
