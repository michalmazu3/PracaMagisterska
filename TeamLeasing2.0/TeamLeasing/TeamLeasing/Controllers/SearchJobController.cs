using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.ViewModels.Job.SearchJob;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Models
{
    public class SearchJobController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IMapper _mapper;

        public SearchJobController (TeamLeasingContext _teamLeasingContext, IMapper mapper)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public  async Task<IActionResult> Search()
        {
            var jobList = await _teamLeasingContext.Jobs.Include(i => i.EmployeeUser)
                .Include(j => j.Technology)
                .ToListAsync();

            List<JobViewModel> vm = _mapper.Map<List<JobViewModel>>(jobList);

            return View("JobSearch",vm);
        }
    }
}
