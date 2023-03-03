using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class Result
    {
        public int? Id { get; set; }
        public string? ResultId { get; set; }
        public string? CA { get; set; }
        public string? Exam { get; set; }
        public string? Total { get; set; }
        public string? Upgrade { get; set; }
        public string? Grade { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        public string? StudentId { get; set; }
        public string? CourseId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
