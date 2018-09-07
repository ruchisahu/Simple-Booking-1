using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using TokenServiceApi.Data;
using TokenServiceApi.Models;

namespace TokenServiceApi
{
    public class Program
        {
            public static void Main(string[] args)
            {

                var host = BuildWebHost(args);

                Console.Title = "IdentityServer4";

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                 //   .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                    .CreateLogger();

                var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //  var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //  var emailService = services.GetRequiredService<IEmailService>();
            //  SeedData.EnsureSeedData(host.Services, roleManager, userManager, emailService);
            //var context = services.GetRequiredService<ApplicationDbContext>();
            //IdentityDbInit.Initialize(context, userManager);
            host.Run();
            }

            public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                  //  .UseSerilog()
                    .Build();
        }
    }
