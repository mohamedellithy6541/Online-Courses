using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.ViewModels.QuestionViewModels;
using onlineCourses.Models;
using onlineCourses.Repository.Courses;
using onlineCourses.Repository.Exams;
using onlineCourses.Repository.Questions;

namespace onlineCourses.Controllers
{
    public class QuestionController : Controller
    {
        private IQuestionRepository questionRepository;
        private IExamRepository examRepository;

        public QuestionController(IQuestionRepository questionRepository,IExamRepository examRepository)
        {
            this.questionRepository = questionRepository;
            this.examRepository = examRepository;
        }
        public IActionResult Index(int ExamID)
        {
            var questions = questionRepository.getAllQuestions(ExamID);
            var selectedColumns = questions.Select(q => new { q.Body, q.Answers, q.ModelAnswer, q.qust_type });
            return Json(selectedColumns);
        }
        public IActionResult AddQuestion(int ExamID)
        {
            List<QuestionType> questionTypes = new List<QuestionType>();
            questionTypes.Add(new QuestionType() { Id = 1, Name = "TF" });
            questionTypes.Add(new QuestionType() { Id = 2, Name = "MCQ" });
            ViewBag.QuesTypes = questionTypes;
            ViewBag.examID = ExamID;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuestion(AddQuestionModel addQuestionModel,bool newQ=false)
        {
            if(ModelState.IsValid)
            {
                Question question = new Question();
                question.Body = addQuestionModel.Body;
                question.Answers = addQuestionModel.Answers;
                question.ModelAnswer = addQuestionModel.ModelAnswer;
                question.exam_id = addQuestionModel.exam_id;
                question.qust_type = addQuestionModel.qust_type;
                questionRepository.AddQuestion(question);
                questionRepository.saveDB();
                if (newQ)
                {
                    return RedirectToAction("AddQuestion", "Question", new {ExamID = question.exam_id});
                }
                return RedirectToAction("CourseDetails","Course",
                new {id= examRepository.getExamByID(question.exam_id ?? 0).crs_id });
            }
            List<QuestionType> questionTypes = new List<QuestionType>();
            questionTypes.Add(new QuestionType() { Id = 1, Name = "TF" });
            questionTypes.Add(new QuestionType() { Id = 2, Name = "MCQ" });
            ViewBag.QuesTypes = questionTypes;
            return View(addQuestionModel);
        }
        public ActionResult DeleteQuestion(int id) 
        {
            Question question = questionRepository.getQuestionByID(id);

			questionRepository.DeleteQuestion(question);
            questionRepository.saveDB();
			return RedirectToAction("CourseDetails", "Course", new
			{
				id = examRepository.getExamByID(question.exam_id ?? 0).crs_id
			});
		}
		public ActionResult DeleteExamQuestions(int ExamID)
		{
			Exam exam= examRepository.getExamByID(ExamID);

			questionRepository.DeleteQuestions(ExamID);
			questionRepository.saveDB();
			return RedirectToAction("CourseDetails", "Course", new
			{
				id = exam.crs_id
			});
		}
	}
}
