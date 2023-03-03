using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class ENTVacancy
    {
        public int? Id { get; set; }
        public string? ApplicantId { get; set; }
        public string? Type { get; set; }
        public string? TellerNo { get; set; }
        public string? FullName { get; set; }
        public string? BusinessName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public MainStatus? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
