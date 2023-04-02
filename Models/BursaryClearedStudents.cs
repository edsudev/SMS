using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class BursaryClearedStudents
    {
        public int? Id { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public Student? Students { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        public ClearanceRemark Remark { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
