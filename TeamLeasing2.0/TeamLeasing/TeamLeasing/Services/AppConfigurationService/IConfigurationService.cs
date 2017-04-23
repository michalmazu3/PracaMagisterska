using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService;
using TeamLeasing.Services.AppConfigurationService.LevelService;
using TeamLeasing.Services.AppConfigurationService.ProvinceService;
using TeamLeasing.Services.AppConfigurationService.TechnologyService;

namespace TeamLeasing.Services.AppConfigurationService
{
    public interface IConfigurationService
    {
         ILevel GetLevel();
         ITechnology GetTechnology();
         IIsFinishedUniversity GetIsFinishedUniversity();
         IProvince GetProvince();
    }
}