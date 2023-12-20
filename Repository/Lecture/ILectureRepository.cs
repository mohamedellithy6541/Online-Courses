using onlineCourses.Models;

namespace onlineCourses.Repository.Lectures
{
	public interface ILectureRepository
	{
		void AddLecture(Lecture lecture);
		void DeleteLecture(Lecture lecture);
		List<Lecture> getAllLectures();
		List<Lecture> getLecByCourseID(int id);

        Lecture getLecByID(int ID);
		void saveDB();
		void UpdateLec(Lecture lecture);
	}
}