using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;
namespace EDSU_SYSTEM.Models
{
    public class VacancyList
    {
        public int? Id { get; set; }
        public VacancyType? Type { get; set; }
        [ForeignKey("Positions")]
        public int PositionId { get; set; }
        public Position? Positions { get; set; }
        [ForeignKey("Units")]
        public int UnitId { get; set; }
        public UnitName? Units { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public bool IsActive { get; set; }
       
    }
}
