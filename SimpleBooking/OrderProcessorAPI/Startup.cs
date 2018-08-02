using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderProcessorAPI.Data;

namespace OrderProcessorAPI
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
            services.AddDbContext<OrderContext>
                (options => options.UseSqlServer(Configuration["ConnectionString"]));

            //Add Framework services
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1",
                new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "SimpleBooking - Order Processor HTTP API",
                    Version = "v1",
                    Description = "The Order Processor Microservice HTTP API.",
                    TermsOfService = "Terms Of Service"
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
            app.UseSwagger()
            .UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "OrderProcessorAPI V1");
            });
            app.UseMvc();
        }
    }
}
