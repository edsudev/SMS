using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class mailbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Staffs_Through",
                table: "Mails");

            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Staffs_Through2",
                table: "Mails");

            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Staffs_Through3",
                table: "Mails");

            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Staffs_To",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_Through",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_Through2",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_Through3",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_To",
                table: "Mails");

            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "Sliders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Mails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Through3",
                table: "Mails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Through2",
                table: "Mails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Through",
                table: "Mails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Acknowledgment",
                table: "IctComplaints",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isActive",
                table: "Sliders",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "To",
                table: "Mails",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Through3",
                table: "Mails",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Through2",
                table: "Mails",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Through",
                table: "Mails",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Acknowledgment",
                table: "IctComplaints",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_Through",
                table: "Mails",
                column: "Through");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_Through2",
                table: "Mails",
                column: "Through2");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_Through3",
                table: "Mails",
                column: "Through3");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_To",
                table: "Mails",
                column: "To");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Staffs_Through",
                table: "Mails",
                column: "Through",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Staffs_Through2",
                table: "Mails",
                column: "Through2",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Staffs_Through3",
                table: "Mails",
                column: "Through3",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Staffs_To",
                table: "Mails",
                column: "To",
                principalTable: "Staffs",
                principalColumn: "Id");
        }
    }
}
