using onlineCourses.Models;

namespace onlineCourses.Repository.Courses
{
    public interface ICourseRepository
    {
        List<Course> getAllCourses ();
        Course getCourseByID (int ID);
        List<Course> getCourseByCategotyID(int CatID);
        public List<Course> GetCoursesByInstructorId(string instructorID);
        void UpdateCourse (Course course);
        void DeleteCourse (Course course);
        void AddCourse (Course course);
        int saveDB ();
    }
}
