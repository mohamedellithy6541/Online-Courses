using onlineCourses.Models;

namespace onlineCourses.Repository.Questions
{
    public interface IQuestionRepository
    {
        List<Question> getAllQuestions(int ExamID);
        Question getQuestionByID(int id);
		void UpdateQuestion(Question question);
        void DeleteQuestion(Question question);
        void DeleteQuestions(int ExamID);
		void AddQuestion(Question question);
        int saveDB();
    }
}
