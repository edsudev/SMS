using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class PgCourse
    {
        public int? Id { get; set; }
        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string? Title { get; set; }
        public string? Code { get; set; }
        [ForeignKey("Levels")]
        public int? Level { get; set; }
        public Level? Levels { get; set; }
        [ForeignKey("Semesters")]
        public int? Semester { get; set; }
        public Semester? Semesters { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        public PgProgram? Programme { get; set; }
        [ForeignKey("Programme")]
        public int? ProgrammeId { get; set; }

        public int? CreditUnit { get; set; }
        public CourseType? Status { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}
