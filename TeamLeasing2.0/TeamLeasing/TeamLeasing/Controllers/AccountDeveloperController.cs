using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService;
using System.Security.Principal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.Infrastructure;
using TeamLeasing.Services.UploadService;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;
namespace TeamLeasing.Controllers
{
    public class AccountDeveloperController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly OptimizedUserManager _manager;
        private readonly IConfigurationService _configurationService;
        private readonly IUploadService _uploadService;
        private readonly IHostingEnvironment _environment;

        public AccountDeveloperController(TeamLeasingContext teamLeasingContext, OptimizedUserManager manager,
                    IConfigurationService configurationService,
                    IUploadService uploadService, IHostingEnvironment environment)
        {
            this._teamLeasingContext = teamLeasingContext;
            _manager = manager;
            _configurationService = configurationService;
            _uploadService = uploadService;
            _environment = environment;
        }
        //  [Route("account/[action]")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit()
        {
            EditDeveloperProfileViewModel vm = await PrepareViewModel();

            return View("EditProfile", vm);
        }

        private async Task<EditDeveloperProfileViewModel> PrepareViewModel()
        {
            EditDeveloperProfileViewModel editvm = new EditDeveloperProfileViewModel();
            string userId = _manager.GetUserId(HttpContext.User);
            User user = await _manager.FindDeveloperUserByIdAsync(userId);

            // editvm = await GetCurrentUserFile();
            editvm.Cv = user.DeveloperUser.Cv;
            editvm.Phone = user.DeveloperUser.Photo;
            editvm.Technologies = await _configurationService.GetTechnology().GetSelectList();
            editvm.Levels = _configurationService.GetLevel().GetSelectList();
            editvm.IsFinishedUnivesity = _configurationService.GetIsFinishedUniversity().GetSelectList();
            editvm.Province = _configurationService.GetProvince().GetSelectList();

            return editvm;
        }

        public async Task<EditDeveloperProfileViewModel> GetCurrentUserFile()
        {
            string username = _manager.GetUserId(HttpContext.User);
           User user =  _manager.Users.Include(i => i.DeveloperUser).First(f => f.UserName == username);
          
            

            return new EditDeveloperProfileViewModel()
            {
                Cv = user.DeveloperUser.Cv,
                Photo = user.DeveloperUser.Photo,

            };
        }

        [HttpPost]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit(EditDeveloperProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UpdateUserformation(vm);

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

        private async Task<User> UpdateUserformation(EditDeveloperProfileViewModel vm)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var user = await _manager.Users.Include(i => i.DeveloperUser)
                                            .ThenInclude(t => t.Technology)
                                            .FirstOrDefaultAsync(f => f.Id == userId);


            await UpdateDeveloperUserInformation(vm, user.DeveloperUser);

            user.PhoneNumber = vm.Phone ?? user.PhoneNumber;

            return user;
        }

        private async Task UpdateDeveloperUserInformation(EditDeveloperProfileViewModel vm, DeveloperUser developerUser)
        {
            developerUser.Technology = vm.ChoosenTechnology != null ? await _teamLeasingContext.Technologies
                                                                                              .FirstOrDefaultAsync(f => f.Name == vm.ChoosenTechnology)
                                                                  : developerUser.Technology;

            developerUser.Experience = vm.Experience == null || vm.Experience < 0 ? developerUser.Experience : (int)vm.Experience;
            developerUser.City = vm.City ?? developerUser.City;
            developerUser.Province = string.IsNullOrEmpty(vm.ChoosenProvince)
                ? developerUser.Province
                : (Enums.Province)Enum.Parse(typeof(Enums.Province), vm.ChoosenProvince);


            developerUser.IsFinishedUniversity = string.IsNullOrEmpty(vm.ChoosenIsFinishedUnivesity)
                                                        ? developerUser.IsFinishedUniversity
                                                        : (Enums.IsFinishedUniversity)Enum.Parse(typeof(Enums.IsFinishedUniversity), vm.ChoosenIsFinishedUnivesity);


            developerUser.Level = string.IsNullOrEmpty(vm.ChoosenLevel)
                                        ? developerUser.Level
                                        : (Enums.Level)Enum.Parse(typeof(Enums.Level), vm.ChoosenLevel);

            developerUser.University = vm.University ?? developerUser.University;
            developerUser.About = vm.About ?? developerUser.About;
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
                    user.DeveloperUser.Cv = await _uploadService.UploadCvFile(user.DeveloperUser.Name, user.DeveloperUser.Surname, cvFile);
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
                    user.DeveloperUser.Photo = await _uploadService.UploadPhotoFile(user.DeveloperUser.Name, user.DeveloperUser.Surname, photoFile);
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
                    ReturnUrl = UrlHelperExtensions.Action(Url, "Edit", "AccountDeveloper"),
                });
            }

            return RedirectToAction("Edit", "AccountDeveloper");

        }
    }
}