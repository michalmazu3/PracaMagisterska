using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Services.AppConfigurationService.EmploymentTypeService;
using TeamLeasing.ViewModels;
using TeamLeasing.Services.AppConfigurationService.LevelService;
using TeamLeasing.Services.AppConfigurationService.TechnologyService;
using TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService;
using TeamLeasing.Services.AppConfigurationService.ProvinceService;

namespace TeamLeasing.Services.AppConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        private ILevel _level;
        private ITechnology _technology;
        private IIsFinishedUniversity _isFinishedUniversity;
        private IProvince _province;
        private IEmploymentType _employmentType;

        public ConfigurationService(TeamLeasingContext teamLeasingContext)
        {
            _teamLeasingContext = teamLeasingContext;
        }
         public IProvince ProvinceService
        {
            get
            {

                _province = _province ?? new Province();
                return _province;
            }
        }
      
        public ITechnology TechnologyService
        {
            get
            {
                _technology = _technology ?? new Technology(_teamLeasingContext);
                return _technology;
            }
        }
        
        public ILevel LevelSerice
        {
            get
            {
                _level = _level ?? new Level();
                return _level;
            }
        }
        public IIsFinishedUniversity IsFinishedUniversityService
        {
            get
            {
                _isFinishedUniversity = _isFinishedUniversity ?? new IsFinishedUniversity();
                return _isFinishedUniversity;
            }
        }

        public IEmploymentType EmploymentType
        {
            get
            {
               _employmentType = _employmentType ?? new EmploymentType();
                return _employmentType;
            }
        }
    }
}
