using onlineCourses.Models;

namespace onlineCourses.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Task<Category> GetById(int id);
        Task<Dictionary<Course, int>?> GetCategoryCourses(string categoryName);
        Task<Dictionary<string, int>> CategoryCoursesCount();
        Task Create(Category category);
        Task Edit(Category category);
        Task Delete(Category category);
    }
}
