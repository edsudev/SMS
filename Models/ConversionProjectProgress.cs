using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class ConversionProjectProgress
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public string? Title { get; set; }
        [ForeignKey("Programs")]
        public int? Program { get; set; }
        public ConversionProgram? Programs { get; set; }
        public string? FileName { get; set; }
        [ForeignKey("Supervisors")]
        public int? SupervisorId { get; set; }
        public ConversionStudentSupervisor? Supervisors { get; set; }
        public ConversionStudentSupervisor? Students { get; set; }
        public Ranking? Ranking { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
