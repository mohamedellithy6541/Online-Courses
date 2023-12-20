using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.Static;
using onlineCourses.Models;
using onlineCourses.Repository;

namespace onlineCourses.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> IndexAsync()
        {

            var categories = _categoryRepository.GetAll();
            ViewBag.coursesCount = await _categoryRepository.CategoryCoursesCount();

            return View(categories);
        }

        public async Task<IActionResult> Courses(string categoryName)
        {
            var courses = await _categoryRepository.GetCategoryCourses(categoryName);
            return View(courses);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Instructor)]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.Create(category);
                }
                catch
                {
                    ModelState.AddModelError("", "Error occured while creating category");
                    return View(category);
                }

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Instructor)]
        public async Task<IActionResult> Edit(int Id)
        {
            Category category = await _categoryRepository.GetById(Id);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.Edit(category);
                }
                catch
                {
                    ModelState.AddModelError("", "Error occurred while updating category");
                    return View(category);
                }

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Instructor)]
        public async Task<IActionResult> Delete(int Id)
        {
            Category category = await _categoryRepository.GetById(Id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            
            try
            {
                await _categoryRepository.Delete(category);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error occurred while deleting the category");
                return View(category);
            }

            return RedirectToAction("Index");

        }
    }
}
