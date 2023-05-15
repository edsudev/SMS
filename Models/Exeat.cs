using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;
namespace EDSU_SYSTEM.Models
{
    public class Exeat
    {
        public int? Id { get; set; }
        public string? ExeatId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MatNo { get; set; }
        [ForeignKey("Hostels")]
        public int? Hall { get; set; }
        public Hostel? Hostels { get; set; }
        public string? Department { get; set; }
        public string? Destination { get; set; }
        public int? NoOfDays { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        //Reason for travelling
        public string? Reason { get; set; }
        public string? Phone { get; set; }
        public string? ParentPhone { get; set; }
        public string? AltParentPhone { get; set; }
        //public string? ChiefPortalPhone { get; set; }
        public ChiefPortalStatus? ChiefPortalStatus { get; set; }
        //Can only approve if Day<=3
        public MainStatus? HallMasterStatus { get; set; }
        public MainStatus? Dean { get; set; }
        public MainStatus? GatePass { get; set; }
        //Checks when student is supposed to come back
        public bool? ReturnStatus { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
