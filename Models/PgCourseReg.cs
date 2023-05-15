using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class PgCourseReg
    {
        public int? Id { get; set; }
        public string? CourseRegId { get; set; }

        [ForeignKey("Courses")]
        public int? CourseId { get; set; }
        public PgCourse? Courses { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public PgStudent? Students { get; set; }
        public string? Comment { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        public MainStatus? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DateApproved { get; set; }
    }
}
