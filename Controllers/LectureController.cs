using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.Static;
using onlineCourses.Models;
using onlineCourses.Repository.Lectures;
using System.Security.Claims;

namespace onlineCourses.Controllers
{
	public class LectureController : Controller
	{
		private ILectureRepository lectureRepository;
		private string Instructor_ID;


		public LectureController(ILectureRepository lectureRepository)
		{

			this.lectureRepository = lectureRepository;

		}

		#region GetALL
		[Authorize]
		public IActionResult Index(int id)
		{
			ViewData["crs_id"] = id;
			return View(lectureRepository.getLecByCourseID(id));
		}
		#endregion


		#region New
		[Authorize(Roles =UserRoles.Instructor)]
		public IActionResult New(int crs_id) {
			ViewData["id"]= HomeController.Instructor_ID;
			ViewData["crs_id"]= crs_id;
			
            return View("New");
		}
		[HttpPost]
		public IActionResult SaveNew(Lecture lec)
		{
			if(ModelState.IsValid)
			{
				lectureRepository.AddLecture(lec);
				lectureRepository.saveDB();
				return RedirectToAction("Index", "Lecture", new { id = lec.crs_id });
			}
			return View("New",lec);
		}

		#endregion

		#region Edit
		[Authorize(Roles =UserRoles.Instructor)]
		public IActionResult Edit(int id) 
		{ 
		Lecture model = lectureRepository.getLecByID(id);
		return View("Edit",model);
		
		}
		[HttpPost]
		public IActionResult SaveEdit(Lecture lec)
		{		
		if (ModelState.IsValid)
			{
				lectureRepository.UpdateLec(lec);
				lectureRepository.saveDB();
				return RedirectToAction("Index", "Lecture", new {id=lec.crs_id});
			}
			return View("Edit", lec);
		}


		#endregion

		#region Delete

		[Authorize(Roles ="Instructor")]
        public IActionResult Delete(int id)
		{
			Lecture model = lectureRepository.getLecByID(id);
			lectureRepository.DeleteLecture(model);
			lectureRepository.saveDB();
			return RedirectToAction("Index", "Lecture", new { id = model.crs_id });
		}
		#endregion

	}
}
