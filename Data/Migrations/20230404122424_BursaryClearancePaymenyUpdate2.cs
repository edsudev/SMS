using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class BursaryClearancePaymenyUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditWallets_UgMainWallets_WalletId",
                table: "CreditWallets");

            migrationBuilder.DropIndex(
                name: "IX_CreditWallets_WalletId",
                table: "CreditWallets");

            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "CreditWallets",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "CreditWallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CreditWallets_WalletId",
                table: "CreditWallets",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditWallets_UgMainWallets_WalletId",
                table: "CreditWallets",
                column: "WalletId",
                principalTable: "UgMainWallets",
                principalColumn: "Id");
        }
    }
}
