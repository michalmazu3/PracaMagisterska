using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.ViewModels;

namespace TeamLeasing.ViewComponents
{
    public class RegistrationDeveloperViewComponent : ViewComponent
    {
       
        private readonly IConfigurationService _configurationService;

        public RegistrationDeveloperViewComponent(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            RegistrationDeveloperViewModel vm =  new RegistrationDeveloperViewModel();
            vm.IsFinishedUnivesity = _configurationService.GetIsFinishedUniversity().GetSelectList();
            vm.Levels = _configurationService.GetLevel().GetSelectList();
            vm.Technologies =await _configurationService.GetTechnology().GetSelectList();
            vm.Province = _configurationService.GetProvince().GetSelectList();

            return View(vm);
        }
   
      
    }
}
