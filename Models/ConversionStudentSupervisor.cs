using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class ConversionStudentSupervisor
    {
        public int? Id { get; set; }
        [ForeignKey("Students")]
        public int? Student { get; set; }
        public ConversionStudent? Students { get; set; }
        [ForeignKey("Lecturers")]
        public int? Supervisor { get; set; }
        public Staff? Lecturers { get; set; }
        public string? SupervisorRole { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
