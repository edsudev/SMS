using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class Staff
    {
       //Acad Staff Application
        public int? Id { get; set; }
        public string? StaffId { get; set; }
        public string? Bio { get; set; }
        [ForeignKey("Types")]
        public int? Type { get; set; }
        public TypeOfStaff? Types { get; set; }
        //Intended Position
        [ForeignKey("Positions")]
        public int? Position { get; set; }
        public Position? Positions { get; set; }
        //Intended Faculty
        [ForeignKey("Faculties")]
        public int? FacultyId { get; set; }
        public Faculty? Faculties { get; set; }
        //Intending Department
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public string? SchoolEmail { get; set; }
        public string? DOB { get; set; }
        public Religion? Religion { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Sex { get; set; }
        [ForeignKey("Nationalities")]
        public int? NationalityId { get; set; }
        public Countries? Nationalities { get; set; }
        [ForeignKey("States")]
        public int? StateId { get; set; }
        public States? States { get; set; }
        [ForeignKey("LGAs")]
        public int? LGAId { get; set; }
        public Lga? LGAs { get; set; }
        public string? Phone { get; set; }
        public string? ContactAddress { get; set; }
        public string? HomeAddress { get; set; }
        public string? HighestQualification { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? AreaOfSpecialization { get; set; }
        public bool? WorkedInHigherInstuition { get; set; }
        public string? CurrentPlaceOfWork { get; set; }
        public string? PositionAtCurrentPlaceOfWork { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? CertUpload { get; set; }
        public string? CVUpload { get; set; }
        public string? BirthCert { get; set; }
        public string? Picture { get; set; }
        //Add createdAt and UpdatedAt
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsEmployed { get; set; }
        public string? EmployedBy { get; set; }

        //Publication Profile
        public string? ORCID { get; set; }
        public string? ResearcherId { get; set; }
        public string? GoogleScholar { get; set; }
        public string? ResearchGate { get; set; }
        public string? Academia { get; set; }
        public string? LinkedIn { get; set; }
        public string? Mendeley { get; set; }
        public string? Scopus { get; set; }
    }
}
