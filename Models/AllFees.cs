using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class AllFees
    {
        public int? Id { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
        [ForeignKey("Levels")]
        public int? LevelId { get; set; }
        public Level? Levels { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Tuition { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        //Only for transfer students in certain departments
        public decimal? Tuition2 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? LMS { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? EDHIS { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SRC { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Acceptance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Medical { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Causion { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Sports { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ICT { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Library { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? IdentityCard { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Registration { get; set; }
    }
}
