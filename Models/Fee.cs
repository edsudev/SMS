//using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class Fee
    {
        public int? Id { get; set; }
        //Tuition
        public string? Faculty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Level1 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Level2 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Level3 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Level4 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Level5 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Level6 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Pgd { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Msc { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Phd { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }

        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public Session? Sessions { get; set; }

    }
}
