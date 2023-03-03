using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class UgProgress
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public string? Title { get; set; }
        [ForeignKey("Programs")]
        public int? Program { get; set; }
        public UgProgram? Programs { get; set; }
        public string? FileName { get; set; }
        [ForeignKey("Supervisors")]
        public int? SupervisorId { get; set; }
        public UgStudentSupervisor? Supervisors { get; set; }
        public UgStudentSupervisor? Students { get; set; }
        public Ranking? Ranking { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
