namespace EDSU_SYSTEM.Models
{
    public class Event
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? CoverImg { get; set; }
        public string? Images { get; set; }
        public string? EventID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
