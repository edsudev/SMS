namespace EDSU_SYSTEM.Models
{
    public class EvaluationVM
    {
        //public EvaluationGrade? Grade { get; set; }
        public List<EvaluationGrade>? Gradee { get; set; }
        public List<EvaluationQuestion>? Question { get; set; }
        public Evaluation? Evaluations { get; set; }
    }
}