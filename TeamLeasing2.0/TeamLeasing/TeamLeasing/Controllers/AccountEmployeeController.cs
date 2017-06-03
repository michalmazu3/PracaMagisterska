using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.Infrastructure;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Employee;
using TeamLeasing.ViewModels.Employee.Account;

namespace TeamLeasing.Controllers
{
    [Route("account/employee/[action]")]
    public class AccountEmployeeController : Controller
    {
        private readonly OptimizedDbManager _manager;
        private readonly IMapper _mapper;

        public AccountEmployeeController(OptimizedDbManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [Authorize(Roles = "Employee")]
        public IActionResult Edit()
        {
            return View("EditProfile", new EditEmployeeAccountViewModel());
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var userId = _manager.GetUserId(HttpContext.User);
                var result = await _manager.UpdateEmployeeUser(vm, userId);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                return View("_Error", new ErrorViewModel
                {
                    ReturnUrl = Url.Action("Edit", "AccountEmployee"),
                    Message = result.Errors.ToString()
                });
            }

            return View("EditProfile", vm);
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> JobWithApplication()
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var jobList = await _manager.GetJobsForEmployee(userId);
            var vm = _mapper.Map<List<JobWithApplicationsviewModel>>(jobList);

            return View("JobApplications", vm);
        }

        [Authorize(Roles = "Employee")]
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

                var result = await _manager.CreateJob(vm, user);
                if (result)
                    return RedirectToAction("Index", "Home");
                return View("_Error", new ErrorViewModel
                {
                    Message = "Utworzenie oferty pracy nie powiodło się",
                    ReturnUrl = Url.Action("CreateJob", "AccountEmployee")
                });
            }
            return View("CreateJob", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> FinishJob(int jobId)
        {
            var result = await _manager.FinishJob(jobId);
            if (result != 0)
                return RedirectToAction("JobWithApplication", "AccountEmployee");
            return View("_Error", new ErrorViewModel
            {
                Message = "Zakończenie oferty nz przyczyn niewyjaśnionych niepowiodło się",
                ReturnUrl = Url.Action("JobWithApplication", "AccountEmployee")
            });
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> RejectApplication(int jobId, int developerId)
        {
            var result = await _manager.RejectJobApplication(jobId, developerId);
            if (result != 0)
                return RedirectToAction("JobWithApplication", "AccountEmployee");
            return View("_Error", new ErrorViewModel
            {
                Message = "Odrzucenie aplikacji z przyczyn niewyjaśnionych niepowiodło się",
                ReturnUrl = Url.Action("JobWithApplication", "AccountEmployee")
            });
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AcceptAppliaction(int jobId, int developerId)
        {
            var result = await _manager.AcceptJobApplication(jobId, developerId);
            if (result != 0)
                return RedirectToAction("JobWithApplication", "AccountEmployee");
            return View("_Error", new ErrorViewModel
            {
                Message = "Odrzucenie aplikacji z przyczyn niewyjaśnionych niepowiodło się",
                ReturnUrl = Url.Action("JobWithApplication", "AccountEmployee")
            });
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SentOffer()
        {
            var user = await _manager.FindEmployeeUserByIdAsync(_manager.GetUserId(HttpContext.User));
            try
            {
                var sentOfferList = await _manager.GetSentOfferByEmployeeUserId(user.EmployeeUser.Id);
                var vm = _mapper.Map<List<SentOfferViewModel>>(sentOfferList);
                return View("SentOffers", vm);
            }
            catch (Exception e)
            {
                return View("_Error", new ErrorViewModel
                {
                    Message = "Wystąpił nieoczekiwany błąd w aplikacji",
                    ReturnUrl = Url.Action("Index", "Home")
                });
            }
        }



        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> CanceldOffer(int offerId)
        {
            var user = await _manager.FindEmployeeUserByIdAsync(_manager.GetUserId(HttpContext.User));
            try
            {
                var result = await _manager.CancelOfferByEmployee(user.EmployeeUser.Id, offerId);
                if (result)
                    return RedirectToAction("SentOffer");
                return View("_Error", new ErrorViewModel
                {
                    Message = "Wystąpił nieoczekiwany błąd związany z wycofaniem propozycji",
                    ReturnUrl = Url.Action("SentOffer", "AccountEmployee")
                });
            }
            catch (Exception e)
            {
                return View("_Error", new ErrorViewModel
                {
                    Message = "Wystąpił niekoreślony błąd w aplikacji",
                    ReturnUrl = Url.Action("SentOffer", "AccountEmployee")
                });
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> AcceptOffer(int offerId)
        {
            var user = await _manager.FindEmployeeUserByIdAsync(_manager.GetUserId(HttpContext.User));
            try
            {
                var result = await _manager.AcceptOfferByEmployeeUser(user.EmployeeUser.Id, offerId);
                if (result)
                    return RedirectToAction("SentOffer");
                return View("_Error", new ErrorViewModel
                {
                    Message = "Wystąpił nieoczekiwany błąd związany z akceptowaniem propozycji",
                    ReturnUrl = Url.Action("SentOffer", "AccountEmployee")
                });
            }
            catch (Exception e)
            {
                return View("_Error", new ErrorViewModel
                {
                    Message = "Wystąpił niekoreślony błąd w aplikacji",
                    ReturnUrl = Url.Action("SentOffer", "AccountEmployee")
                });
            }
        }
    }
}