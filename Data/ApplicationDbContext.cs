using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using System;
using Microsoft.AspNetCore.Identity;

namespace EDSU_SYSTEM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //A student shouldn't register one course twice
            modelBuilder.Entity<CourseRegistration>()
                .HasIndex(c => new { c.StudentId, c.CourseId })
                .IsUnique(true);
            modelBuilder.Entity<ConversionCourseReg>()
                .HasIndex(c => new { c.StudentId, c.CourseId })
                .IsUnique(true);
            modelBuilder.Entity<PgCourseReg>()
                .HasIndex(c => new { c.StudentId, c.CourseId })
                .IsUnique(true);
            //A student shouldn't evaluate one lecturer on one course twice
            modelBuilder.Entity<Evaluation>()
                .HasIndex(c => new { c.StudentId, c.LecturerId, c.SessionId, c.CourseId })
                .IsUnique(true);
            //One lecturer can't be allocated same course twice
            modelBuilder.Entity<CourseAllocation>()
                .HasIndex(c => new { c.LecturerId, c.CourseId })
                .IsUnique(true);
            modelBuilder.Entity<Result>()
                .HasIndex(c => new { c.StudentId, c.CourseId, c.SessionId })
                .IsUnique(true); 
            modelBuilder.Entity<BursaryClearedStudents>()
                .HasIndex(c => new { c.StudentId, c.SessionId })
                .IsUnique(true);
            modelBuilder.Entity<ParentWard>()
                .HasIndex(c => new { c.StudentId, c.ParentId })
                .IsUnique(true);
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<EDSU_SYSTEM.Models.Applicant>? UgApplicants { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgApplicant>? PgApplicants { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgProgram>? PgPrograms { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Faculty>? Faculties { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Department>? Departments { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Course>? Courses { get; set; }
        public DbSet<EDSU_SYSTEM.Models.SsceSubjects>? SsceSubjects { get; set; }
        public DbSet<EDSU_SYSTEM.Models.SSCEGrade>? SSCEGrades { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Transcript>? Transcripts { get; set; }
        public DbSet<EDSU_SYSTEM.Models.TypeOfStaff>? Types { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgProgress>? PgProgresses { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Work>? Works { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OerCourseware>? OerCoursewares { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OerConferencePaper>? OerConferencePapers { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OerJournalArticle>? OerJournalArticles { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OerStudentProject>? OerStudentProjects { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OerTextbook>? OerTextbooks { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Exeat>? Exeats { get; set; }
        public DbSet<EDSU_SYSTEM.Models.VacationExeat>? VacationExeats { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Student>? Students { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Lga>? Lgas { get; set; }
        public DbSet<EDSU_SYSTEM.Models.States>? States { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Countries>? Countries { get; set; }
        public DbSet<EDSU_SYSTEM.Models.UgSubWallet>? UgSubWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.UgMainWallet>? UgMainWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Session>? Sessions { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Fee>? Fees { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Level>? Levels { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Payment>? Payments { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OtherFees>? OtherFees { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Vacancy>? Vacancies { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ENTVacancy>? ENTVacancies { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Staff>? Staffs { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Hostel>? Hostels { get; set; }
        public DbSet<EDSU_SYSTEM.Models.AdmissionLetter>? AdmissionLetters { get; set; }
        public DbSet<EDSU_SYSTEM.Models.CourseAllocation>? CourseAllocations { get; set; }
        public DbSet<EDSU_SYSTEM.Models.CourseRegistration>? CourseRegistrations { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Semester>? Semesters { get; set; }
        public DbSet<EDSU_SYSTEM.Models.CreditUnit>? CreditUnits { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Mail>? Mails { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ToDo>? Todos { get; set; }
        public DbSet<EDSU_SYSTEM.Models.LevelAdviser>? LevelAdvisers { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Position>? Positions { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgStudent>? PostGraduateStudents { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgStudentSupervisor>? PgStudentsSupervisors { get; set; }
        public DbSet<EDSU_SYSTEM.Models.UnitName>? UnitNames { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Unit>? Units { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgSubWallet>? PgSubWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgMainWallet>? PgMainWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.UgProgram>? Programs { get; set; }
        public DbSet<EDSU_SYSTEM.Models.UgProgress>? UgProgresses { get; set; }
        public DbSet<EDSU_SYSTEM.Models.UgStudentSupervisor>? UgStudentSupervisors { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Result>? Results { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgCourse>? PgCourses { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgResult>? PgResults { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Evaluation>? Evaluations { get; set; }
        public DbSet<EDSU_SYSTEM.Models.EvaluationQuestion>? EvaluationQuestions { get; set; }
        public DbSet<EDSU_SYSTEM.Models.EvaluationGrade>? EvaluationGrades { get; set; }
        public DbSet<EDSU_SYSTEM.Models.TimeTable>? TimeTables { get; set; }
        public DbSet<EDSU_SYSTEM.Models.IctComplaint>? IctComplaints { get; set; }
        public DbSet<EDSU_SYSTEM.Models.BursaryClearance>? BursaryClearances { get; set; }
        public DbSet<EDSU_SYSTEM.Models.BursaryClearedStudents>? BursaryClearedStudents { get; set; }
        public DbSet<EDSU_SYSTEM.Models.BursaryClearanceFresher>? BursaryClearancesFreshers { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Parent>? Parent { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ParentWard>? ParentWards { get; set; }
        public DbSet<EDSU_SYSTEM.Models.SubDepartment>? SubDepartments { get; set; }
        public DbSet<EDSU_SYSTEM.Models.HostelPayment>? HostelPayments { get; set; }
        public DbSet<EDSU_SYSTEM.Models.VacancyList>? VacancyLists { get; set; }
        public DbSet<EDSU_SYSTEM.Models.LatestNews>? LatestNews { get; set; }
        public DbSet<EDSU_SYSTEM.Models.NewsSubcriptionEmail>? NewsSubcriptionEmails { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Slider>? Sliders { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Magazine>? Magazines { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Event>? Events { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Activities>? Activities { get; set; }
        public DbSet<EDSU_SYSTEM.Models.AllFees>? AllFees { get; set; }
        public DbSet<EDSU_SYSTEM.Models.CreditWallet>? CreditWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.OfflinePaymentClearance>? OfflinePaymentClearances { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionApplicant>? ConversionApplicants { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionResult>? ConversionResults { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionProjectProgress>? ConversionProjectProgresses { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionStudent>? ConversionStudents { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionMainWallet>? ConversionMainWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionSubWallet>? ConversionSubWallets { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionStudentSupervisor>? ConversionStudentSupervisors { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionCourse>? ConversionCourses { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionCourseReg>? ConversionCourseRegs { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ConversionProgram>? ConversionPrograms { get; set; }
        public DbSet<EDSU_SYSTEM.Models.PgCourseReg>? PgCourseRegs { get; set; }
        public DbSet<EDSU_SYSTEM.Models.ApplicationPayment>? ApplicationPayments { get; set; }
        public DbSet<EDSU_SYSTEM.Models.Jupeb>? JupebApplicants { get; set; }
        public DbSet<EDSU_SYSTEM.Models.JupebStudent>? JupebStudents { get; set; }


    }
}