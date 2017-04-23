using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Services.Developer;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;

namespace TeamLeasing.ViewComponents
{
    public class SearchDeveloperViewComponent : ViewComponent
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private readonly IDeveloperConfigurationInformation _developerConfigurationInformation;

        public SearchDeveloperViewComponent(TeamLeasingContext teamLeasingContext, 
            IDeveloperConfigurationInformation developerConfigurationInformation)
        {
            _teamLeasingContext = teamLeasingContext;
            _developerConfigurationInformation = developerConfigurationInformation;
        }

        public async Task<IViewComponentResult> InvokeAsync(SidebarDeveloperViewModel test)
        {
            SidebarDeveloperViewModel model = new SidebarDeveloperViewModel
            {
                TechnologyNameValuePairs = await CheckAvailableTechnology(),
                UniversityNameValuePairs = CheckAvailableUniversity(),
                LevelNameValuePairs = CheckAvailableLevel()
            };
            SidebarDeveloperViewModel tt = TempData.Get<SidebarDeveloperViewModel>("search");
            
            return View("Sidebar", tt??model);
        }

        private List<NameValuePairSearchViewModel<Enums.Level>> CheckAvailableLevel()
        {
            return _developerConfigurationInformation.GetListLevel()
                .Select(level => new NameValuePairSearchViewModel<Enums.Level>()
                {
                    Name = level,
                    Value = false
                }).ToList();
        }

        private async Task<List<NameValuePairSearchViewModel<string>>> CheckAvailableTechnology()
        {
            var technologyList = await _developerConfigurationInformation.GetListTechnology();
            return await Task.Run(() =>
            {
                return technologyList.Select(s => new NameValuePairSearchViewModel<string>()
                    {
                        Name = s,
                        Value = false,
                    })
                    .ToList();
            });
        }

        private List<NameValuePairSearchViewModel<Enums.IsFinishedUniversity>> CheckAvailableUniversity()
        {
            return _developerConfigurationInformation.GetListIsFinishedUniversity()
                .Select(s => new NameValuePairSearchViewModel<Enums.IsFinishedUniversity>()
                {
                    Name = s,
                    Value = false,
                })
                .ToList();
        }
    }
}
