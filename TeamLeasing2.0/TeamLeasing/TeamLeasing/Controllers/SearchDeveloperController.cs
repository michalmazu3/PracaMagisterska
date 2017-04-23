﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Controllers
{
    [Route("search")]
    public class SearchDeveloperController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly UserManager<User> _manager;
        private readonly IHostingEnvironment _environment;


        public SearchDeveloperController(TeamLeasingContext teamLeasingContext, UserManager<User> manager
            , IHostingEnvironment environment)
        {
            _teamLeasingContext = teamLeasingContext;
            _manager = manager;
            _environment = environment;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Developers(List<int> developersId)
        {

            var developers = !developersId.Any()
                ? await _teamLeasingContext.DeveloperUsers
                    .Include(i => i.Technology)
                    .ToListAsync()
                : await _teamLeasingContext.DeveloperUsers
                    .Include(i => i.Technology)
                    .Join(developersId, a => a.Id, b => b, (a, b) => a).ToListAsync();
            return View("DeveloperSearch", developers);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Developers(SidebarDeveloperViewModel vm)
        {
            IEnumerable<DeveloperUser> searchingDevelopers = new List<DeveloperUser>();
            List<DeveloperUser> developers = await _teamLeasingContext.DeveloperUsers
                                                                      .Include(i => i.Technology)
                                                                      .ToListAsync();

            var tech = await Apllytechnologies(vm, developers);
            var level = await ApllyLevel(vm, developers);
            var isFinishedUniversity = await ApllyIsFinishedUniversity(vm, developers);
            var experience = await ApllyExperience(vm, developers);
            searchingDevelopers = await Intersection(tech, level, isFinishedUniversity, experience);

            TempData.Put("search", vm);

            return RedirectToAction("Developers", new { developersId = new List<int>(searchingDevelopers.Select(s => s.Id).ToList()) });
        }

        [Authorize]
        [Route("developer/{developerId}/[action]/download")]
        public async Task<IActionResult> Cv(int developerId)
        {
            DeveloperUser developerUser = await _teamLeasingContext.DeveloperUsers.FindAsync(developerId);

            return await DownloadFile(developerUser.Surname, developerUser.Name, developerId);

        }

        private async Task<IActionResult> DownloadFile(string surname, string name, int id)
        {
            var fileName = $"{surname}_{name}.pdf";
            var filepath = $"wwwroot/UploadFIle/Cv/{fileName}";

            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes, "application/x-msdownload", fileName);
            }
            catch (Exception e)
            {
                return View("_Error", new ErrorViewModel()
                {
                    Message = $"Nie można odnaleźć pliku dla użytkownika {name} {surname}",
                    ReturnUrl = UrlHelperExtensions.Action(Url, "Profile", "SearchDeveloper", new { developerId = id })
                });
            }
        }

        private Task<IEnumerable<DeveloperUser>> Intersection(params IEnumerable<DeveloperUser>[] paramsDevelopers)
        {
            return Task.Run(() =>
            {
                paramsDevelopers = paramsDevelopers.Where(w => w.Count() != 0).ToArray();

                for (int i = 1; i < paramsDevelopers.Length; i++)
                {
                    paramsDevelopers[0] = paramsDevelopers[0].Intersect(paramsDevelopers[i]).ToList();
                }
                return paramsDevelopers[0];
            });

        }

        private Task<List<DeveloperUser>> ApllyExperience(SidebarDeveloperViewModel vm, List<DeveloperUser> developers)
        {
            return Task.Run(() =>
            {
                return developers.Where(w => w.Experience > vm.ExpirienceMin && w.Experience < vm.ExpirienceMax)
                .ToList();
            });


        }

        private Task<List<DeveloperUser>> ApllyLevel(SidebarDeveloperViewModel vm, List<DeveloperUser> developers)
        {
            return Task.Run(() =>
            {
                return developers.Where(w => vm.LevelNameValuePairs
                             .Where(v => v.Value)
                              .Select(s => s.Name)
                              .Contains(w.Level))
                                 .ToList();
            });

        }
        private Task<List<DeveloperUser>> ApllyIsFinishedUniversity(SidebarDeveloperViewModel vm, List<DeveloperUser> developers)
        {
            return Task.Run(() =>
            {
                return developers.Where(w => vm.UniversityNameValuePairs
                          .Where(v => v.Value)
                          .Select(s => s.Name)
                          .Contains(w.IsFinishedUniversity))
                           .ToList();
            });

        }

        private Task<List<DeveloperUser>> Apllytechnologies(SidebarDeveloperViewModel vm, List<DeveloperUser> developers)
        {
            return Task.Run(() =>
            {
                return developers.Where(w => vm.TechnologyNameValuePairs
                        .Where(v => v.Value)
                        .Select(s => s.Name)
                      .Contains(w.Technology.Name))
                          .ToList();
            });

        }
        [Route("developer/{developerId}")]
        // [HttpGet("{developerId}")]
        public async Task<IActionResult> Profile(int developerId)
        {

            var developer = await _teamLeasingContext.DeveloperUsers
                                               .Include(i => i.Technology)
                                               .FirstOrDefaultAsync(w => w.Id == developerId);

            return View("DeveloperProfile", developer);


        }
    }
}
