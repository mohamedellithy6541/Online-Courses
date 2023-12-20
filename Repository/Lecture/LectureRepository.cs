using onlineCourses.Models;


namespace onlineCourses.Repository.Lectures
{
	public class LectureRepository : ILectureRepository
	{
		private DBContext dBContext;
		public LectureRepository(DBContext dBContext)
		{
			this.dBContext = dBContext;
		}
		public void AddLecture(Lecture lecture)
		{
			dBContext.Lectures.Add(lecture);
		}

		public void DeleteLecture(Lecture lecture)
		{
			dBContext.Remove(lecture);
		}
		public List<Lecture> getAllLectures()
		{
			return dBContext.Lectures.ToList();
		}
		public Lecture getLecByID(int ID)
		{
			return dBContext.Lectures.Where(l => l.Id == ID).FirstOrDefault();
		}
		public List<Lecture> getLecByCourseID(int id)
		{
			return dBContext.Lectures.Where(x=>x.crs_id==id).ToList();
		}
		public void saveDB()
		{
		  dBContext.SaveChanges();
		}

		public void UpdateLec(Lecture lecture)
		{
			dBContext.Update(lecture);
		}

	}
}
