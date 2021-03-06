﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
using TeamLeasing.Infrastructure.Helper;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.Services.AppConfigurationService.CheckAccesToActionService;
using TeamLeasing.Services.AppConfigurationService.EmploymentTypeService;
using TeamLeasing.Services.AppConfigurationService.ProjectTypeService;
using TeamLeasing.Services.Mail;
using TeamLeasing.Services.MailService;
using TeamLeasing.Services.UploadService;

namespace TeamLeasing
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IHostingEnvironment _env;

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

            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<TeamLeasingContext>()
            //    .AddDefaultTokenProviders()
            //    .AddUserManager<OptimizedUserManager>();


            services.AddIdentity<User, IdentityRole>(c =>
                {
                    c.Password.RequiredLength = 4;
                    c.User.RequireUniqueEmail = true;
                    c.Lockout.MaxFailedAccessAttempts = 5;
                    c.Cookies.ApplicationCookie.LoginPath = "/login/login";
                })
                .AddEntityFrameworkStores<TeamLeasingContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<OptimizedDbManager>();
            ;


            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                    config.Filters.Add(new RequireHttpsAttribute());
            });
            services.AddSession();
            services.AddDbContext<TeamLeasingContext>(ServiceLifetime.Scoped);
            services.AddSingleton(_configuration);
            services.AddScoped<TeamLeasingSeedData>();

            services.AddSingleton<TeamLeasingSeedData>();
            services.AddSingleton<SeedRoles>();
            //service
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICheckAccesToActionService, CheckAccesToActionService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IMessage, MessageModel>();
            services.AddScoped<ISendEmail, SendEmail>();
            services.AddScoped<IEmploymentType, EmploymentType>();
            services.AddScoped<IProjectType, ProjectType>();

            //helper
            services.AddScoped<ILoadingDataToSidebarHelper, LoadingDataToSidebarHelper>();
            services.AddScoped<ISearchHelper, SearchHelper>();

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
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
            seedRoles.Seed().Wait();
            seeder.Seed().Wait();
        }
    }
}