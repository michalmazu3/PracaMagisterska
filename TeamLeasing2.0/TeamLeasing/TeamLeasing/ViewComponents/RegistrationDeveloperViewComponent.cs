using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.Developer;
using TeamLeasing.ViewModels;

namespace TeamLeasing.ViewComponents
{
    public class RegistrationDeveloperViewComponent : ViewComponent
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IDeveloperConfigurationInformation _developerConfigurationInformation;

        public RegistrationDeveloperViewComponent(TeamLeasingContext _teamLeasingContext, 
            IDeveloperConfigurationInformation developerConfigurationInformation)
        {
            this._teamLeasingContext = _teamLeasingContext;
            _developerConfigurationInformation = developerConfigurationInformation;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            RegistrationDeveloperViewModel vm =  new RegistrationDeveloperViewModel();
            vm.IsFinishedUnivesity = _developerConfigurationInformation.GetIsFinishedUniversityConfiguration();
            vm.Levels = _developerConfigurationInformation.GetLevelConfiguration();
            vm.Technologies =await _developerConfigurationInformation.GetTechnologyConfiguration();

            return View(vm);
        }
   
      
    }
}
