using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class bursaryclearanceHostelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostelId",
                table: "BursaryClearancesFreshers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HostelId",
                table: "BursaryClearances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearancesFreshers_HostelId",
                table: "BursaryClearancesFreshers",
                column: "HostelId");

            migrationBuilder.CreateIndex(
                name: "IX_BursaryClearances_HostelId",
                table: "BursaryClearances",
                column: "HostelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BursaryClearances_HostelPayments_HostelId",
                table: "BursaryClearances",
                column: "HostelId",
                principalTable: "HostelPayments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BursaryClearancesFreshers_HostelPayments_HostelId",
                table: "BursaryClearancesFreshers",
                column: "HostelId",
                principalTable: "HostelPayments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BursaryClearances_HostelPayments_HostelId",
                table: "BursaryClearances");

            migrationBuilder.DropForeignKey(
                name: "FK_BursaryClearancesFreshers_HostelPayments_HostelId",
                table: "BursaryClearancesFreshers");

            migrationBuilder.DropIndex(
                name: "IX_BursaryClearancesFreshers_HostelId",
                table: "BursaryClearancesFreshers");

            migrationBuilder.DropIndex(
                name: "IX_BursaryClearances_HostelId",
                table: "BursaryClearances");

            migrationBuilder.DropColumn(
                name: "HostelId",
                table: "BursaryClearancesFreshers");

            migrationBuilder.DropColumn(
                name: "HostelId",
                table: "BursaryClearances");
        }
    }
}
