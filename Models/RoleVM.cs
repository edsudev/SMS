using System.ComponentModel.DataAnnotations;

namespace EDSU_SYSTEM.Models
{
    public class RoleVM
    {
        [Required]
        public string? Name { get; set; }
    }
}
