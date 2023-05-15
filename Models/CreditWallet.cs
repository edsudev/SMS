using System.ComponentModel.DataAnnotations.Schema;
namespace EDSU_SYSTEM.Models
{
    public class CreditWallet
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string? Wallet { get; set; }
        public string? UTME { get; set; }
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
