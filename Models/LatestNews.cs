namespace EDSU_SYSTEM.Models
{
    public class LatestNews
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
