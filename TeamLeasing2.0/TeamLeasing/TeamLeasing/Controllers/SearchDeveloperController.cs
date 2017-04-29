using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
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
        private readonly OptimizedDbManager _manager;
        private readonly IHostingEnvironment _environment;


        public SearchDeveloperController(TeamLeasingContext teamLeasingContext, OptimizedDbManager manager
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
            var developers = await _manager.GetDeveloperUser();
            var selectedDevelopers = !developersId.Any()
                 ? developers
                 : developers.Join(developersId, a => a.Id, b => b, (a, b) => a);

            return View("DeveloperSearch", selectedDevelopers.ToList());
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Developers(SidebarDeveloperViewModel vm)
        {
            List<DeveloperUser> developers = await _manager.GetDeveloperUser();
            List<DeveloperUser> searchingDevelopers = await GetSearchResul(vm, developers);
            TempData.Put("searchDevelopers", vm);
            return RedirectToAction("Developers", new { developersId = new List<int>(searchingDevelopers.Select(s => s.Id)
                                                                                                        .ToList()) });
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



        private async Task<List<DeveloperUser>> GetSearchResul(SidebarDeveloperViewModel vm,
            List<DeveloperUser> developers)
        {
           return  await  Task.Run(async () =>
            {
                var tech = Aplly<DeveloperUser>(w => vm.TechnologyNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.Technology.Name), developers);

                var level = Aplly<DeveloperUser>(w => vm.LevelNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.Level), developers);

                var isFinishedUniversity = Aplly<DeveloperUser>(w => vm.UniversityNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.IsFinishedUniversity), developers);

                var experience = Aplly<DeveloperUser>(w => w.Experience > vm.ExpirienceMin
                                                           && w.Experience < vm.ExpirienceMax, developers);

                var searchingResult = await Intersection(tech, level, isFinishedUniversity, experience);

                return searchingResult.ToList();
            });
         
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

        private List<T> Aplly<T>(Func<T, bool> querry, List<T> list)
        {
            return list.Where(querry).ToList();
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
