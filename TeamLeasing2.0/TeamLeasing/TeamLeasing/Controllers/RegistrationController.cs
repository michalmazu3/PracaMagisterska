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
using TeamLeasing.Models;
using TeamLeasing.Models;
using TeamLeasing.Services.UploadService;
using TeamLeasing.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamLeasing.Controllers
{
    [Route("/registration")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> _manager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IUploadService _uploadService;


        public RegistrationController(UserManager<User> manager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper, TeamLeasingContext teamLeasingContext,
            IUploadService uploadService)
        {
            _manager = manager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _teamLeasingContext = teamLeasingContext;
            _uploadService = uploadService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("Registration");
        }

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
            return View("Registration", vm);

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

            developerUser.Cv = await _uploadService.UploadCvFile(vm.Name, vm.Surname, vm.CvFile);
            developerUser.Photo = await _uploadService.UploadPhotoFile(vm.Name, vm.Surname, vm.PhotoFile);
            return developerUser;
        }


    }
}
