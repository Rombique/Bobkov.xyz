using Bobkov.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bobkov.DAL
{
    public class IdentityDbInitializer
    {
        public static async Task InitAsync(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            var userRole = await roleManager.FindByNameAsync("User");
            if (adminRole == null)
                await roleManager.CreateAsync(new Role { Name = "Admin" });
            if (userRole == null)
                await roleManager.CreateAsync(new Role { Name = "User" });

            if (await userManager.FindByNameAsync("Rombique") == null)
            {
                User admin = new User { Email = "roman@bobkov.xyz", UserName = "Rombique" };
                IdentityResult result = await userManager.CreateAsync(admin, "P@ssw0rd"); //TODO: нужно брать из конфига
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
