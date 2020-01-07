using Bobkov.DAL.EF;
using Bobkov.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bobkov.DAL
{
    public class DbInitializer
    {
        public static async Task InitIdentityAsync(RoleManager<Role> roleManager, UserManager<User> userManager)
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

        public static void InitMain(MainContext context)
        {
            var categories = context.Set<Category>().ToList();
            if (categories.Count == 0)
            {
                List<Category> categoryList = new List<Category>()
                {
                    new Category() { Name = "Sharepoint" },
                    new Category() { Name = "Javascript" },
                    new Category() { Name = ".NET" },
                };
                categoryList.ForEach(a => context.Categories.Add(a));
                var m = context.SaveChanges();
            }
        }
    }
}
