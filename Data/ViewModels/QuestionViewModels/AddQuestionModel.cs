using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Data.ViewModels.QuestionViewModels
{
    public class AddQuestionModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Required]
        public string Answers { get; set; }
        [Required]
        public string ModelAnswer { get; set; }
        [Required]
        public int? exam_id { get; set; }
        [Required]
        public int? qust_type { get; set; }

    }
}
