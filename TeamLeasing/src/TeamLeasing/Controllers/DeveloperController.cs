using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        }

        // GET: /<controller>/
        public async Task<IActionResult> Search()
        {
            var developers = await _teamLeasingContext.Developers.Include(i => i.Technology).ToListAsync();
            return View(developers);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchDeveloperViewModel vm)
        {
            List<Developer> developers = new List<Developer>();

            var tech = Apllytechnologies(vm);
            var level = ApllyLevel(vm);
            var isFinishedUniversity = ApllyIsFinishedUniversity(vm);
            var experience = ApllyExperience(vm);

           // developers = Intersection(tech, level, (t, l)=>  );

            return View("Search", developers);
        }

       

        private IEnumerable<Developer> Intersection(IEnumerable<Developer> d1, IEnumerable<Developer> d2,
            Func<IEnumerable<Developer>, IEnumerable<Developer>, List<Developer>> check)
        {

           return check(d1, d2);

        }

   

        private IEnumerable<Developer> ApllyExperience(SearchDeveloperViewModel vm)
        {
            return _teamLeasingContext.Developers.Include(i => i.Technology)
                .Where(w=>w.Experience>vm.ExpirienceMin && w.Experience<vm.ExpirienceMax)
                .ToList();
        }

        private IEnumerable<Developer> ApllyLevel(SearchDeveloperViewModel vm)
        {
            return _teamLeasingContext.Developers.Include(i => i.Technology)
                .Where(
                    w => vm.LevelNameValuePairs.Where(v => v.Value)
                                                .Select(s => s.Name)
                                                .Contains(w.Level))
                .ToList();
        }
        private IEnumerable<Developer> ApllyIsFinishedUniversity(SearchDeveloperViewModel vm)
        {

            return _teamLeasingContext.Developers.Include(i => i.Technology)
                 .Where(
                     w => vm.UniversityNameValuePairs.Where(v => v.Value)
                                                      .Select(s => s.Name)
                                                      .Contains(w.IsFinishedUniversity))
                  .ToList();
        }

        private IEnumerable<Developer> Apllytechnologies(SearchDeveloperViewModel vm)
        {
            return _teamLeasingContext.Developers.Include(i => i.Technology)
                   .Where(
                       w => vm.TechnologyNameValuePairs.Where(v => v.Value)
                                                        .Select(s => s.Name)
                                                        .Contains(w.Technology.Name))
                   .ToList();
        }

        public async Task<IActionResult> Profile(int developerId)
        {
            var developer = await _teamLeasingContext.Developers
                .Include(i => i.Technology)
                .Where(w => w.Id == developerId)
                .FirstOrDefaultAsync();

            return View("Profile", developer);
        }
    }
}
