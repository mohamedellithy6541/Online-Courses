using Microsoft.EntityFrameworkCore;
using onlineCourses.Models;
using System.Data.Common;

namespace onlineCourses.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly DBContext _dbContext;

        public StudentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public void Enroll(string studentId, int? courseid)
        {
            var koko = _dbContext.Student_Courses.Where(c => c.Stud_Id.Equals(studentId) && c.Course_Id==courseid).FirstOrDefault();
            if (koko!=null)
            {
                return;
            }
            var courses = new Student_Course();
            courses.Stud_Id = studentId;
            courses.Course_Id = courseid;
            var s = _dbContext.Student_Courses.Add(courses);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbContext.Students.ToList();
        }

        public Student GetStudent(string name)
        {
            var student = _dbContext.Students.Include(s => s.Student_Course).ThenInclude(sc =>sc.Course).FirstOrDefault(s => s.UserName == name);

            //var student = _dbContext.Students.Where(s =>s.UserName == name).FirstOrDefault();
            return student;
        }
    }
}
