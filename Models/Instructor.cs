using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Models
{
    public class Instructor : AppUser
    {
        public virtual ICollection<Course>? Courses { get; set; }
        public virtual ICollection<Lecture>? Lectures { get; set; }
    }
}

