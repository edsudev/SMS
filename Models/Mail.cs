using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class Mail
    {
        public int? Id { get; set; }

        public string? MailId { get; set; }    
        public string? From { get; set; }
        [ForeignKey("StaffTo")]
        public int? To { get; set; }
        [ForeignKey("StaffThrough")]
        public int? Through { get; set; }
        [ForeignKey("StaffThrough2")]
        public int? Through2 { get; set; }
        [ForeignKey("StaffThrough3")]
        public int? Through3 { get; set; }
        public string? Subject { get; set; }
        public string? File { get; set; }
        //When forwarded, this status changes and that is the only time the "To" can see it
        public MailStatus? Status { get; set; }
        public bool? Draft { get; set; }
        public bool? IsImportant { get; set; }
        public bool? Trash { get; set; }
        
        public string? Message { get; set; }
        //The comment come from the "Through"
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateDate { get; set; }

        //Foreign Keys
        public Staff? StaffThrough { get; set; }
        public Staff? StaffThrough2 { get; set; }
        public Staff? StaffThrough3 { get; set; }
        public Staff? StaffTo { get; set; }
    }
}
