using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class parentwardupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentWards_Students_students",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_students",
                table: "ParentWards");

            migrationBuilder.DropColumn(
                name: "students",
                table: "ParentWards");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_StudentId",
                table: "ParentWards",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentWards_Students_StudentId",
                table: "ParentWards",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentWards_Students_StudentId",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_StudentId",
                table: "ParentWards");

            migrationBuilder.AddColumn<int>(
                name: "students",
                table: "ParentWards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_students",
                table: "ParentWards",
                column: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentWards_Students_students",
                table: "ParentWards",
                column: "students",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
