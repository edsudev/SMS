namespace EDSU_SYSTEM.Models
{
    public class OerTextbook
    {
        public int? Id { get; set; }
        public string? BookId { get; set; }
        public string? Text { get; set; }
        public string? Author { get; set; }
        public string? Contributors { get; set; }
        public string? Publisher { get; set; }
        public string? Summary { get; set; }
        public string? Edition { get; set; }
        public string? ISBN { get; set; }
        public string? PublicationYear { get; set; }
        public int? NoOfPages { get; set; }
        public string? FieldOfStudy { get; set; }
        public Department? Department { get; set; }
        public string? Upload { get; set; }
    }
}
