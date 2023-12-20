using onlineCourses.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Models
{
    public class Student : AppUser
    {
        public virtual ICollection<Student_Course>?Student_Course { get; set; }

    }
}
