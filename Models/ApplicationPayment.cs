using System.ComponentModel.DataAnnotations.Schema;
namespace EDSU_SYSTEM.Models
{
    public class ApplicationPayment
    {
        public int Id { get; set; }
        public string? Ref { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string? Email { get; set; }
        //UG, PG, CONVERSION, JUPEB
        public string? Type { get; set; }
        //PUTME, TRANSFER, DE, JUPEB
        public string? ModeOfAdmission { get; set; }
        public string? Phone { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Status { get; set; }
        public Session? Sessions { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
    }
}
