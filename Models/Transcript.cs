
using System.ComponentModel.DataAnnotations;
using static EDSU_SYSTEM.Models.Enum;
namespace EDSU_SYSTEM.Models
{
    public class Transcript
    {
        public int? Id { get; set; }
        public string? TranscriptId { get; set; }
        public Title? Title { get; set; }
        public string? Surname { get; set; }
        public string? Firstname { get; set; }
        public string? Othername { get; set; }
        public string? MatNo { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public MainStatus? Status { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }

        //Program details
        public string? Programme { get; set; }
        public DateTime? GraduationDate { get; set; }
        public bool? AppliedBefore { get; set; }
       
        public DateTime? IfYes { get; set; }

        //Destination Details
        public string? DestinationName { get; set; }
        public string? DestinationEmail { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? TranscriptLabel { get; set; }

        //Supporting Documents
        public string? Receipt { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? NotificationOfResult { get; set; }
        public string? Others { get; set; }
        public string? UserId { get; set; }
    }
}
