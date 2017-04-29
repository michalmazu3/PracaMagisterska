using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
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

        public SearchJobController (TeamLeasingContext _teamLeasingContext, IMapper mapper, OptimizedDbManager manager)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _mapper = mapper;
            _manager = manager;
        }

        [Route("[action]")]
        public  async Task<IActionResult> Jobs(List<int> jobsId)
        {
            var jobList = await _manager.GetJob();
            var selectedJobs = !jobsId.Any() ? jobList
                : jobList.Join(jobsId, a => a.Id, b => b, (a, b) => a).ToList();

            List<JobViewModel> vm = _mapper.Map<List<JobViewModel>>(selectedJobs);

            return View("JobSearch",vm);
        }




        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Jobs(SidebarEmployeeViewModel vm)
        {
            return RedirectToAction("Jobs", new List<int>());
        }
    }
}
