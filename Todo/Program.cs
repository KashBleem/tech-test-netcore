using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

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

                    // Create specific user
                    var specificUser = new IdentityUser { UserName = "kash.bleem@gmail.com", Email = "kash.bleem@gmail.com" };
                    var result = userManager.CreateAsync(specificUser, "Password123!").Result;

                    if (!result.Succeeded)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogWarning("User creation failed: {0}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    // Create random users
                    for (int i = 1; i <= 4; i++)
                    {
                        var randomUserName = $"randomuser{i}@example.com";
                        var randomUser = new IdentityUser { UserName = randomUserName, Email = randomUserName };
                        result = userManager.CreateAsync(randomUser, "RandomPass123!").Result;

                        if (!result.Succeeded)
                        {
                            var logger = services.GetRequiredService<ILogger<Program>>();
                            logger.LogWarning("User creation failed for {0}: {1}", randomUserName, string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
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
