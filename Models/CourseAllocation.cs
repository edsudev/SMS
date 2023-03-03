using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class CourseAllocation
    {
        public int? Id { get; set; }
        public string? CourseAllocationId { get; set; }
        [ForeignKey("Courses")]
        public int? CourseId { get; set; }
        public Course? Courses { get; set; }
        [ForeignKey("Staff")]
        public int? LecturerId { get; set; }
        public Staff? Staff { get; set; }
        public string? CourseLecturerRole { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
