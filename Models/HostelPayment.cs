using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class HostelPayment
    {
        public int? Id { get; set; }
        public string? Ref { get; set; }
        public string? Email { get; set; }
        public string? Mode { get; set; }
        public string? ReceiptNo { get; set; }
        [ForeignKey("Wallets")]
        public int? WalletId { get; set; }
        public UgSubWallet? Wallets { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public double? Amount { get; set; }
        
        [ForeignKey("HostelFees")]
        public int? HostelType { get; set; }
        public Hostel? HostelFees { get; set; }
        public string? Status { get; set; }
        public Session? Sessions { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        
    }
}
