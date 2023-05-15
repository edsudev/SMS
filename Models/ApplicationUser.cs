using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Students")]
        public int? StudentsId { get; set; }
        public Student? Students { get; set; }
        [ForeignKey("PgStudents")]
        public int? PgStudent { get; set; }
        public PgStudent? PgStudents { get; set; }
        [ForeignKey("Parents")]
        public int? Parent { get; set; }
        public Parent? Parents { get; set; }
        [ForeignKey("Staffs")]
        public int? StaffId { get; set; }
        public Staff? Staffs { get; set; }
        [ForeignKey("ConversionStudents")]
        public int? ConversionStudent { get; set; }
        public ConversionStudent? ConversionStudents { get; set; }
        // Type 1 is for UG students
        // Type 2 is for Staffs
        // Type 3 is for PG Students
        // Type 4 is for Conversion Students
        // Type 5 is for Parent
        public int? Type { get; set; }
    }
}
