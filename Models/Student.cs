using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class Student
    {
        public int? Id { get; set; }
        [ForeignKey("Applicants")]
        public int? ApplicantId { get; set; }
        public Applicant? Applicants { get; set; }
        public string? StudentId { get; set; }
        //Student info
        public string? Picture { get; set; }
        public string? Fullname { get; set; }
        public string? Sex { get; set; }
        public string? DOB { get; set; }
        public Religion? Religion { get; set; }
        public string? Phone { get; set; }
        public string? AltPhoneNumber { get; set; }
        public string? Email { get; set; }
        [ForeignKey("Nationalities")]
        public int? NationalityId { get; set; }
        public Countries? Nationalities { get; set; }
        [ForeignKey("States")]
        public int? StateOfOriginId { get; set; }
        public States? States { get; set; }
        [ForeignKey("LGAs")]
        public int? LGAId { get; set; }
        public Lga? LGAs { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? PermanentHomeAddress { get; set; }
        public string? ContactAddress { get; set; }
        public string? MaritalStatus { get; set; }
        //Parent/guardian Info
        public string? ParentName { get; set; }
        public string? ParentOccupation { get; set; }
        public string? ParentPhone { get; set; }
        public string? ParentAltPhone { get; set; }
        public string? ParentEmail { get; set; }
        public string? ParentAddress { get; set; }
        //Academic Info
        public string? SchoolEmailAddress { get; set; }
        public string? UTMENumber { get; set; }
        public string? MatNumber { get; set; }
        [ForeignKey("Faculties")]
        public int? Faculty { get; set; }
        public Faculty? Faculties { get; set; }
        [ForeignKey("Levels")]
        public int? Level { get; set; }
        public Level? Levels { get; set; }
        public string? ModeOfAdmission { get; set; }
        [ForeignKey("Programs")]
        public int? ProgrameId { get; set; }
        public UgProgram? Programs { get; set; }
        [ForeignKey("YearOfAdmissions")]
        public int? YearOfAdmission { get; set; }
        [ForeignKey("Departments")]
        public int? Department { get; set; }
        public Department? Departments { get; set; }
        [ForeignKey("Sessions")]
        public int? CurrentSession { get; set; }
        public Session? Sessions { get; set; }
        public Session? YearOfAdmissions { get; set; }
        //Clearance Info
        public DateTime? CreatedAt { get; set; }
        public bool? Cleared { get; set; }
        [ForeignKey("Staffs")]
        public int? ClearedBy { get; set; }
        public Staff? Staffs { get; set; }

        //This checks if the student is still our student
        public bool? IsStillAStudent { get; set; }
        //Checks if you're still a student, graduated or rusticated
        // 1 student
        // 2 Graduate
        // 3 Rusticated/Expelled
        public int? StudentStatus { get; set; }
    }
}
