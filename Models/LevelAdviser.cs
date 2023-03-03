using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class LevelAdviser
    {
        public int? Id { get; set; }
        public string? LevelAdviserId { get; set; }
        [ForeignKey("Staffs")]
        public int? StaffId { get; set; }
        public Staff? Staffs { get; set; }
        [ForeignKey("Levels")]
        public int? LevelId { get; set; }
        public Level? Levels { get; set; }
        public Status? Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
