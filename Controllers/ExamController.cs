using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.ViewModels.ExamViewModels;
using onlineCourses.Models;
using onlineCourses.Repository.Courses;
using onlineCourses.Repository.Exams;
using onlineCourses.Repository.Questions;

namespace onlineCourses.Controllers
{
	public class ExamController : Controller
	{
		private IExamRepository examRepository;
        private ICourseRepository courseRepository;
		private IQuestionRepository questionRepository;

        public ExamController(IQuestionRepository questionRepository, IExamRepository examRepository,ICourseRepository courseRepository) 
		{
			this.examRepository = examRepository;
            this.courseRepository = courseRepository;
			this.questionRepository = questionRepository;
        }
		public IActionResult index(int crs_id)
		{
			Exam exam = examRepository.getExamByCourseID(crs_id);
			if(exam != null)
			ViewData["Questions"] = questionRepository.getAllQuestions(exam.Id);
			return View(exam);
		}
		//public IActionResult getExamsByName(string name)
		//{
		//	List<Exam> exams = examRepository.getExamByName(name);
		//	var examsList = exams.Select(e=>new {e.Id,e.Name,e.Duration,e.crs_id}).ToList();

		//	return Json(examsList);
		//}
		//public IActionResult getExamsByCourseID(int ID)
		//{
		//	Exam exams = examRepository.getExamByCourseID(ID);
		//	var examsList = exams.Select(e => new { e.Id, e.Name, e.Duration }).ToList();
		//	return Json(examsList);
		//}
		[HttpGet]
		public IActionResult AddExam()
		{
			ViewBag.courses = courseRepository.getAllCourses();
			return View();
		}
        [HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult AddExam(AddExamModel examModel)
        {
			if (ModelState.IsValid)
			{
				Exam exam = new Exam();
				exam.Name = examModel.Name;
				exam.Duration = examModel.Duration;
				exam.crs_id = examModel.Course_ID;
				try
				{
					examRepository.AddExam(exam);
					examRepository.saveDB();
					return RedirectToAction("AddQuestion", "Question" , new { ExamID = exam.Id});
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("Course_ID", ex.InnerException.Message);
				}
			}
            ViewBag.courses = courseRepository.getAllCourses();
			return View(examModel);
        }
        public IActionResult EditExam(Exam exam)
        {
            ViewBag.courses = courseRepository.getAllCourses();
            return View(new AddExamModel() { Name=exam.Name,Course_ID=exam.crs_id??0,Duration=exam.Duration});
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult EditExam(AddExamModel examModel)
        {
            if (ModelState.IsValid)
            {
                Exam exam = new Exam();
                exam.Name = examModel.Name;
                exam.Duration = examModel.Duration;
                exam.crs_id = examModel.Course_ID;
                try
                {
                    examRepository.UpdateExam(exam);
                    examRepository.saveDB();
                    return RedirectToAction("index", "Course");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Course_ID", ex.InnerException.Message);
                }
            }
            ViewBag.courses = courseRepository.getAllCourses();
            return View(examModel);
        }
    }
}
