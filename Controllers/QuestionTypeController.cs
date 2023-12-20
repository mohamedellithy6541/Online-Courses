using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.Static;
using onlineCourses.Models;
using onlineCourses.Repository;

namespace onlineCourses.Controllers
{
    public class QuestionTypeController : Controller
    {
        private readonly IQuestionTypeRepository questionTypeRepository;

        public QuestionTypeController(IQuestionTypeRepository questionTypeRepository)
        {
            this.questionTypeRepository = questionTypeRepository;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Instructor)]
        public async Task<IActionResult> Create()
        {
            return View(new QuestionType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionType questionType)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await questionTypeRepository.Create(questionType);
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Error occured while creating question type.");
                    return View(questionType);
                }

                return RedirectToAction("AddQuestion", "Question");
            }

            return View(questionType);
        }
    }
}
