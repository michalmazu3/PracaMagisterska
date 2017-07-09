using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Project
{
    public class SidebarProjectViewModel
    {
        [Display(Name = "Od")]
        public int? BudgetMin { get; set; }

        [Display(Name = "Do")]
        public int? BudgetMax { get; set; }

        public List<NameValuePairSearchViewModel<string>> ProjectTypeValuePairs { get; set; }
    }
}