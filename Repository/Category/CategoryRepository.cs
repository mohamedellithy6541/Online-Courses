using Microsoft.EntityFrameworkCore;
using onlineCourses.Models;

namespace onlineCourses.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _dbContext;
        public CategoryRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> GetAll()
        {

            return _dbContext.Categories
                .Include(c => c.Courses)
                .AsNoTracking().ToList();
        }

        public async Task<Dictionary<string,int>> CategoryCoursesCount()
        {
            Dictionary<string,int> coursesCount = new Dictionary<string,int>();
            var categories = _dbContext.Categories.ToList();
            var courseCount = 0;

            foreach (var category in categories)
            {
                courseCount = _dbContext.Entry(category)
                .Collection(c => c.Courses)
                .Query()
                .Count(x=> x.IsDeleted==false);

                coursesCount.Add(category.Name, courseCount);
            }
            
            return coursesCount;

        }

        public async Task<Category> GetById(int id)
        {
            Category category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task<Dictionary<Course,int>?> GetCategoryCourses(string categoryName)
        {
            Dictionary<Course,int> coursesAndStudentsCount = new Dictionary<Course,int>();
            var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Name.Equals(categoryName));
            if(category == null)
            {
                return null;
            }
            var courses = await _dbContext.Courses
                .Where(c => c.IsDeleted == false & c.cat_id == category.Id).Include(x=>x.Instructor)
                .ToListAsync();
            int count = 0;
            foreach(Course course in courses)
            {
                count = _dbContext.Entry(course)
                .Collection(c => c.Student_Course)
                .Query()
                .Count();

                coursesAndStudentsCount.Add(course, count);
            }

            return coursesAndStudentsCount;
        }
        public async Task Create(Category category)
        {
            _dbContext.Categories.Add(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(Category category)
        {
            _dbContext.Categories.Update(category);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();
        }
    }
}
