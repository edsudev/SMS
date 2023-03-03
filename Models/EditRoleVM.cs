using System.ComponentModel;
namespace EDSU_SYSTEM.Models
{
    public class EditRoleVM
    {
        public EditRoleVM()
        {
            Users = new List<string>();
        }
        public string? Id { get; set; }
        public string? RoleName { get; set; }
        public List<string>? Users { get; set; }
    }
}
