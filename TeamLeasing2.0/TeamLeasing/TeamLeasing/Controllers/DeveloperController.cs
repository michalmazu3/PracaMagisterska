using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
  

        public DeveloperController(TeamLeasingContext teamLeasingContext)
        {
            _teamLeasingContext = teamLeasingContext;
            //using (_teamLeasingContext)
            //{
            //    var list = _teamLeasingContext.Technologies.Include(i => i.DeveloperUsers).Select(s => s).ToArray();
            //}
         }

        // GET: /<controller>/
        public async Task< IActionResult> Search()
        {
            using (_teamLeasingContext)
            {
               var developers = _teamLeasingContext.Developers.Include(i => i.Technology).ToListAsync();
               return View( await developers);
            }
         
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchDeveloperViewModel vm)
        {
            IEnumerable<Developer> searchingDevelopers = new List<Developer>();
            List<Developer> developers = new List<Developer>();
            using (_teamLeasingContext)
            {
                developers = await _teamLeasingContext.Developers
                    .Include(i => i.Technology)
                    .ToListAsync();
            }
                var tech = await Apllytechnologies(vm, developers);
                var level = await ApllyLevel(vm, developers);
                var isFinishedUniversity = await ApllyIsFinishedUniversity(vm, developers);
                var experience = await ApllyExperience(vm, developers);
                searchingDevelopers = await Intersection(tech, level, isFinishedUniversity, experience);

                return View("Search", searchingDevelopers.ToList());
            
        }
        
        private Task<IEnumerable<Developer>> Intersection(params IEnumerable<Developer>[] paramsDevelopers)
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
        private Task<List<Developer>> ApllyExperience(SearchDeveloperViewModel vm, List<Developer> developers )
        {
            return Task.Run(() =>
            {
              return  developers.Where(w => w.Experience > vm.ExpirienceMin && w.Experience < vm.ExpirienceMax)
              .ToList();
            });
              

        }

        private Task<List<Developer>> ApllyLevel(SearchDeveloperViewModel vm, List<Developer> developers)
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
        private Task<List<Developer>> ApllyIsFinishedUniversity(SearchDeveloperViewModel vm, List<Developer> developers)
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

        private Task<List<Developer>> Apllytechnologies(SearchDeveloperViewModel vm, List<Developer> developers)
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

        public  async  Task<IActionResult> Profile(int developerId)
        {
            using (_teamLeasingContext)
            {
                 var developer =  _teamLeasingContext.Developers
                                                    .Include(i=>i.Technology)
                                                    .FirstOrDefaultAsync(w => w.Id == developerId);

                 return View("Profile",await developer); 
            }
          
        }
    }
}
