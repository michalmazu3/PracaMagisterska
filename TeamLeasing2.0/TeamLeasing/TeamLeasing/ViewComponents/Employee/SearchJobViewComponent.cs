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
using TeamLeasing.ViewModels.Job;

namespace TeamLeasing.ViewComponents
{
    public class SearchJobViewComponent : ViewComponent
    {
        private readonly ILoadingDataToSidebarHelper _loadingDataToSidebar;


        public SearchJobViewComponent(TeamLeasingContext teamLeasingContext,
            ILoadingDataToSidebarHelper loadingDataToSidebar)
        {
            _loadingDataToSidebar = loadingDataToSidebar;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SidebarEmployeeViewModel vm = TempData.Get<SidebarEmployeeViewModel>("searchjobs")??
                                           await PrepareViewModel();
            return View("Sidebar", vm);
        }

        private async Task<SidebarEmployeeViewModel> PrepareViewModel()
        {
            return new SidebarEmployeeViewModel()
            {
                TechnologyNameValuePairs = await _loadingDataToSidebar.LoadColletionAsync<string>(async s =>
                                                    await s.TechnologyService.GetListTechnology()),
                LevelNameValuePairs = _loadingDataToSidebar.LoadColletion<Enums.Level>(s =>
                                                    s.LevelSerice.GetListLevel()),
                ProvinceNameValuePairs = _loadingDataToSidebar.LoadColletion<string>(s =>
                                                    s.ProvinceService.GetListProvince()),
            };
        }

    }
}
