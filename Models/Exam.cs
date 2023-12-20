using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Duration{ get; set; }
        
        [ForeignKey("Course")]
        public int? crs_id { get; set; }
        public virtual Course? Course { get; set; }
        public virtual ICollection<Question>? Questions{ get; set; }

    }
}
