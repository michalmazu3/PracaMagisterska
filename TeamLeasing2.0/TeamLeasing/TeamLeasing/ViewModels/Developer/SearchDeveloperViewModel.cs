using System.Collections.Generic;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Developer
{
    public class SearchDeveloperViewModel
    {
        public SidebarDeveloperViewModel SidebarDeveloperViewModel { get; set; }
        public List<DeveloperUser> DeveloperUsers { get; set; }
    }
}