using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class Programupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Programs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "PgPrograms",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "PgPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Payments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "BursaryClearances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_DepartmentId",
                table: "Programs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PgPrograms_DepartmentId",
                table: "PgPrograms",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearances_SessionId",
                table: "BursaryClearances",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BursaryClearances_Sessions_SessionId",
                table: "BursaryClearances",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PgPrograms_Departments_DepartmentId",
                table: "PgPrograms",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Departments_DepartmentId",
                table: "Programs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BursaryClearances_Sessions_SessionId",
                table: "BursaryClearances");

            migrationBuilder.DropForeignKey(
                name: "FK_PgPrograms_Departments_DepartmentId",
                table: "PgPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Departments_DepartmentId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_DepartmentId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_PgPrograms_DepartmentId",
                table: "PgPrograms");

            migrationBuilder.DropIndex(
                name: "IX_BursaryClearances_SessionId",
                table: "BursaryClearances");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "PgPrograms");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "PgPrograms");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "BursaryClearances");
        }
    }
}
