using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamLeasing.DAL;
using Microsoft.DotNet.Cli.Utils;

namespace TeamLeasing
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(_env.ContentRootPath).AddJsonFile("config.json").AddEnvironmentVariables();
            _configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<TeamLeasingContext>();
            services.AddSingleton(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults:  new {Controller = "Home",Action = "Index"}
                );
           
            });
        }
    }
}
