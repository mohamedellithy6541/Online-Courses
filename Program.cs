using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using onlineCourses.Data;
using onlineCourses.Models;
using onlineCourses.Repository;
using onlineCourses.Repository.Courses;
using onlineCourses.Repository.Exams;
using onlineCourses.Repository.InstructorRepo;
using onlineCourses.Repository.Lectures;
using onlineCourses.Repository.Questions;

namespace onlineCourses
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DBContext>(options=>options.UseSqlServer(builder.Configuration.
                GetConnectionString("DbConnectionString")));

            builder.Services.AddSession();
            builder.Services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
                  

            builder.Services.AddIdentity<AppUser, IdentityRole>(o =>
            {
                o.Password.RequiredLength = 10;
                o.Password.RequireUppercase = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireDigit = true;

            })
              .AddEntityFrameworkStores<DBContext>();

            builder.Services.AddIdentityCore<Student>().AddEntityFrameworkStores<DBContext>();

            builder.Services.AddIdentityCore<Instructor>().AddEntityFrameworkStores<DBContext>();

			builder.Services.AddScoped<ILectureRepository, LectureRepository>();
			builder.Services.AddScoped<IStudentRepository, StudentRepository>();

			builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();

			builder.Services.AddScoped<IExamRepository, ExamRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuestionTypeRepository, QuestionTypeRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            AppDbInitializer.SeedRolesAsync(app).Wait();

            app.Run();

        }
    }
}