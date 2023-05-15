
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    
    public class PgApplicant
    {
        public int? id { get; set; }
        public string? UserId { get; set; }
        public string? ApplicantionId { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? OtherName { get; set; }
        public string? Sex { get; set; }
        public string? DOB { get; set; }
        public string? MaritalStatus { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? ContactAddress { get; set; }
        public string? PermanentHomeAddress { get; set; }
        [ForeignKey("Countries")]
        public int? Nationality { get; set; }
        public Countries? Countries { get; set; }
        [ForeignKey("States")]
        public int? StateOfOrigin { get; set; }
        public States? States { get; set; }
        [ForeignKey("Lgas")]
        public int? LGA { get; set; }
        public Lga? Lgas { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PrimarySchool { get; set; }
        public string? SecondarySchool { get; set; }
        
        //Second Page
        /// <summary>

        public string? ProgramApplyingFor { get; set; }
        public string? PreviousInstitution { get; set; }
        public string? CurrentQualification { get; set; }
        public string? ClassOfDegree { get; set; }
        public DateTime? YearGraduated { get; set; }
        public string? PercentageOfResult { get; set; }
        public string? AreaOfSpecialization { get; set; }
        public string? EmploymentHistory { get; set; }
        public string? ResearchExperience { get; set; }

        //Third Page
        /// <summary>
        /// //SSCE Details
        /// </summary>
        public int? NoOfSittings { get; set; }
        public string? Ssce1Type { get; set; }
        public string? Ssce1Year { get; set; }
        public string? Ssce1Number { get; set; }
        public string? Ssce1Subject1 { get; set; }
        public string? Ssce1Subject2 { get; set; }
        public string? Ssce1Subject3 { get; set; }
        public string? Ssce1Subject4 { get; set; }
        public string? Ssce1Subject5 { get; set; }
        public string? Ssce1Subject6 { get; set; }
        public string? Ssce1Subject7 { get; set; }
        public string? Ssce1Subject8 { get; set; }
        public string? Ssce1Subject9 { get; set; }

        //ssce 1 grade
        public int? Ssce1Subject1Grade { get; set; }
        public int? Ssce1Subject2Grade { get; set; }
        public int? Ssce1Subject3Grade { get; set; }
        public int? Ssce1Subject4Grade { get; set; }
        public int? Ssce1Subject5Grade { get; set; }
        public int? Ssce1Subject6Grade { get; set; }
        public int? Ssce1Subject7Grade { get; set; }
        public int? Ssce1Subject8Grade { get; set; }
        public int? Ssce1Subject9Grade { get; set; }

        public string? Ssce2Type { get; set; }
        public string? Ssce2Year { get; set; }
        public string? Ssce2Number { get; set; }
        public string? Ssce2Subject1 { get; set; }
        public string? Ssce2Subject2 { get; set; }
        public string? Ssce2Subject3 { get; set; }
        public string? Ssce2Subject4 { get; set; }
        public string? Ssce2Subject5 { get; set; }
        public string? Ssce2Subject6 { get; set; }
        public string? Ssce2Subject7 { get; set; }
        public string? Ssce2Subject8 { get; set; }
        public string? Ssce2Subject9 { get; set; }

        //ssce 1 grade
        public int? Ssce2Subject1Grade { get; set; }
        public int? Ssce2Subject2Grade { get; set; }
        public int? Ssce2Subject3Grade { get; set; }
        public int? Ssce2Subject4Grade { get; set; }
        public int? Ssce2Subject5Grade { get; set; }
        public int? Ssce2Subject6Grade { get; set; }
        public int? Ssce2Subject7Grade { get; set; }
        public int? Ssce2Subject8Grade { get; set; }
        public int? Ssce2Subject9Grade { get; set; }

        //Fourth Page
        /// <summary>
        /// Next of Kin Info
        /// </summary>
        public string? FullNameOfNextOfKin { get; set; }
        public string? AddressOfNextOfKin { get; set; }
        public string? PhoneNumberOfNextOfKin { get; set; }
        public string? AlternatePhoneNumberOfNextOfKin { get; set; }
        public string? EmailOfNextOfKin { get; set; }
        public string? OccupationOfNextOfKin { get; set; }

        //Fifth Page
        /// <summary>
        /// File Uploads
        /// </summary>
        public string? Passport { get; set; }
        public string? HigherDegrees { get; set; }
        public string? Ssce1 { get; set; }
        public string? Ssce2 { get; set; }
        public string? BirthCertificate { get; set; }
        public string? LGAUpload { get; set; }
        public string? NYSC { get; set; }
        public string? Ref1 { get; set; }
        public string? Ref2 { get; set; }
        public string? Ref3 { get; set; }

        //admission Details
        public MainStatus? Status { get; set; }
        public bool? Screened { get; set; }
        [ForeignKey("Levels")]
        public int? LevelAdmittedTo { get; set; }
        public Level? Levels { get; set; }
        [ForeignKey("Departments")]
        public int? AdmittedInto { get; set; }
        public Department? Departments { get; set; }
        [ForeignKey("YearOfAdmissions")]
        public int? YearOfAdmission { get; set; }
        public Session? YearOfAdmissions { get; set; }
        public bool? Cleared { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }


}
