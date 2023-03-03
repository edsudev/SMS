namespace EDSU_SYSTEM.Models
{
    public class OerConferencePaper
    {
        public int? Id { get; set; }
        public string? ConferencePaperId { get; set; }
        public string? Title { get; set; }
        public string? Authors { get; set; }
        public string? Keywords { get; set; }
        public string? Abstract { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? PublisherName { get; set; }
        public string? NoOfPages { get; set; }
        public Department? Department { get; set; }
        public string? Upload { get; set; }
        public bool? IsPublished { get; set; }
        public string? Volume { get; set; }
        public string? Issue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UploadedBy { get; set; }

    }
}
