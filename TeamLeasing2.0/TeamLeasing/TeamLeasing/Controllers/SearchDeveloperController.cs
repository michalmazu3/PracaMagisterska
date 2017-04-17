using System;
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
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Controllers
{
    [Route("search")]
    public class SearchDeveloperController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly UserManager<User> _manager;
        private readonly IHostingEnvironment _environment;


        public SearchDeveloperController(TeamLeasingContext teamLeasingContext, UserManager<User> manager, IHostingEnvironment environment)
        {
            _teamLeasingContext = teamLeasingContext;
            _manager = manager;
            _environment = environment;
            //using (_teamLeasingContext)
            //{
            //    var list = _teamLeasingContext.Technologies.Include(i => i.DeveloperUsers).Select(s => s).ToArray();
            //}
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Developer(List<int> developersId)
        {

            var developers = developersId.Count() == 0
                ? await _teamLeasingContext.DeveloperUsers
                    .Include(i => i.Technology)
                    .ToListAsync()
                : await _teamLeasingContext.DeveloperUsers
                    .Include(i => i.Technology)
                    .Join(developersId, a => a.Id, b => b, (a, b) => a).ToListAsync();



            return View("DeveloperSearch", developers);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Developer(SearchDeveloperViewModel vm)
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

         
            return RedirectToAction("Developer",  new { developersId = new List<int>(searchingDevelopers.Select(s => s.Id).ToList()) }); 
        }

        [Authorize]
        public async Task<IActionResult> Cv(int developerId)
        {
            DeveloperUser developerUser = await _teamLeasingContext.DeveloperUsers.FindAsync(developerId);

            var pathToFile = $"UploadFile\\Cv\\{developerUser.Surname}_{developerUser.Name}.pdf";
            string pdfPath = Path.Combine(_environment.WebRootPath, pathToFile);
            var file = File(pdfPath, "application/pdf", $"{developerUser.Surname}_{developerUser.Name}.pdf");
            if (file!=null)
            {
                return file;
            }
            else
            {
                throw  new  Exception(message:"brak pliku");
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
        private Task<List<DeveloperUser>> ApllyExperience(SearchDeveloperViewModel vm, List<DeveloperUser> developers)
        {
            return Task.Run(() =>
            {
                return developers.Where(w => w.Experience > vm.ExpirienceMin && w.Experience < vm.ExpirienceMax)
                .ToList();
            });


        }

        private Task<List<DeveloperUser>> ApllyLevel(SearchDeveloperViewModel vm, List<DeveloperUser> developers)
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
        private Task<List<DeveloperUser>> ApllyIsFinishedUniversity(SearchDeveloperViewModel vm, List<DeveloperUser> developers)
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

        private Task<List<DeveloperUser>> Apllytechnologies(SearchDeveloperViewModel vm, List<DeveloperUser> developers)
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
        [Route("developer/[action]/{developerId}")]
       // [HttpGet("{developerId}")]
        public async Task<IActionResult> Profile(int developerId)
        {

            var developer =await _teamLeasingContext.DeveloperUsers
                                               .Include(i => i.Technology)
                                               .FirstOrDefaultAsync(w => w.Id == developerId);

            return View("DeveloperProfile", developer);


        }
    }
}
