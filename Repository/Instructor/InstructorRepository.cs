using Microsoft.EntityFrameworkCore;
using onlineCourses.Models;

namespace onlineCourses.Repository.InstructorRepo
{
    public class InstructorRepository : IInstructorRepository
    {
        private DBContext dbContext;
        public InstructorRepository(DBContext db)
        {
            dbContext = db;
        }

        public List<Instructor> getAll()
        {
            return dbContext.Instructors.ToList();
        }

        public Instructor getById(string id)
        {
            return dbContext.Instructors.FirstOrDefault(x => x.Id == id);
        }
        public  int countStudentEnrolled(int courseID)
        {
            return dbContext.Student_Courses.Count(x=>x.Course_Id == courseID);
        }
        public void Add(Instructor instructor)
        {
            dbContext.Instructors.Add(instructor);
        }

        public void delete(string id)
        {
            dbContext.Instructors.Remove(getById(id));
        }

        public void save()
        {
            dbContext.SaveChanges();
        }

        public void update(Instructor instructor)
        {
            dbContext.Update(instructor);
        }

    }
}
