namespace EDSU_SYSTEM.Models
{
    public class OerStudentProject
    {
        public int? Id { get; set; }
        public string? ProjectId { get; set; }
        public string? Title { get; set; }
        public string? NameOfStudent { get; set; }
        public string? MatNo { get; set; }
        public string? SessionOfAdmission { get; set; }
        public Faculty? Faculty { get; set; }
        public Department? Department { get; set; }
        public string? ModeOfStudy { get; set; }
        public string? Abstract { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? Upload { get; set; }
        public string? Synopsis { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
