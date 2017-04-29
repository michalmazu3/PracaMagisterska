using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Job
{
    public class SidebarEmployeeViewModel
    {
        [Display(Name = "Od")]
        public int? SalaryMin { get; set; }

        [Display(Name = "Do")]
        public int? SalaryMax { get; set; }

        public List<NameValuePairSearchViewModel<string>> TechnologyNameValuePairs { get; set; }
        public List<NameValuePairSearchViewModel<Enums.Level>> LevelNameValuePairs { get; set; }
        public List<NameValuePairSearchViewModel<string>> ProvinceNameValuePairs { get; set; }
    }
}