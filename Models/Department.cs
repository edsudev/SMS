using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    [Table("Departments")]

    public class Department
    {
        public int? Id { get; set; }

        public string? Name { get; set; }
        public string? ShortCode { get; set; }
        public bool Active { get; set; }
        [ForeignKey("Faculties")]
        public int? FacultyId { get; set; }
        public Faculty? Faculties { get; set; }
    }
}