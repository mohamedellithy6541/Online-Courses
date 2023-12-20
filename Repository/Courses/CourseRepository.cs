using onlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace onlineCourses.Repository.Courses
{
    public class CourseRepository : ICourseRepository
    {
        private DBContext dBContext;

        public CourseRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public void AddCourse(Course course)
        {
            dBContext.Add(course);
        }

        public void DeleteCourse(Course course)
        {
            dBContext.Remove(course);
        }
        public List<Course> getAllCourses()
        {
            return  dBContext.Courses.Where(c=>!c.IsDeleted)
                .Include(i=>i.Instructor)
                .Include(c => c.Category)
                .ToList();
        }

        public Course getCourseByID(int ID)
        {
            return  dBContext.Courses.Where(c => !c.IsDeleted).Include(i => i.Instructor).Where(c => c.Id== ID).FirstOrDefault();
        }
		public List<Course> getCourseByCategotyID(int CatID)
		{
			return dBContext.Courses.Where(c => !c.IsDeleted).Include(i => i.Instructor).
				Where(c => c.cat_id == CatID).ToList();
		}
        public List<Course> GetCoursesByInstructorId(string instructorID)
        {
            return dBContext.Courses.Where(c => !c.IsDeleted & c.ins_id == instructorID).ToList();
        }

        public int saveDB()
        {
            return dBContext.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            dBContext.Update(course);
        }

       
    }
}
