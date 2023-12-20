using Microsoft.EntityFrameworkCore;
using onlineCourses.Models;

namespace onlineCourses.Repository
{
    public class QuestionTypeRepository : IQuestionTypeRepository
    {
        private readonly DBContext _dbContext;

        public QuestionTypeRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<QuestionType>> GetAll()
        {
            return await _dbContext.QuestionTypes.AsNoTracking().ToListAsync();
        }

        public async Task<QuestionType> GetById(int Id)
        {
            return await _dbContext.QuestionTypes.SingleOrDefaultAsync(q => q.Id == Id);
        }

        public async Task Create(QuestionType questionType)
        {
            await _dbContext.QuestionTypes.AddAsync(questionType);

            await _dbContext.SaveChangesAsync();
        }
    }
}
