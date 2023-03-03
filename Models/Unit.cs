using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class Unit
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        [ForeignKey("Positions")]
        public int? PositionId { get; set; }
        public Position? Positions { get; set; }
        [ForeignKey("Units")]
        public int? UnitId { get; set; }
        public UnitName? Units { get; set; }
        public string? Pic { get; set; }
        public string? Phone { get; set; }
        public  string? Message { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDirector { get; set; }
        public bool? IsActing { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
