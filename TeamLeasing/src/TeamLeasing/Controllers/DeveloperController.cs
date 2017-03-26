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
            var developers =await _teamLeasingContext.Developers.Include(i=>i.Technology).ToListAsync();
            return View(developers);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchDeveloperViewModel vm)
        {
            var developers = await _teamLeasingContext.Developers
                .Include(c => c.Technology)
                .Where(r => r.Experience < vm.ExpirienceMax && r.Experience > vm.ExpirienceMin)
                .ToListAsync();
            return View("Search",developers);
        }
        
        public async Task< IActionResult> Profile(int developerId)
        {
            var developer = await _teamLeasingContext.Developers
                .Include(i => i.Technology)
                .Where(w => w.Id == developerId)
                .FirstOrDefaultAsync();

            return View("Profile",developer);
        }
    }
}
