using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class bursaryclearancestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BursaryClearances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BursaryClearancesFreshers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClearanceId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryClearancesFreshers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BursaryClearancesFreshers_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BursaryClearancesFreshers_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BursaryClearancesFreshers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearancesFreshers_PaymentId",
                table: "BursaryClearancesFreshers",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearancesFreshers_SessionId",
                table: "BursaryClearancesFreshers",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearancesFreshers_StudentId",
                table: "BursaryClearancesFreshers",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BursaryClearancesFreshers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BursaryClearances");
        }
    }
}
