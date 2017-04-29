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
using TeamLeasing.Infrastructure.Helper;
using TeamLeasing.Services.AppConfigurationService;
using TeamLeasing.ViewModels;
using TeamLeasing.ViewModels.Developer;

namespace TeamLeasing.ViewComponents
{
    public class SearchDeveloperViewComponent : ViewComponent
    {
        private readonly ILoadingDataToSidebarHelper _loadingDataToSidebar;


        public SearchDeveloperViewComponent(ILoadingDataToSidebarHelper loadingDataToSidebar)
        {
            _loadingDataToSidebar = loadingDataToSidebar;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SidebarDeveloperViewModel vm = TempData.Get<SidebarDeveloperViewModel>("searchDevelopers") ??
                                             await PrepareViewModel();

            return View("Sidebar", vm);
        }

        private async Task<SidebarDeveloperViewModel> PrepareViewModel()
        {
            return new SidebarDeveloperViewModel
            {
                TechnologyNameValuePairs =
                    await _loadingDataToSidebar.LoadColletionAsync(s => s.TechnologyService.GetListTechnology()),
                UniversityNameValuePairs =
                    _loadingDataToSidebar.LoadColletion(
                        s => s.IsFinishedUniversityService.GetListIsFinishedUniversity()),
                LevelNameValuePairs = _loadingDataToSidebar.LoadColletion(t => t.LevelSerice.GetListLevel())
            };
        }
    }
}
