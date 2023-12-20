using onlineCourses.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace onlineCourses.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be atleast 3 in length and maximum 50")]
        [Unique]
        public string Name { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }

    }
}
