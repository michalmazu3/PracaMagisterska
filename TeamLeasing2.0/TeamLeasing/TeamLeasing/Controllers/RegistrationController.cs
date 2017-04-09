using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.DAL;
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


        public RegistrationController(UserManager<User> manager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper, TeamLeasingContext teamLeasingContext)
        {
            _manager = manager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _teamLeasingContext = teamLeasingContext;
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
                User user = new User();
                DeveloperUser developerUser = new DeveloperUser();

                developerUser = _mapper.Map<DeveloperUser>(vm);
                developerUser.Technology = _teamLeasingContext.Technologies.FirstOrDefault(f => f.Name == vm.ChoosenTechnology);
                user = _mapper.Map<User>(vm);

                await _teamLeasingContext.DeveloperUsers.AddAsync(developerUser);
                var result = await _manager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await _teamLeasingContext.DeveloperUsers.AddAsync(developerUser);
                    await _teamLeasingContext.SaveChangesAsync();

                }
                var r = await _manager.AddToRoleAsync(user, Roles.Developer.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Registration", vm);
            }

        }

    }
}
