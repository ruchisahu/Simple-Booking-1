using EventCatalogAPI.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EventCatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())      // setting up docker destroy docer andclean memory if use in a using loop.
            {
                var services = scope.ServiceProvider;  //docker providing alll the provider one iscatalogcontext
                var context = services.GetRequiredService<EventCatalogContext>();

                DbInitializer.SeedAsync(context).Wait();
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
