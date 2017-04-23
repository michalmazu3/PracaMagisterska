using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Developer
{
    public class SidebarDeveloperViewModel
    {
        [Display(Name = "Od")]
        public int? ExpirienceMin { get; set; }

        [Display(Name = "Do")]
        public int? ExpirienceMax { get; set; }

        public List<NameValuePairSearchViewModel<string>> TechnologyNameValuePairs { get; set; }
        public List<NameValuePairSearchViewModel<Enums.Level>> LevelNameValuePairs { get; set; }
        public List<NameValuePairSearchViewModel<Enums.IsFinishedUniversity>> UniversityNameValuePairs { get; set; }

        
    }
}
   