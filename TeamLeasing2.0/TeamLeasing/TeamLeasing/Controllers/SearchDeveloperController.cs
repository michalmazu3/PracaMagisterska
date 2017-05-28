using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Infrastructure.Helper;
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
        private readonly ISearchHelper _searchHelper;


        public SearchDeveloperController(TeamLeasingContext teamLeasingContext, OptimizedDbManager manager
            , IHostingEnvironment environment, ISearchHelper searchHelper)
        {
            _teamLeasingContext = teamLeasingContext;
            _manager = manager;
            _environment = environment;
            _searchHelper = searchHelper;
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
        private async Task<List<DeveloperUser>> GetSearchResul(SidebarDeveloperViewModel vm,
            List<DeveloperUser> developers)
        {
            return await Task.Run(async () =>
            {
                var tech = _searchHelper.Aplly<DeveloperUser>(w => vm.TechnologyNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.Technology.Name), developers);

                var level = _searchHelper.Aplly<DeveloperUser>(w => vm.LevelNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.Level), developers);

                var isFinishedUniversity = _searchHelper.Aplly<DeveloperUser>(w => vm.UniversityNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.IsFinishedUniversity), developers);

                var experience = _searchHelper.Aplly<DeveloperUser>(w => w.Experience > vm.ExpirienceMin
                                                                         && w.Experience < vm.ExpirienceMax, developers);

                var searchingResult = await _searchHelper.Intersection<DeveloperUser>(tech, level, isFinishedUniversity, experience);

                return searchingResult.ToList();
            });

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





        [Route("developer/{developerId}")]
        // [HttpGet("{developerId}")]
        public async Task<IActionResult> Profile(int developerId, string message = null)
        {

            var developer = await _teamLeasingContext.DeveloperUsers
                                                    .Include(i => i.Technology)
                                                    .FirstOrDefaultAsync(w => w.Id == developerId);
            ViewBag.SendOffer = message ?? String.Empty;
            return View("DeveloperProfile", developer);

          
        }
        [Authorize(Roles = "Employee")]
        [Route("developer/{developerId}/[action]")]
        public async Task<IActionResult> Offer(int developerId)
        {
           
            return View("SendOffer", new SendingOfferViewModel() {DeveloperUserId = developerId});
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        [Route("developer/{developerId}/[action]")]
        public async Task<IActionResult> SendOffer(SendingOfferViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _manager.GetUserId(HttpContext.User);
                    if (await _manager.SendOffer(userId, vm))
                    {
                        return RedirectToAction("Profile", new {developerId= vm.DeveloperUserId, message = "Wysłąłeś propozycje pracy do tego developera!" });
                    }
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = "Wysłanie propozycji pracy nie powiodło się",
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Profile", "SearchDeveloper", new { developerId = vm.DeveloperUserId })
                    });
                }
                catch (Exception e)
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = "Wystąpił błąd nieokreslony błąd w aplikajci",
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Profile", "SearchDeveloper", new { developerId = vm.DeveloperUserId })
                    });
                }
            }
            return View("SendOffer", vm);
        }
    }
}
