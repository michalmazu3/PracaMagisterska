using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService.EmploymentTypeService;
using TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService;
using TeamLeasing.Services.AppConfigurationService.LevelService;
using TeamLeasing.Services.AppConfigurationService.ProjectTypeService;
using TeamLeasing.Services.AppConfigurationService.ProvinceService;
using TeamLeasing.Services.AppConfigurationService.TechnologyService;

namespace TeamLeasing.Services.AppConfigurationService
{
    public interface IConfigurationService
    {
        [Display(Name = "Województwo")]
        IProvince ProvinceService { get; }
        [Display(Name = "Technologia")]
        ITechnology TechnologyService { get; }
        [Display(Name = "Poziom")]
        ILevel LevelSerice { get; }
        [Display(Name = "Studia")]
        IIsFinishedUniversity IsFinishedUniversityService { get; }
        [Display(Name = "Rodzaj zatrudnienia")]
        IEmploymentType EmploymentType { get; }
        [Display(Name = "Rodzaj projektu")]
        IProjectType ProjectType  { get; }
    }
}