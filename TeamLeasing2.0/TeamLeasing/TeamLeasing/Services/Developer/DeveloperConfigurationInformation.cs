using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

namespace TeamLeasing.Services.Developer
{
    public class DeveloperConfigurationInformation : IDeveloperConfigurationInformation
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        public DeveloperConfigurationInformation(TeamLeasingContext _teamLeasingContext)
        {
            this._teamLeasingContext = _teamLeasingContext;
        }

        public async Task<SelectList> GetTechnologyConfiguration()
        {
            return new SelectList(await _teamLeasingContext.Technologies.Select(s => s.Name).ToListAsync());
        }

        public SelectList GetLevelConfiguration()
        {
            List<Level> list = new List<Level>();

            foreach (Level level in Enum.GetValues(typeof(Level)))
            {
                list.Add(level);
            }
            return new SelectList(list);
        }

        public SelectList GetIsFinishedUniversityConfiguration()
        {
            List<IsFinishedUniversity> list = new List<IsFinishedUniversity>();

            foreach (IsFinishedUniversity item in Enum.GetValues(typeof(IsFinishedUniversity)))
            {
                list.Add(item);
            }
            return new SelectList(list);
        }
    }
}
