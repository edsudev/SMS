using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    [Table("Faculties")]
    public class Faculty 
    {
        public int? Id { get; set; }

        public string? Name { get; set; }
        public string? ShortCode { get; set; }

        
       
    }
}
