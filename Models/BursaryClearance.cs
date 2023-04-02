using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;
namespace EDSU_SYSTEM.Models
{
    public class BursaryClearance
    {
        public int? Id { get; set; }
        public string? ClearanceId { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public Student? Students { get; set; }
        [ForeignKey("Payments")]
        public int? PaymentId { get; set; }
        public Payment? Payments { get; set; }
        [ForeignKey("Hostels")]
        public int? HostelId { get; set; }
        public HostelPayment? Hostels { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public MainStatus? Status { get; set; }
        public Session? Sessions { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
