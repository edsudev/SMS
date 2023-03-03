using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class TimeTable
    {
        public int? Id { get; set; }
        public string? TimeTableId { get; set; }
        public string? Venue { get; set; }
        [ForeignKey("Courses")]
        public int? CourseId { get; set; }
        public CourseAllocation? Courses { get; set; }
        [ForeignKey("Staffs")]
        public int? LecturerId { get; set; }
        public CourseAllocation? Staffs { get; set; }
        public string? Time { get; set; }
        public DaysOfTheWeek? Day { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
