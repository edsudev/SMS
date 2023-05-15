using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDSU_SYSTEM.Data.Migrations
{
    public partial class BursaryClearancePaymenyUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "Wallet",
               table: "CreditWallets",
               type: "longtext",
               nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wallet",
                table: "CreditWallets",
                newName: "WalletId");
        }
    }
}
