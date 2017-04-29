using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Identity;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService;
using System.Security.Principal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.Infrastructure;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.Services.UploadService;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
using TeamLeasing.ViewModels.Developer.Account;

namespace TeamLeasing.Controllers
{
    public class AccountDeveloperController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly OptimizedUserManager _manager;
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IUploadService _uploadService;
        private readonly IHostingEnvironment _environment;

        public AccountDeveloperController(TeamLeasingContext _teamLeasingContext, OptimizedUserManager manager, IHttpContextAccessor contextAccessor,
            IUploadService uploadService, IHostingEnvironment environment)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _manager = manager;
            _contextAccessor = contextAccessor;

            _uploadService = uploadService;
            _environment = environment;
        }
        //  [Route("account/[action]")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit()
        {
           EditDeveloperAccountViewModel vm = await GetCurrentUserFile();

            return View("EditProfile", vm);
        }

        public async Task<EditDeveloperAccountViewModel> GetCurrentUserFile()
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var user = await _manager.FindDeveloperUserByIdAsync(userId);

        
            return new EditDeveloperAccountViewModel()
            {
                Cv = new Cv(){CvPath = user.DeveloperUser.Cv },
                Photo = new Photo(){PhotoPath = user.DeveloperUser.Photo},

            };
        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit(EditDeveloperAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UpdateUserformation(vm.BasicInformation);

                var result = await _manager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Edit", "AccountDeveloper"),
                        Message = result.Errors.ToString(),
                    });
                }
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
            developerUser.Technology = informationm.ChoosenTechnology != null ? await _teamLeasingContext.Technologies
                                                                                              .FirstOrDefaultAsync(f => f.Name == informationm.ChoosenTechnology)
                                                                  : developerUser.Technology;

            developerUser.Experience = informationm.Experience == null || informationm.Experience < 0 ? developerUser.Experience : (int)informationm.Experience;
            developerUser.City = informationm.City ?? developerUser.City;
            developerUser.Province = string.IsNullOrWhiteSpace(informationm.ChoosenProvince)? developerUser.Province 
                                                                                  : informationm.ChoosenProvince ;

            developerUser.IsFinishedUniversity = string.IsNullOrEmpty(informationm.ChoosenIsFinishedUnivesity)
                                                        ? developerUser.IsFinishedUniversity
                                                        : (Enums.IsFinishedUniversity)Enum.Parse(typeof(Enums.IsFinishedUniversity), informationm.ChoosenIsFinishedUnivesity);


            developerUser.Level = string.IsNullOrEmpty(informationm.ChoosenLevel)
                                        ? developerUser.Level
                                        : (Enums.Level)Enum.Parse(typeof(Enums.Level), informationm.ChoosenLevel);

            developerUser.University = informationm.University ?? developerUser.University;
            developerUser.About = informationm.About ?? developerUser.About;
            _teamLeasingContext.DeveloperUsers.Update(developerUser);
            await _teamLeasingContext.SaveChangesAsync();

        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
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
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = e.Message,
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Edit", "AccountDeveloper"),
                    });
                }
            }

            return RedirectToAction("Edit", "AccountDeveloper");

        }


        [HttpPost]
        [Authorize(Roles = "Developer")]
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
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = e.Message,
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Edit", "AccountDeveloper"),
                    });
                }
            }

            return RedirectToAction("Edit", "AccountDeveloper");

        }


        [Authorize(Roles = "Developer")]
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
                return View("_Error", new ErrorViewModel()
                {
                    Message = e.Message,
                    ReturnUrl = UrlHelperExtensions.Action(Url, "Edit","AccountDeveloper"),
                });
            }

            return RedirectToAction("Edit", "AccountDeveloper");

        }
    }
}