using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class OfflinePaymentClearance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditWallets_Students_StudentId",
                table: "CreditWallets");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_ParentId_StudentId",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_StudentId",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_CreditWallets_StudentId",
                table: "CreditWallets");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CreditWallets");

            migrationBuilder.AddColumn<string>(
                name: "UTME",
                table: "UgMainWallets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UTME",
                table: "CreditWallets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OfflinePaymentClearances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    Mode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflinePaymentClearances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfflinePaymentClearances_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfflinePaymentClearances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_ParentId",
                table: "ParentWards",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_StudentId_ParentId",
                table: "ParentWards",
                columns: new[] { "StudentId", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfflinePaymentClearances_SessionId",
                table: "OfflinePaymentClearances",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflinePaymentClearances_StudentId",
                table: "OfflinePaymentClearances",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfflinePaymentClearances");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_ParentId",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_StudentId_ParentId",
                table: "ParentWards");

            migrationBuilder.DropColumn(
                name: "UTME",
                table: "UgMainWallets");

            migrationBuilder.DropColumn(
                name: "UTME",
                table: "CreditWallets");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "CreditWallets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_ParentId_StudentId",
                table: "ParentWards",
                columns: new[] { "ParentId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_StudentId",
                table: "ParentWards",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditWallets_StudentId",
                table: "CreditWallets",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditWallets_Students_StudentId",
                table: "CreditWallets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
