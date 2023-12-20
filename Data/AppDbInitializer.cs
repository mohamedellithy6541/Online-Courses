using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using onlineCourses.Data.Static;

namespace onlineCourses.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedRolesAsync(WebApplication app )
        {
            using(var ServiceScope = app.Services.CreateScope())
            {
                var roleManager = ServiceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Student))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Student));

                if (!await roleManager.RoleExistsAsync(UserRoles.Instructor))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Instructor));
            }
        }
    }
}
