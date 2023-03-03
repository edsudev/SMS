using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class CreditUnit
    {
        public int? Id { get; set; }
        public int? Max { get; set; }
        public int? Min { get; set; }
        [ForeignKey("Levels")]
        public int? LevelId { get; set; }
        public Level? Levels { get; set; }
        [ForeignKey("Semesters")]
        public int? SemesterId { get; set; }
        public Semester? Semesters { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
    }
}
