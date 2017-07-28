using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure;
using TeamLeasing.Models;
using TeamLeasing.Models;
using TeamLeasing.Services.UploadService;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Employee;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly OptimizedDbManager _manager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IUserService _userService;


        public RegistrationController(OptimizedDbManager manager, SignInManager<User> signInManager,
           IMapper mapper, TeamLeasingContext teamLeasingContext,
            IUserService userService)
        {
            _manager = manager;
            _signInManager = signInManager;
            _mapper = mapper;
            _teamLeasingContext = teamLeasingContext;
            _userService = userService;
        }

        [Route("[controller]")]
        public IActionResult Index()
        {
            return View("Registration", new RegistrationViewModel());
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Developer(RegistrationDeveloperViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await CreateUser(vm);
                }
                catch (Exception e)
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = e.Message,
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Index", "Registration"),

                    });
                }

            }
            return View("Registration", new RegistrationViewModel() { RegistrationDeveloperViewModel = vm });

        }

        private async Task<IActionResult> CreateUser(RegistrationDeveloperViewModel vm)
        {
            User user = new User();
            user = _mapper.Map<User>(vm);

            var result = await _manager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                var newUser = _teamLeasingContext.Users.Find(user.Id);
                DeveloperUser developerUser = await CreateDeveloperUser(vm, newUser);

                await _teamLeasingContext.DeveloperUsers.AddAsync(developerUser);
                await _teamLeasingContext.SaveChangesAsync();
                await _manager.AddToRoleAsync(_teamLeasingContext.Users.Find(user.Id), Roles.Developer.ToString());

                await _signInManager.SignInAsync(_teamLeasingContext.Users.Find(user.Id), true, null);
                return RedirectToAction("Index", "Home");
            }

            throw new Exception(message: "Utworzenie użytkownka z przyczyn niewyjaśnionych nie powiodło się");
        }

        private async Task<DeveloperUser> CreateDeveloperUser(RegistrationDeveloperViewModel vm, User user)
        {
            DeveloperUser developerUser = new DeveloperUser();
            developerUser = _mapper.Map<DeveloperUser>(vm);
            developerUser.Technology = _teamLeasingContext.Technologies
                .Where(t => t.Name.ToLower() == vm.ChoosenTechnology.ToLower())
                .ToList()
                .FirstOrDefault();
            developerUser.UserId = user.Id;
            if (vm.CvFile != null)
            {
                developerUser.Cv = await _userService.UploadService.UploadCvFile(vm.Name, vm.Surname, vm.CvFile);
            }
            if (vm.PhotoFile != null)
            {
                developerUser.Photo = await _userService.UploadService.UploadPhotoFile(vm.Name, vm.Surname, vm.PhotoFile);
            }
            return developerUser;
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Employee(RegistrationEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _manager.CreateEmployeeUser(vm);
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, true, null);
                        return RedirectToAction("Index", "Home");
                    }

                }
                catch (Exception e)
                {
                    return View("_Error", new ErrorViewModel()
                    {
                        Message = e.Message,
                        ReturnUrl = UrlHelperExtensions.Action(Url, "Index", "Registration"),
                    });
                }
            }
            return View("Registration", new RegistrationViewModel() { RegistrationEmployeeViewModel = vm });
        }
    }
}
