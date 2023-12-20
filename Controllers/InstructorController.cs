using Microsoft.AspNetCore.Mvc;
using onlineCourses.Repository.Courses;
using onlineCourses.Repository.InstructorRepo;
using onlineCourses.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using onlineCourses.Repository;
using onlineCourses.Data.Static;
using Microsoft.AspNetCore.Identity;

namespace onlineCourses.Controllers
{
    [Authorize(Roles =UserRoles.Instructor)]
    public class InstructorController : Controller
    {
        private readonly ICourseRepository CourseRepository;
        public readonly IInstructorRepository InstructorRepository;
        public readonly ICategoryRepository CategoryRepository;

        public InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, ICategoryRepository categoryRepository)
        {
            InstructorRepository = instructorRepository;
            CourseRepository = courseRepository;
            CategoryRepository = categoryRepository;

        }
        public IActionResult Home()
        {
			List<Course> courses = CourseRepository.GetCoursesByInstructorId(HomeController.Instructor_ID);
            return View(courses);
        }
        public IActionResult Details()
        {
			Instructor instructor= InstructorRepository.getById(HomeController.Instructor_ID);
            return View(instructor);
        }



	}
}
