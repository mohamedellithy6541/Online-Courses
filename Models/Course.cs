using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName ="Money")]
        public decimal price { get; set; }
        public int Duration { get; set; }
        public int Grade { get; set; }  //not displayed
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Instructor")]
        public string? ins_id { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<Student_Course>? Student_Course { get; set; }
        public virtual ICollection<Lecture>? Course_Lecture { get; set; }
        public virtual Exam? Exam { get; set; }

        [ForeignKey("Category")]
        public int? cat_id { get; set; }
        public virtual Category? Category { get; set; }



    }
}
