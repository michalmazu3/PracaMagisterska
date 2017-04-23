using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;
using TeamLeasing.Services.AppConfigurationService.LevelService;
using TeamLeasing.Services.AppConfigurationService.TechnologyService;
using TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService;
using TeamLeasing.Services.AppConfigurationService.ProvinceService;

namespace TeamLeasing.Services.AppConfigurationService
{
    public class  ConfigurationService : IConfigurationService
    { 
        public ConfigurationService(ILevel level, ITechnology technology, IIsFinishedUniversity isFinishedUniversity,
            IProvince province)
        {

        }
    }
}
