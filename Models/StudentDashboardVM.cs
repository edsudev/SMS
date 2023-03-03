namespace EDSU_SYSTEM.Models
{
    public class StudentDashboardVM
    {
        public UgMainWallet? MainWallet { get; set; }
        public List<CourseRegistration>? Courses { get; set; }
        public List<TimeTable>? TimeTables { get; set; }
    }
}
