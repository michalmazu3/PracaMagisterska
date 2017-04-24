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
        private readonly IHostingEnvironment _environment;


        public RegistrationController(UserManager<User> manager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper, TeamLeasingContext teamLeasingContext,
            IHostingEnvironment environment)
        {
            _manager = manager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _teamLeasingContext = teamLeasingContext;
            _environment = environment;
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
                return await CreateUser(vm);

            }
            return View("Registration", vm);

        }

        private async Task<IActionResult> CreateUser(RegistrationDeveloperViewModel vm)
        {
            User user = new User();
            user = _mapper.Map<User>(vm);

            var result = await _manager.CreateAsync(user, vm.Password);
            if (result.Succeeded){

                var newUser = _teamLeasingContext.Users.Find(user.Id);
                DeveloperUser developerUser =  await CreateDeveloperUser(vm, newUser);


                await _teamLeasingContext.DeveloperUsers.AddAsync(developerUser);
                await _teamLeasingContext.SaveChangesAsync();
                await _manager.AddToRoleAsync(_teamLeasingContext.Users.Find(user.Id), Roles.Developer.ToString());

               await _signInManager.SignInAsync(_teamLeasingContext.Users.Find(user.Id), true, null);
               return RedirectToAction("Index", "Home");

            }

            throw new Exception(message: "User creating error");

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

            developerUser.Cv = await CopyCvFile(vm.Name, vm.Surname, vm.CvFile);
            developerUser.Photo = await CopyPhotoFile(vm.Name, vm.Surname, vm.PhotoFile);
            return developerUser;
        }

        private async Task<string> CopyPhotoFile(string name, string surname, IFormFile photoFile)
        {

            var pathToFile = $"UploadFile/Photo/{surname}_{name}.jpg";
            var uploadCv = Path.Combine(_environment.WebRootPath, pathToFile);

            if (photoFile != null)
            {
                using (var fileStream = new FileStream(uploadCv, FileMode.Create))
                {
                    await photoFile.CopyToAsync(fileStream);
                }
                return pathToFile;
            }

            throw new Exception(message: "Bład wgrywania pliku");
        }

        private async Task<string> CopyCvFile(string Name, string Surname, IFormFile cvFile)
        {
            var pathToFile = $"UploadFile/Cv/{Surname}_{Name}.pdf";
            var uploadCv = Path.Combine(_environment.WebRootPath, pathToFile);

            if (cvFile != null)
            {
                using (var fileStream = new FileStream(uploadCv, FileMode.Create))
                {
                    await cvFile.CopyToAsync(fileStream);
                }
                return pathToFile;
            }

            throw new Exception(message: "Bład wgrywania pliku");
        }
    }
}
