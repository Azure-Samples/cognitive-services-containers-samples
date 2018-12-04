using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontendService.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FrontendService
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // This allows the application to work when run locally with docker-compose.
                // In this case, the SDK will direct requests to localhost:5000 instead of the hostname of the Kubernetes service
                LanguageUnderstandingController.ServiceEndpoint = "http://localhost:5000";
            }
            app.UseMvc();
        }
    }
}
