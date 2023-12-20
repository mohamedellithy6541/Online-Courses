using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Answers { get; set; }
        public string ModelAnswer { get; set; }
        
        [ForeignKey("Exam")]
        public int? exam_id { get; set; }
        public virtual Exam? Exam{ get; set; }
        
        [ForeignKey("QuestionType")]
        public int? qust_type { get; set; }
        public virtual QuestionType? QuestionType { get; set; }
    }
}
