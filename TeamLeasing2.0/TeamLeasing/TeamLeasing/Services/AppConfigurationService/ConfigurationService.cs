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
        private readonly ILevel _level;
        private readonly ITechnology _technology;
        private readonly IIsFinishedUniversity _isFinishedUniversity;
        private readonly IProvince _province;

        public ConfigurationService(ILevel level, ITechnology technology, IIsFinishedUniversity isFinishedUniversity,
            IProvince province)
        {
            _level = level;
            _technology = technology;
            _isFinishedUniversity = isFinishedUniversity;
            _province = province;
        }

        public ILevel GetLevel()
        {
            return _level;
        }

        public ITechnology GetTechnology()
        {
            return _technology;
        }

        public IIsFinishedUniversity GetIsFinishedUniversity()
        {
            return _isFinishedUniversity;
        }

        public IProvince GetProvince()
        {
            return _province;
        }
    }
}
