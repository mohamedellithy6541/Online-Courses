using onlineCourses.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace onlineCourses.Models
{
    public class QuestionType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Name must be atleast 3 and maximum 250 in length")]
        [Unique]
        public string Name { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }

    }
}
