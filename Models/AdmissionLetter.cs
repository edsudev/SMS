using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class AdmissionLetter
    {
        public int? Id { get; set; }
        public string? Header { get; set; }
        public string? Body { get; set; }
        public string? Var1 { get; set; }
        public string? Var2 { get; set; }
        public string? Var3 { get; set; }
        public string? Note { get; set; }
        public string? Registrar { get; set; }
       
        public string? Ref { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
        [ForeignKey("Faculties")]
        public int? FacultyId { get; set; }
        public Faculty? Faculties { get; set; }
    }
}
