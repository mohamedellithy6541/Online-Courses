using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.Static;
using onlineCourses.Models;
using onlineCourses.Repository;
using System.Security.Claims;

namespace onlineCourses.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        public StudentController(IStudentRepository studentRepository)
        {
            _StudentRepository = studentRepository;
        }

        public IStudentRepository _StudentRepository { get; }

        public IActionResult Index()
        {
            Student s=_StudentRepository.GetStudent(User.Identity.Name);
            return View(s);
        }
        [Authorize]
        public IActionResult Details(string Name)
        {
            var s = _StudentRepository.GetStudent(Name);
            return View(s);
        }
        [Authorize]
        public IActionResult AddCourse(int courseid) 
        {
            Claim  ss=User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.NameIdentifier);
            string sid = ss.Value;
            _StudentRepository.Enroll(sid,courseid);

            return RedirectToAction("Details", new {Name= User.Identity.Name });
        }
    }
}
