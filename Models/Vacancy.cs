using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class Vacancy
    {
        //Acad Staff Application
        public int? Id { get; set; }
        public string? ApplicantId { get; set; }
        public VacancyType? Type { get; set; }
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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Religion? Religion { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public DateTime? DOB { get; set; }
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
        public string? HighestQualification { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? AreaOfSpecialization { get; set; }
        public bool? WorkedInHigherInstuition { get; set; }
        public string? CurrentPlaceOfWork { get; set; }
        public string? PositionAtCurrentPlaceOfWork { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? CertUpload { get; set; }
        public string? CVUpload { get; set; }
        public string? Picture { get; set; }
        //Add createdAt and UpdatedAt
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool? IsEmployed { get; set; }
    }
}
