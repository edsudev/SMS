namespace EDSU_SYSTEM.Models
{
    public class ToDo
    {
        public int? Id { get; set; }
        public string? ToDoId { get; set; }
        public string? Task { get; set; }
        public string? Owner { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public bool? IsImportant { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
