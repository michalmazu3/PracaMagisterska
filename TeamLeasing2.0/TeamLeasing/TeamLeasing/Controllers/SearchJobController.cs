using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Infrastructure.Helper;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Job;
using TeamLeasing.ViewModels.Job.SearchJob;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Models
{
    [Route("search")]
    public class SearchJobController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IMapper _mapper;
        private readonly OptimizedDbManager _manager;
        private readonly ISearchHelper _searchHelper;

        public SearchJobController(TeamLeasingContext _teamLeasingContext, IMapper mapper,
            OptimizedDbManager manager, ISearchHelper searchHelper)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _mapper = mapper;
            _manager = manager;
            _searchHelper = searchHelper;
        }

        [Route("[action]")]
        public async Task<IActionResult> Jobs(List<int> jobsId)
        {
            var jobList = await _manager.GetJob();
            var selectedJobs = !jobsId.Any() ? jobList
                : jobList.Join(jobsId, a => a.Id, b => b, (a, b) => a).ToList();

            List<JobViewModel> vm = _mapper.Map<List<JobViewModel>>(selectedJobs);

            return View("JobSearch", vm);
        }




        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Jobs(SidebarEmployeeViewModel vm)
        {
            List<Job> jobs = await _manager.GetJob();
            List<Job> searchingJobs = await GetSearchResul(vm, jobs);
            TempData.Put("searchJobs", vm);
            return RedirectToAction("Jobs", new
            {
                jobsId = new List<int>(searchingJobs.Select(s => s.Id)
                    .ToList())
            });
        }
        private async Task<List<Job>> GetSearchResul(SidebarEmployeeViewModel vm,
            List<Job> developers)
        {
            return await Task.Run(async () =>
            {
                var tech = _searchHelper.Aplly<Job>(w => vm.TechnologyNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.Technology.Name), developers);

                var level = _searchHelper.Aplly<Job>(w => vm.LevelNameValuePairs
                    .Where(s => s.Value)
                    .Select(s => s.Name)
                    .Contains(w.Level), developers);

                var experience = _searchHelper.Aplly<Job>(w => w.Price > vm.SalaryMin
                                                                         && w.Price < vm.SalaryMax, developers);

                var searchingResult = await _searchHelper.Intersection<Job>(tech, level, experience);

                return searchingResult.ToList();
            });

        }
        [Route("[action]/{jobId}")]
        public async Task<IActionResult> Job(int jobId, bool withHidden = false)
        {
            var job = await _manager.GetJob(w => w.Id == jobId, withHidden);
            var vm = _mapper.Map<JobViewModel>(job.FirstOrDefault());

            return View("JobDetails", vm);

        }

        [Authorize(Roles = "Developer")]
        [HttpPost]
        [Route("[action]/{jobId}")]
        public async Task<IActionResult> ApplyForJob(int jobId)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var result = await _manager.ApplyForJob(userId, jobId);

            if (result == 0)
            {
                return View("_Error", new ErrorViewModel()
                {
                    Message = "Aplikcja na tą oferte pracy nie powiodła się z przyczyn niewyjaśnionych",
                    ReturnUrl = UrlHelperExtensions.Action(Url, "Jobs", "SearchJob"),
                });
              
            }
            else
            {
                ViewBag.ApplyForJob = result == 1
                    ? "Gratulacje, wysłałeś aplikacje na to stanowisko!"
                    : "Już wysłałes aplikacje na to stanowisko!";
                var job = await _manager.GetJob(w => w.Id == jobId);
                var vm = _mapper.Map<JobViewModel>(job.FirstOrDefault());

                return View("JobDetails", vm);

            }
        }


    }
}
