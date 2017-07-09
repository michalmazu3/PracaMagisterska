using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.DAL;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Infrastructure.Helper;
using TeamLeasing.ViewModels.Project;

namespace TeamLeasing.ViewComponents.Project
{
    public class SearchProjectViewComponent : ViewComponent
    {
        private readonly ILoadingDataToSidebarHelper _loadingDataToSidebar;


        public SearchProjectViewComponent(TeamLeasingContext teamLeasingContext,
            ILoadingDataToSidebarHelper loadingDataToSidebar)
        {
            _loadingDataToSidebar = loadingDataToSidebar;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = TempData.Get<SidebarProjectViewModel>("searchprojects") ??
                     await PrepareViewModel();
            return View("Sidebar", vm);
        }

        private async Task<SidebarProjectViewModel> PrepareViewModel()
        {
            return new SidebarProjectViewModel
            {
                ProjectTypeValuePairs = await _loadingDataToSidebar.LoadColletionAsync(async s =>
                    await s.ProjectType.GetListProjectType())
            };
        }
    }
}