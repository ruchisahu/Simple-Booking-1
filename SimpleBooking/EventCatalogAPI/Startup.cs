using EventCatalogAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventCatalogAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<EventCatalogContext>(options =>
            {
                var server = Configuration["DatabaseServer"];
                var database = Configuration["DatabaseName"];
                var user = Configuration["DatabaseUser"];
                var password = Configuration["DatabaseUserPassword"];
                var connectionString = $"Server={server};Database={database};User={user};Password={password};";
                options.UseSqlServer(connectionString);
            });

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "EventCatalog",
                    Version = "v1",
                    Description = "Catalog",
                    TermsOfService = "Terms Of Services"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "EventCatalog");
            });

            app.UseMvc();
        }
    }
}
