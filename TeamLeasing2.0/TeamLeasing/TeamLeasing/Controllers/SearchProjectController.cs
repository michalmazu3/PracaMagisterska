using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Infrastructure.Helper;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Job.SearchJob;
using TeamLeasing.ViewModels.Project;
using TeamLeasing.ViewModels.Project.SearchProject;

namespace TeamLeasing.Controllers
{
    [Route("search")]
    public class SearchProjectController : Controller
    {
        private readonly OptimizedDbManager _manager;
        private readonly IMapper _mapper;
        private readonly ISearchHelper _searchHelper;


        private readonly TeamLeasingContext _teamLeasingContext;
 

        public SearchProjectController(TeamLeasingContext _teamLeasingContext, IMapper mapper,
            OptimizedDbManager manager, ISearchHelper searchHelper)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _mapper = mapper;
            _manager = manager;
            _searchHelper = searchHelper;
        }
        [Route("[action]")]

        public async Task<IActionResult> Projects(List<int> projectsId)
        {
            var projectList = await _manager.GetProject();
            var selectedJobs = !projectsId.Any()
                ? projectList
                : projectList.Join(projectsId, a => a.Id, b => b, (a, b) => a).ToList();

            var vm = _mapper.Map<List<ProjectViewModel>>(selectedJobs);

            return View("ProjectSearch", vm);
        }


        [HttpPost]
        [Route("[action]")]

        public async Task<IActionResult> Projects(SidebarProjectViewModel vm)
        {
            var projects = await _manager.GetProject();
            var searchingProjects = await GetSearchResul(vm, projects);
            TempData.Put("searchProjects", vm);
            return RedirectToAction("Projects", new
            {
                projectsId = new List<int>(searchingProjects.Select(s => s.Id)
                    .ToList())
            });
        }

        private async Task<List<Project>> GetSearchResul(SidebarProjectViewModel vm,
            List<Project> projects)
        {
            return await Task.Run(async () =>
            {
                var type = _searchHelper.Aplly(w => vm.ProjectTypeValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.ProjectType), projects);


                var budget = _searchHelper.Aplly(w => w.Budget > vm.BudgetMin
                                                      && w.Budget < vm.BudgetMax, projects);

                var searchingResult = await _searchHelper.Intersection(type, budget);

                return searchingResult.ToList();
            });
        }

        [Route("[action]/{projectId}")]
        public async Task<IActionResult> Project(int projectId, bool withHidden = false)
        {
            var job = await _manager.GetProject(w => w.Id == projectId, withHidden);
            var vm = _mapper.Map<ProjectViewModel>(job.FirstOrDefault());

            return View("ProjectDetails", vm);
        }

        [Authorize(Roles = "Developer")]
        [HttpPost]
        public async Task<IActionResult> ApplyForProject(int jobId)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var result = await _manager.ApplyForJob(userId, jobId);

            if (result == 0)
                return View("_Error", new ErrorViewModel
                {
                    Message = "Aplikcja na tą oferte pracy nie powiodła się z przyczyn niewyjaśnionych",
                    ReturnUrl = Url.Action("Jobs", "SearchJob")
                });
            ViewBag.ApplyForJob = result == 1
                ? "Gratulacje, wysłałeś aplikacje na to stanowisko!"
                : "Już wysłałes aplikacje na to stanowisko!";
            var job = await _manager.GetJob(w => w.Id == jobId);
            var vm = _mapper.Map<JobViewModel>(job.FirstOrDefault());

            return View("ProjectDetails", null);
        }
    }
}

