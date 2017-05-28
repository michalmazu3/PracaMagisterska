using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
using TeamLeasing.Models;
using TeamLeasing.Services.UploadService;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Developer.Account;

namespace TeamLeasing.Controllers
{
    [Route("account")]
    public class AccountDeveloperController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHostingEnvironment _environment;
        private readonly OptimizedDbManager _manager;
        private readonly IMapper _mapper;
        private readonly TeamLeasingContext _teamLeasingContext;

        private readonly IUploadService _uploadService;

        public AccountDeveloperController(TeamLeasingContext teamLeasingContext,
            OptimizedDbManager manager, IHttpContextAccessor contextAccessor, IMapper mapper,
            IUploadService uploadService, IHostingEnvironment environment)
        {
            _teamLeasingContext = teamLeasingContext;
            _manager = manager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;

            _uploadService = uploadService;
            _environment = environment;
        }

        [Route("[action]")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit()
        {
            var vm = await GetCurrentUserFile();

            return View("EditProfile", vm);
        }


        private async Task<EditDeveloperAccountViewModel> GetCurrentUserFile()
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var user = await _manager.FindDeveloperUserByIdAsync(userId);


            return new EditDeveloperAccountViewModel
            {
                Cv = new Cv {CvPath = user.DeveloperUser.Cv},
                Photo = new Photo {PhotoPath = user.DeveloperUser.Photo}
            };
        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
        [Route("[action]")]
        public async Task<IActionResult> Edit(EditDeveloperAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UpdateUserformation(vm.BasicInformation);

                var result = await _manager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                return View("_Error", new ErrorViewModel
                {
                    ReturnUrl = Url.Action("Edit", "AccountDeveloper"),
                    Message = result.Errors.ToString()
                });
            }

            return View("EditProfile", vm);
        }

        private async Task<User> UpdateUserformation(BasicInformation information)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var user = await _manager.Users.Include(i => i.DeveloperUser)
                .ThenInclude(t => t.Technology)
                .FirstOrDefaultAsync(f => f.Id == userId);


            await UpdateDeveloperUserInformation(information, user.DeveloperUser);

            user.PhoneNumber = information.Phone ?? user.PhoneNumber;

