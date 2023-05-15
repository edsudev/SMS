using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class ParentWard
    {
        public int? Id { get; set; }
        [ForeignKey("students")]
        public int? StudentId { get; set; }
        public Student? Students { get; set; }
        [ForeignKey("Parents")]
        public int? ParentId { get; set; }
        public Parent? Parents { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
