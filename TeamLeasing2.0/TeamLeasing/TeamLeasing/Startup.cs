using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamLeasing.DAL;
using Microsoft.DotNet.Cli.Utils;
using TeamLeasing.Models;
using TeamLeasing.Services;
using TeamLeasing.Services.Developer;
using TeamLeasing.Services.Mail;

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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(7);
                options.CookieName = ".FileSystem";
            });
            services.AddIdentity<User, IdentityRole>(c =>
                {
                    c.Password.RequiredLength = 4;
                    c.User.RequireUniqueEmail = true;
                    c.Lockout.MaxFailedAccessAttempts = 5;
                    c.Cookies.ApplicationCookie.LoginPath = "/login/login";
                })
                .AddEntityFrameworkStores<TeamLeasingContext>()
                .AddDefaultTokenProviders();
         
       
            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            });
          
            services.AddDbContext<TeamLeasingContext>(ServiceLifetime.Scoped);
            services.AddSingleton(_configuration);
            services.AddTransient<TeamLeasingSeedData>();
            services.AddTransient<IMessage,MessageModel>();
            services.AddTransient<ISendEmail, SendEmail>();
            services.AddSingleton<TeamLeasingSeedData>();
            services.AddSingleton<SeedRoles>();
            services.AddSingleton<IDeveloperConfigurationInformation, DeveloperConfigurationInformation>();
            services.AddAutoMapper();
            services.AddLogging();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            TeamLeasingSeedData seeder,
            SeedRoles seedRoles)
        {
            loggerFactory.AddConsole();

            app.UseIdentity();
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole(LogLevel.Information);
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            seedRoles.Seed().Wait();
           seeder.Seed().Wait();

        }
    }
}
