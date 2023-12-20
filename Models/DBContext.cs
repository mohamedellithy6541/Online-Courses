using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace onlineCourses.Models
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {

        }
        public DBContext() : base()
        { }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors  { get; set; }
        public virtual DbSet<Course> Courses  { get; set; }
        public virtual DbSet<Student_Course> Student_Courses  { get; set; }
        public virtual DbSet<Exam> Exams  { get; set; }
        public virtual DbSet<Question> Questions  { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes  { get; set; }
        public virtual DbSet<Lecture> Lectures  { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ITI_MVC_Project;Trusted_Connection=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.UserId, l.LoginProvider, l.ProviderKey });

            builder.Entity<Student_Course>().HasKey("Stud_Id", "Course_Id");

		}
    }
}
