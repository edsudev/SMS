using System.Runtime.Serialization;

namespace EDSU_SYSTEM.Models
{ 
    public class Enum
    {
        public enum MainStatus
        {
            Pending,
            Approved,
            Declined
        }
        public enum Title
        {
            Mr,
            Mrs,
            Ms,
            Dr,
            Prof
        }
        //Post Graduate Progress Report Ranking
        public enum Ranking
        {
            Pending,
            Satisfied,
            Partially_Satisfied,
            Not_Satisfied
        }
        public enum PgProgram
        {
            PGD,
            MSc,
            MPhil,
            MPhil_PhD,
            PhD
        }
        public enum WorksStatus
        {
            Pending,
            In_Progress,
            Done
        }
        public enum WorksStatusAcknowledgment
        {
            Satisfied,
            Not_Satisfied
        }
        public enum Status
        {
            Active,
            Inactive
        }
        public enum ChiefPortalStatus
        {
            Pending,
            Recommended,
            Reject
        }
        public enum Clearance
        {
            Pending,
            Cleared,
            Reject
        }
        public enum Religion
        {
            Christian,
            Muslim,
            Others
        }
        public enum TransactionType
        {
            Credit,
            Debit
        }
        public enum AdmissionType
        {
            PUTME,
            Transfer,
            DE
        }
        public enum PgProgramApplicant
        {
            HND,
            BSc,
            PGD,
            MSc,
            MPhil,
            MPhil_PhD

        }
        public enum VacancyType
        {
            Academic,
            [EnumMember(Value = "Non Academic")]
            NonAcademic
        }
        public enum CourseType
        {
            C,
            E,
            R
        }
        public enum MailStatus
        {
            Through1,
            Through2,
            Through3,
            Final
        } 
        public enum DaysOfTheWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday
        }
        public enum MaritalStatus
        {
            Single,
            Married,
            Divorced,
            Widowed
        }
        public enum ClearanceRemark
        {
            Pending,
            Partial_Cleared,
            Fully_Cleared,
            Scholarship_Cleared,
            Temp_Pass,
            Not_Cleared

        }
    }
}
