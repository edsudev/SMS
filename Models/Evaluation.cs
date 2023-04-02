using System.ComponentModel.DataAnnotations.Schema;

namespace EDSU_SYSTEM.Models
{
    public class Evaluation
    {
        public int? Id { get; set; }
        public string? EvaluationId { get; set; }
        public EvaluationGrade? Grades1 { get; set; }
        [ForeignKey("Grades1")]
        public int? Grade1 { get; set; }
        public EvaluationGrade? Grades2 { get; set; }
        [ForeignKey("Grades2")]
        public int? Grade2 { get; set; }
        public EvaluationGrade? Grades3 { get; set; }
        [ForeignKey("Grades3")]
        public int? Grade3 { get; set; }
        public EvaluationGrade? Grades4 { get; set; }
        [ForeignKey("Grades4")]
        public int? Grade4 { get; set; }
        public EvaluationGrade? Grades5 { get; set; }
        [ForeignKey("Grades5")]
        public int? Grade5 { get; set; }
        public EvaluationGrade? Grades6 { get; set; }
        [ForeignKey("Grades6")]
        public int? Grade6 { get; set; }
        public EvaluationGrade? Grades7 { get; set; }
        [ForeignKey("Grades7")]
        public int? Grade7 { get; set; }
        public EvaluationGrade? Grades8 { get; set; }
        [ForeignKey("Grades8")]
        public int? Grade8 { get; set; }
        public EvaluationGrade? Grades9 { get; set; }
        [ForeignKey("Grades9")]
        public int? Grade9 { get; set; }
        public EvaluationGrade? Grades10 { get; set; }
        [ForeignKey("Grades10")]
        public int? Grade10 { get; set; }
        public EvaluationGrade? Grades11 { get; set; }
        [ForeignKey("Grades11")]
        public int? Grade11 { get; set; }
        public EvaluationGrade? Grades12 { get; set; }
        [ForeignKey("Grades12")]
        public int? Grade12 { get; set; }
        public EvaluationGrade? Grades13 { get; set; }
        [ForeignKey("Grades13")]
        public int? Grade13 { get; set; }
        public EvaluationGrade? Grades14 { get; set; }
        [ForeignKey("Grades14")]
        public int? Grade14 { get; set; }
        public EvaluationGrade? Grades15 { get; set; }
        [ForeignKey("Grades15")]
        public int? Grade15 { get; set; }
        public EvaluationGrade? Grades16 { get; set; }
        [ForeignKey("Grades16")]
        public int? Grade16 { get; set; }
        public EvaluationGrade? Grades17 { get; set; }
        [ForeignKey("Grades17")]
        public int? Grade17 { get; set; }
        public EvaluationGrade? Grades18 { get; set; }
        [ForeignKey("Grades18")]
        public int? Grade18 { get; set; }
        public EvaluationGrade? Grades19 { get; set; }
        [ForeignKey("Grades19")]
        public int? Grade19 { get; set; }
        public EvaluationGrade? Grades20 { get; set; }
        [ForeignKey("Grades20")]
        public int? Grade20 { get; set; }
        
        public Staff? Lecturers { get; set; }
        [ForeignKey("Lecturers")]
        public int? LecturerId { get; set; }
        public Course? Courses { get; set; }
        [ForeignKey("Courses")]
        public int? CourseId { get; set; }
        public Semester? Semesters { get; set; }
        [ForeignKey("Semesters")]
        public int? SemesterId { get; set; } 
        public Session? Sessions { get; set; }
        [ForeignKey("Students")]
        public int? StudentId { get; set; } 
        public Student? Students { get; set; }
        [ForeignKey("Sessions")]
        public int? SessionId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
