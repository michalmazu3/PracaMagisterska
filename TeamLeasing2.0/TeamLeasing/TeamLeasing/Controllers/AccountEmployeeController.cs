using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.Infrastructure;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Employee.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TeamLeasing.Models;

namespace TeamLeasing.Controllers
{
    [Route("account/employee/[action]")]
    public class AccountEmployeeController : Controller
    {
        private readonly OptimizedDbManager _manager;
        private readonly IMapper _mapper;

        public AccountEmployeeController(OptimizedDbManager manager, IMapper mapper  )
        {
            _manager = manager;
            _mapper = mapper;
        }

        
        public IActionResult Edit()
        {
            return View("EditProfile", new EditEmployeeAccountViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId =  _manager.GetUserId(HttpContext.User);
                var result = await _manager.UpdateEmployeeUser(vm, userId);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Edit", "AccountEmployee"),
                        Message = result.Errors.ToString(),
                    });
                }
            }

            return View("EditProfile", vm);
        }

        public async Task<IActionResult> JobWithApplication()
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var jobList = await _manager.GetJobsForEmployee(userId);
            var vm = _mapper.Map<List<JobWithApplicationsviewModel>>(jobList);

            return View("JobApplications", vm);
        }

        public IActionResult CreateJob()
        {
            return View("CreateJob", new CreateJobViewModel());
        }
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateJob(CreateJobViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = _manager.GetUserId(HttpContext.User);
                var user = await _manager.FindEmployeeUserByIdAsync(userId);

                var result = await _manager.CreateJob(vm,user);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = "Utworzenie oferty pracy nie powiodło się",
                        ReturnUrl = UrlHelperExtensions.Action(Url,"CreateJob","AccountEmployee"),
                    });
                }
                
                 
            }
            else
            {
                return View("CreateJob", vm);
            }
        }

    }
}