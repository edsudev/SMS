using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    [Table("SubDepartments")]

    public class SubDepartment
    {
        public int? Id { get; set; }

        public string? Name { get; set; }
        public string? ShortCode { get; set; }
        public bool? Active { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
    }
}
