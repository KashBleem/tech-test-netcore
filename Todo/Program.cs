using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Todo.Models; // Ensure this namespace includes ApplicationUser

namespace Todo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            CreateUserIfNotExists(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void CreateUserIfNotExists(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    var user = new IdentityUser { UserName = "kash.bleem@gmail.com", Email = "kash.bleem@gmail.com" };
                    var result = userManager.CreateAsync(user, "Password123!").Result;

                    if (result.Succeeded)
                    {
                        // Assign roles??
                    }
                    else
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogWarning("User creation failed: {0}", string.Join(", ", result.Errors.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating the user.");
                }
            }
        }
    }
}
