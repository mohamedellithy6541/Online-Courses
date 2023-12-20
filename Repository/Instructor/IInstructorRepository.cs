using onlineCourses.Models;

namespace onlineCourses.Repository.InstructorRepo
{
    public interface IInstructorRepository
    {
        void Add(Instructor instructor);
        void delete(string id);
        List<Instructor> getAll();
        Instructor getById(string id);
        int countStudentEnrolled(int courseID);
        void save();
        void update(Instructor instructor);
    }
}