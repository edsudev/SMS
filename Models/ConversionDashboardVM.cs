namespace EDSU_SYSTEM.Models
{
    public class ConversionDashboardVM
    {
        public ConversionMainWallet? MainWallet { get; set; }
        public List<ConversionCourseReg>? Courses { get; set; }
        public List<TimeTable>? TimeTables { get; set; }
    }
}
