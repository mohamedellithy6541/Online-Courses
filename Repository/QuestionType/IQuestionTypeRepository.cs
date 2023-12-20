using onlineCourses.Models;

namespace onlineCourses.Repository
{
    public interface IQuestionTypeRepository
    {
        Task Create(QuestionType questionType);
        Task<List<QuestionType>> GetAll();
        Task<QuestionType> GetById(int Id);
    }
}