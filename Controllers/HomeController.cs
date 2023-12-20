using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Models;
using onlineCourses.Repository;
using onlineCourses.Repository.Courses;

namespace onlineCourses.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICourseRepository _courseRepository;
		public static string Instructor_ID;

		public HomeController(ICategoryRepository categoryRepository, ICourseRepository courseRepository)
        {
            _categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
			Claim idclaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
			Instructor_ID = idclaim?.Value;
			var categories = _categoryRepository.GetAll();
            ViewBag.coursesCount = await _categoryRepository.CategoryCoursesCount();

			      ViewBag.courses = _courseRepository.getAllCourses();

            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}