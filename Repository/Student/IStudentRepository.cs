using onlineCourses.Models;

namespace onlineCourses.Repository
{
    public interface IStudentRepository
    {
        void Enroll(string studentId, int? courseid);
        IEnumerable<Student> GetAll();

        Student GetStudent(string name);

       

    }
}
