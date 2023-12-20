using onlineCourses.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace onlineCourses.Data.ViewModels.CourseViewModels
{
    public class AddCourseModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 10)]
        public string Title { get; set; }
        [StringLength(30, MinimumLength = 20)]
        public string Description { get; set; }
        [Required]
        [Range(300, 2000)]
        public decimal price { get; set; }
        [Required]
        [Range(2, 30)]
        public int Duration { get; set; }
        [Required]
        [Range(50, 200)]
        public int Grade { get; set; }
        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "you should enter a valid category")]
        public int categoryID { get; set; }

    }
}
