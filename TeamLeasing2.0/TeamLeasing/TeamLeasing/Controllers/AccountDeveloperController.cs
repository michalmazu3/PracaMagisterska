using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.Developer;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.ViewModels.Developer;
namespace TeamLeasing.Controllers
{
    public class AccountDeveloperController : Controller
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly UserManager<User> _manager;
        private readonly IDeveloperConfigurationInformation _developerConfigurationInformation;

        public AccountDeveloperController(TeamLeasingContext _teamLeasingContext, UserManager<User> manager, 
                    IDeveloperConfigurationInformation developerConfigurationInformation)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _manager = manager;
            _developerConfigurationInformation = developerConfigurationInformation;
        }
      //  [Route("account/[action]")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit()
        {
            EditDeveloperProfileViewModel vm = await PrepareViewModel();
            return  View("EditProfile", vm);
        }

        private async Task<EditDeveloperProfileViewModel>PrepareViewModel()
        {

            return new EditDeveloperProfileViewModel()
            {
                Technologies =await _developerConfigurationInformation.GetTechnologyConfiguration(),
                Levels = _developerConfigurationInformation.GetLevelConfiguration(),
                IsFinishedUnivesity = _developerConfigurationInformation.GetIsFinishedUniversityConfiguration(),
                
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
            }

            return View("EditProfile",vm);
        }

        private async Task<User> UpdateUserformation(EditDeveloperProfileViewModel vm)
        {
            var userId = _manager.GetUserId(HttpContext.User);
            var user = await _manager.Users.Include(i=>i.DeveloperUser)
                                            .ThenInclude(t=>t.Technology)
                                            .FirstOrDefaultAsync(f=>f.Id==userId);


            await UpdateDeveloperUserInformation(vm, user.DeveloperUser);

            user.PhoneNumber = vm.Phone != null ? vm.Phone : user.PhoneNumber;

            return user;
        }

        private async Task UpdateDeveloperUserInformation(EditDeveloperProfileViewModel vm, DeveloperUser developerUser)
        {
            developerUser.Technology = vm.ChoosenTechnology!=null ? await  _teamLeasingContext.Technologies
                                                                                     .FirstOrDefaultAsync(f=>f.Name==vm.ChoosenTechnology)  
                                                         : developerUser.Technology;
            developerUser.Experience = vm.Experience == null || vm.Experience < 0 ? developerUser.Experience :(int)vm.Experience;
            developerUser.City = vm.City ?? developerUser.City;
            developerUser.Province = vm.Province ?? developerUser.Province;
            developerUser.IsFinishedUniversity = vm.ChoosenIsFinishedUnivesity!= null? vm.ChoosenIsFinishedUnivesity
                                                                            :developerUser.IsFinishedUniversity;
            developerUser.Level = vm.ChoosenLevel != null ? vm.ChoosenLevel : developerUser.Level;
            developerUser.University = vm.University ?? developerUser.University;
            developerUser.About = vm.About ?? developerUser.About;
            _teamLeasingContext.DeveloperUsers.Update(developerUser);
            await _teamLeasingContext.SaveChangesAsync();

        }
    }
}