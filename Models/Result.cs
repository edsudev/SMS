using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class Result
    {
        public int? Id { get; set; }
        public string? ResultId { get; set; }
        public double? CA { get; set; }
        public double? Exam { get; set; }
        public double? Total { get; set; }
        public double? Upgrade { get; set; }
        public string? Grade { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        public string? StudentId { get; set; }
        public string? CourseId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
