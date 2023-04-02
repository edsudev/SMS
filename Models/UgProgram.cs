using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{ 
    public class UgProgram
    {
        public int? Id { get; set; }
        public string? NameOfProgram { get; set; }
        public bool Active { get; set; }
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public string? Link { get; set; }
    }
}
