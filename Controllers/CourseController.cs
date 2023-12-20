using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.ViewModels.CourseViewModels;
using onlineCourses.Models;
using onlineCourses.Repository;
using onlineCourses.Repository.Courses;
using onlineCourses.Repository.InstructorRepo;
using onlineCourses.Repository.Lectures;

namespace onlineCourses.Controllers
{
    public class CourseController : Controller
	{
		private ICourseRepository courseRepository;
		private ICategoryRepository categoryRepository;
		private IInstructorRepository instructorRepository;

		public CourseController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, ICategoryRepository categoryRepository)
		{
			this.courseRepository = courseRepository;
			this.categoryRepository = categoryRepository;
			this.instructorRepository = instructorRepository;
		}
		public IActionResult Index()
		{

			ViewBag.cats = categoryRepository.GetAll();
			return View(courseRepository.getAllCourses());
			
		}	
		public IActionResult getCoursesByCategory(int CatID)
		{
			if (CatID == 0)
			{
				var courses = courseRepository.getAllCourses().Select(x => new {
					x.Id,
					x.Title,
					x.Description,
					x.Grade,
					x.Name
				});
				return Json(courses);
			}
			else
			{
				var courses = courseRepository.getCourseByCategotyID(CatID).Select(x => new {
					x.Id,
					x.Title,
					x.Description,
					x.Grade,
					x.Name
				});

				return Json(courses);
			}
		}
		public IActionResult CourseDetails(int id)
		{
			Course course = courseRepository.getCourseByID(id);
			ViewData["Count"]=instructorRepository.countStudentEnrolled(id);
            return View(course);
		}
		[HttpGet]
		[Authorize(Roles ="Instructor")]
		public IActionResult NewCourse()
		{
			ViewBag.cats = categoryRepository.GetAll();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult NewCourse(AddCourseModel addCourseModel)
		{
			if (ModelState.IsValid)
			{
				Course course = new Course();
				course.Name = addCourseModel.Name;
				course.Title = addCourseModel.Title;
				course.Description = addCourseModel.Description;
				course.price = addCourseModel.price;
				course.Duration = addCourseModel.Duration;
				course.Grade = addCourseModel.Grade;
				course.Created = DateTime.Now;
				course.IsDeleted = false;
				course.cat_id = addCourseModel.categoryID;
				course.ins_id = HomeController.Instructor_ID;
				courseRepository.AddCourse(course);
				courseRepository.saveDB();
				return RedirectToAction("Home","Instructor");
			}
			return View(addCourseModel);
		}

		[HttpGet]
		[Authorize(Roles = "Instructor")]
		public IActionResult EditCourse(int id)
		{
			Course course = courseRepository.getCourseByID(id);
			AddCourseModel addCourseModel = new AddCourseModel();
			addCourseModel.Name = course.Name;
			addCourseModel.Title = course.Title;
			addCourseModel.Description = course.Description;
			addCourseModel.price = course.price;
			addCourseModel.Duration = course.Duration;
			addCourseModel.Grade = course.Grade;
			addCourseModel.categoryID = course.cat_id ?? 0;
			ViewBag.id = id;
			ViewBag.cats = categoryRepository.GetAll();

			return View(addCourseModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditCourse(AddCourseModel addCourseModel, int id)
		{
			if (ModelState.IsValid)
			{
				Course course = courseRepository.getCourseByID(id);
				course.Name = addCourseModel.Name;
				course.Title = addCourseModel.Title;
				course.Description = addCourseModel.Description;
				course.price = addCourseModel.price;
				course.Duration = addCourseModel.Duration;
				course.Grade = addCourseModel.Grade;
				course.cat_id = addCourseModel.categoryID;
				courseRepository.UpdateCourse(course);
				courseRepository.saveDB();
				return RedirectToAction("Home", "Instructor");
			}
			ViewBag.cats = categoryRepository.GetAll();
			return View(addCourseModel);
		}
		[Authorize(Roles ="Instructor")]
		public ActionResult DeleteCourse(int id)
		{
			Course course = courseRepository.getCourseByID(id);
			course.IsDeleted = true;
			courseRepository.UpdateCourse(course);
			courseRepository.saveDB();
            return RedirectToAction("Home", "Instructor");
        }

    }
}
