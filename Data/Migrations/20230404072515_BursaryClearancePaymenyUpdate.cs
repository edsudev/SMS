using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class BursaryClearancePaymenyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParentWards_ParentId",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_BursaryClearedStudents_StudentId",
                table: "BursaryClearedStudents");

            migrationBuilder.CreateTable(
                name: "AllFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    LevelId = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    Tuition = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tuition2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LMS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EDHIS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SRC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Acceptance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Causion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sports = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ICT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Library = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdentityCard = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Registration = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllFees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllFees_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllFees_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CreditWallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditWallets_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreditWallets_UgMainWallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "UgMainWallets",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_ParentId_StudentId",
                table: "ParentWards",
                columns: new[] { "ParentId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearedStudents_StudentId_SessionId",
                table: "BursaryClearedStudents",
                columns: new[] { "StudentId", "SessionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllFees_DepartmentId",
                table: "AllFees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AllFees_LevelId",
                table: "AllFees",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AllFees_SessionId",
                table: "AllFees",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditWallets_StudentId",
                table: "CreditWallets",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditWallets_WalletId",
                table: "CreditWallets",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllFees");

            migrationBuilder.DropTable(
                name: "CreditWallets");

            migrationBuilder.DropIndex(
                name: "IX_ParentWards_ParentId_StudentId",
                table: "ParentWards");

            migrationBuilder.DropIndex(
                name: "IX_BursaryClearedStudents_StudentId_SessionId",
                table: "BursaryClearedStudents");

            migrationBuilder.CreateIndex(
                name: "IX_ParentWards_ParentId",
                table: "ParentWards",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearedStudents_StudentId",
                table: "BursaryClearedStudents",
                column: "StudentId");
        }
    }
}