            return user;
        }

        private async Task UpdateDeveloperUserInformation(BasicInformation informationm, DeveloperUser developerUser)
        {
            developerUser.Technology = informationm.ChoosenTechnology != null
                ? await _teamLeasingContext.Technologies
                    .FirstOrDefaultAsync(f => f.Name == informationm.ChoosenTechnology)
                : developerUser.Technology;

            developerUser.Experience = informationm.Experience == null || informationm.Experience < 0
                ? developerUser.Experience
                : (int) informationm.Experience;
            developerUser.City = informationm.City ?? developerUser.City;
            developerUser.Province = string.IsNullOrWhiteSpace(informationm.ChoosenProvince)
                ? developerUser.Province
                : informationm.ChoosenProvince;

            developerUser.IsFinishedUniversity = string.IsNullOrEmpty(informationm.ChoosenIsFinishedUnivesity)
                ? developerUser.IsFinishedUniversity
                : (Enums.IsFinishedUniversity) Enum.Parse(typeof(Enums.IsFinishedUniversity),
                    informationm.ChoosenIsFinishedUnivesity);


            developerUser.Level = string.IsNullOrEmpty(informationm.ChoosenLevel)
                ? developerUser.Level
                : (Enums.Level) Enum.Parse(typeof(Enums.Level), informationm.ChoosenLevel);

            developerUser.University = informationm.University ?? developerUser.University;
            developerUser.About = informationm.About ?? developerUser.About;
            _teamLeasingContext.DeveloperUsers.Update(developerUser);
            await _teamLeasingContext.SaveChangesAsync();
        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
        [Route("[action]")]
        public async Task<IActionResult> UploadCV(IFormFile cvFile)
        {
            if (ModelState.IsValid)
            {
                var userId = _manager.GetUserId(HttpContext.User);
                var user = await _manager.Users.Include(i => i.DeveloperUser)
                    .ThenInclude(t => t.Technology)
                    .FirstOrDefaultAsync(f => f.Id == userId);
                try
                {
                    user.DeveloperUser.Cv = await _uploadService.UploadCvFile(user.DeveloperUser.Name
                        , user.DeveloperUser.Surname, cvFile, !string.IsNullOrEmpty(user.DeveloperUser.Cv));
                    _teamLeasingContext.DeveloperUsers.Update(user.DeveloperUser);

                    await _teamLeasingContext.SaveChangesAsync();
                    return RedirectToAction("Edit", "AccountDeveloper");
                }
                catch (Exception e)
                {
                    return View("_Error", new ErrorViewModel
                    {
                        Message = e.Message,
                        ReturnUrl = Url.Action("Edit", "AccountDeveloper")
                    });
                }
            }

            return RedirectToAction("Edit", "AccountDeveloper");
        }


        [HttpPost]
        [Authorize(Roles = "Developer")]
        [Route("[action]")]
        public async Task<IActionResult> UploadPhoto(IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {
                var userId = _manager.GetUserId(HttpContext.User);
                var user = await _manager.Users.Include(i => i.DeveloperUser)
                    .ThenInclude(t => t.Technology)
                    .FirstOrDefaultAsync(f => f.Id == userId);
                try
                {
                    user.DeveloperUser.Photo = await _uploadService.UploadPhotoFile(user.DeveloperUser.Name
                        , user.DeveloperUser.Surname, photoFile, !string.IsNullOrEmpty(user.DeveloperUser.Photo));
                    _teamLeasingContext.DeveloperUsers.Update(user.DeveloperUser);
                    await _teamLeasingContext.SaveChangesAsync();
                    return RedirectToAction("Edit", "AccountDeveloper");
                }
                catch (Exception e)
                {
                    return View("_Error", new ErrorViewModel
                    {
                        Message = e.Message,
                        ReturnUrl = Url.Action("Edit", "AccountDeveloper")
                    });
                }
            }

            return RedirectToAction("Edit", "AccountDeveloper");
        }


        [Authorize(Roles = "Developer")]
        [Route("[action]")]
        public async Task<IActionResult> DeletePhoto()
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var user = await _manager.Users.Include(i => i.DeveloperUser)
                .FirstOrDefaultAsync(f => f.Id == userId);
            try
            {
                var pathToFile = _environment.WebRootPath + user.DeveloperUser.Photo;
                System.IO.File.Delete(pathToFile);
                user.DeveloperUser.Photo = "/UploadFile/Photo/default.jpg";
                _teamLeasingContext.DeveloperUsers.Update(user.DeveloperUser);
                await _teamLeasingContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return View("_Error", new ErrorViewModel
                {
                    Message = e.Message,
                    ReturnUrl = Url.Action("Edit", "AccountDeveloper")
                });
            }

            return RedirectToAction("Edit", "AccountDeveloper");
        }

        [Route("[action]")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Applications()
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var jobList = await _manager.GetJobsForDeveloperByUserId(userId);
            var vm = _mapper.Map<List<ApplicationViewModel>>(jobList);

            return View("Application", vm);
        }

        [Route("[action]/{jobId}")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Resign(int jobId)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var result = await _manager.ResignJobApplication(userId, jobId);
            if (result != 0)
                return RedirectToAction("Applications", "AccountDeveloper");
            return View("_Error", new ErrorViewModel
            {
                ReturnUrl = Url.Action("Applications", "AccountDeveloper"),
                Message = "Wycofanie aplikacji nie powiodło się z przyczyn niewyjaśnionych"
            });
        }

        [Authorize(Roles = "Developer")]
        [HttpPost]
        [Route("[action]/{jobId}")]
        public async Task<IActionResult> Apply(int jobId)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var result = await _manager.ApplyForJob(userId, jobId);
            if (result == 1)
                return RedirectToAction("Applications", "AccountDeveloper");
            return View("_Error", new ErrorViewModel
            {
                Message = "Aplikcja na tą oferte pracy nie powiodła się z przyczyn niewyjaśnionych",
                ReturnUrl = Url.Action("Jobs", "SearchJob")
            });
        }

        [Authorize(Roles = "Developer")]
        [Route("[action]")]
        public async Task<IActionResult> RecivedOffers()
        {
            var user = await _manager.FindDeveloperUserByIdAsync(_manager.GetUserId(HttpContext.User));
            var offerList = await _manager.GetRecivedOfferByDeveloperUserId(user.DeveloperUser.Id);
            var vm = _mapper.Map<List<RecivedOfferViewModel>>(offerList);
            return View("RecivedOffers", vm);
        }
    }
}