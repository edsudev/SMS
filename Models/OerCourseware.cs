namespace EDSU_SYSTEM.Models
{ 
    public class OerCourseware
    {
        public int? Id { get; set; }
        public string? CoursewareId { get; set; }
        public string? LectureName { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTitle { get; set; }
        public int? Level { get; set; }
        public string? Upload { get; set; }
        public Department? Department { get; set; }
        public Faculty? Faculty { get; set; }
        public string? Synopsis { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
    }
}
