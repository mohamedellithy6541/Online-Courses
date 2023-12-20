using System.ComponentModel.DataAnnotations;

namespace onlineCourses.Data.ViewModels.ExamViewModels
{
	public class AddExamModel
	{
        [Required]
        [RegularExpression("^\\w{5,}$")]
        public string Name { get; set; }
        [Required]
        [Range(minimum:30.0,maximum:180.0)]
        public float Duration { get; set; }
        [Required]
        public int Course_ID { get; set; }

    }
}
