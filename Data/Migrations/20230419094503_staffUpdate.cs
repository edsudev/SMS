using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class staffUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Staffs",
                newName: "Surname");

            migrationBuilder.AlterColumn<string>(
                name: "DOB",
                table: "Staffs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Staffs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BirthCert",
                table: "Staffs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Staffs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Staffs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Staffs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "BirthCert",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Staffs");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Staffs",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Staffs",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
