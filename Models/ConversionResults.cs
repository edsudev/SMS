using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class ConversionResult
    {
        public int? Id { get; set; }
        public string? CA { get; set; }
        public string? Exam { get; set; }
        public string? Total { get; set; }
        public string? Upgrade { get; set; }
        public string? Grade { get; set; }
        public string? StudentId { get; set; }
        public string? CourseId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
