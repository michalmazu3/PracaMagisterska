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
using TeamLeasing.Infrastructure;
using TeamLeasing.Models;
using TeamLeasing.Services;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService;
using TeamLeasing.Services.AppConfigurationService.LevelService;
using TeamLeasing.Services.AppConfigurationService.ProvinceService;
using TeamLeasing.Services.AppConfigurationService.TechnologyService;
using TeamLeasing.Services.Mail;
using TeamLeasing.Services.MailService;
using TeamLeasing.Services.UploadService;
using TeamLeasing.Services.UserService;

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

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TeamLeasingContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<OptimizedUserManager>();


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
            services.AddSession();
            services.AddDbContext<TeamLeasingContext>(ServiceLifetime.Scoped);
            services.AddSingleton(_configuration);
            services.AddTransient<TeamLeasingSeedData>();

            services.AddSingleton<TeamLeasingSeedData>();
            services.AddSingleton<SeedRoles>();

            //informacje konfiguracyjne dla serwisu
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<ITechnology, Services.AppConfigurationService.TechnologyService.Technology>();
            services.AddScoped<IProvince, Province>();
            services.AddScoped<ILevel, Level>();
            services.AddScoped<IIsFinishedUniversity, IsFinishedUniversity>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessage, MessageModel>();
            services.AddScoped<ISendEmail, SendEmail>();
            services.AddScoped<IUploadService, UploadService>();

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


            app.UseSession();
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
