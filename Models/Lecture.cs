using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onlineCourses.Models
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }

		[MinLength(3, ErrorMessage = "Name must be more than 2 letter")]
		[MaxLength(25)]
		public string Name { get; set; }

		[MaxLength(250)]

		public string Description { get; set; }
		[Range(minimum: 2, maximum: 6, ErrorMessage = "Range Must be More than 1 and less than or equal 6 ")]

		public float Duration { get; set; }
        
        [ForeignKey("Instructor")]
        public string ins_id{ get; set; }
        public Instructor? Instructor { get; set; }

        [ForeignKey("course")]
        public int crs_id {  get; set; }
        public Course? course { get; set; }

    }
}
