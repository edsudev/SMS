using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class Moreupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Acknowledgment",
                table: "Works",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MainStatus",
                table: "Works",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Staffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Semesters",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "PgCourses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReturnStatus",
                table: "Exeats",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "ConversionCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VacationExeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExeatId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MatNo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hall = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Destination = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartureDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AltParentPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChiefPortalStatus = table.Column<int>(type: "int", nullable: true),
                    HallMasterStatus = table.Column<int>(type: "int", nullable: true),
                    Dean = table.Column<int>(type: "int", nullable: true),
                    GatePass = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationExeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationExeats_Hostels_Hall",
                        column: x => x.Hall,
                        principalTable: "Hostels",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Type",
                table: "Staffs",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_PgCourses_SessionId",
                table: "PgCourses",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SessionId",
                table: "Courses",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversionCourses_SessionId",
                table: "ConversionCourses",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationExeats_Hall",
                table: "VacationExeats",
                column: "Hall");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversionCourses_Sessions_SessionId",
                table: "ConversionCourses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Sessions_SessionId",
                table: "Courses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PgCourses_Sessions_SessionId",
                table: "PgCourses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Types_Type",
                table: "Staffs",
                column: "Type",
                principalTable: "Types",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversionCourses_Sessions_SessionId",
                table: "ConversionCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Sessions_SessionId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_PgCourses_Sessions_SessionId",
                table: "PgCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Types_Type",
                table: "Staffs");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "VacationExeats");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_Type",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_PgCourses_SessionId",
                table: "PgCourses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SessionId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_ConversionCourses_SessionId",
                table: "ConversionCourses");

            migrationBuilder.DropColumn(
                name: "MainStatus",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "PgCourses");

            migrationBuilder.DropColumn(
                name: "ReturnStatus",
                table: "Exeats");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "ConversionCourses");

            migrationBuilder.AlterColumn<string>(
                name: "Acknowledgment",
                table: "Works",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Staffs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
