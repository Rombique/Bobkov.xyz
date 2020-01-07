using Bobkov.DAL;
using Bobkov.DAL.EF;
using Bobkov.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bobkov.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var context = services.GetRequiredService<MainContext>();
                    await DbInitializer.InitIdentityAsync(roleManager, userManager);
                    DbInitializer.InitMain(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Произошла ошибка при инициализации БД.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
