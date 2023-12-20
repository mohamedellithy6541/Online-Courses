using onlineCourses.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using onlineCourses.Repository.Exams;

namespace onlineCourses.Repository.Courses
{
    public class ExamRepository : IExamRepository
    {
        private DBContext dBContext;

        public ExamRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

		public void AddExam(Exam exam)
		{
			dBContext.Exams.Add(exam);
		}

		public void DeleteExam(Exam exam)
		{
			throw new NotImplementedException();
		}

		public Exam getExamByCourseID(int ID)
		{
			return dBContext.Exams.FirstOrDefault(e => e.crs_id == ID);
		}
        public Exam getExamByID(int ID)
        {
            return dBContext.Exams.Where(e => e.Id == ID).FirstOrDefault();
        }

        public List<Exam> getExamByName(string Name)
		{
			return dBContext.Exams.Where(e => e.Name == Name).ToList();
		}

		public int saveDB()
        {
            return dBContext.SaveChanges();
        }

		public void UpdateExam(Exam exam)
		{
			dBContext.Update(exam);
		}
	}
}
