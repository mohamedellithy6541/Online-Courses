using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onlineCourses.Data.Static;
using onlineCourses.Data.ViewModels;
using onlineCourses.Models;

namespace onlineCourses.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(IWebHostEnvironment webHostEnvironment,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Register(RegisterVM registerVM)
        {
			if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var userExist = await userManager.FindByEmailAsync(registerVM.Email);
            if(userExist != null)
            {
                ModelState.AddModelError("Email", "This Email Already Exists");
                return View(registerVM);
            }

            var role = Request.Form["role"];

            AppUser user = null;

            if (role.Contains("Student"))
            {
                user = new Student()
                {
                    UserName = registerVM.UserName,
                    Age = registerVM.Age,
                    Address = registerVM.Address,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    Gender = registerVM.Gender,
                    PasswordHash = registerVM.Password
                };
            }
            else
            {
				user = new Instructor()
				{
					UserName = registerVM.UserName,
					Age = registerVM.Age,
					Address = registerVM.Address,
					Email = registerVM.Email,
					PhoneNumber = registerVM.PhoneNumber,
					Gender = registerVM.Gender,
					PasswordHash = registerVM.Password
				};
			}

            var result = await userManager.CreateAsync(user, registerVM.Password);

            

            if (result.Succeeded)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile postedImage = Request.Form.Files["UploadedImage"];
                    string path = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = Path.GetFileName(registerVM.UserName + postedImage.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        postedImage.CopyTo(stream);
                        registerVM.ImageURL = fileName ?? null;
                    }
                }

                user.ImageURL = registerVM.ImageURL;

                if (role.Contains("Student"))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Student);
                }
                else
                {
					await userManager.AddToRoleAsync(user, UserRoles.Instructor);
				}
				await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginVM.UserName);

                if (user == null)
                {
                    ModelState.AddModelError("", "No Such User Exist");
                    return View(loginVM);
                }

                var check = await userManager.CheckPasswordAsync(user, loginVM.Password);

                if (!check)
                {
                    ModelState.AddModelError("", "Wrong Username or Password");
                    return View(loginVM);
                }
                await signInManager.SignInAsync(user, loginVM.RememberMe);
            }
            return RedirectToAction("index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
