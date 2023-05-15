using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;
namespace EDSU_SYSTEM.Models
{
    public class OfflinePaymentClearance
    {
        public int? Id { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public Student? Students { get; set; }
        public string? Mode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public MainStatus? Status { get; set; }
        public string? OrderId { get; set; }
        public Session? Sessions { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
