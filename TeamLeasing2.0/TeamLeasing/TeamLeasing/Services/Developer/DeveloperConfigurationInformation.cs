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

        public async Task<SelectList> GetSelectListTechnology()
        {
            var list = await this.GetListTechnology();
            list.Insert(0, "");
            return new SelectList(list);
        }

        public SelectList GetSelectListLevel()
        {
            List<string> selectList = this.GetListLevel().Select(s => s.ToString())
                                                         .ToList();
            selectList.Insert(0, "");
            return new SelectList(selectList);
        }

        public SelectList GetSelectListIsFinishedUniversity()
        {
            List<string> selectList = this.GetListIsFinishedUniversity().Select(s => s.ToString())
                                                                    .ToList();

            selectList.Insert(0, "");
            return new SelectList(selectList);
        }

        public async Task<IList<string>> GetListTechnology()
        {
            return await _teamLeasingContext.Technologies.Select(s => s.Name)
                                                         .ToListAsync();
        }

        public IList<Level> GetListLevel()
        {
            List<Level> list = new List<Level>();
            foreach (Level level in Enum.GetValues(typeof(Level)))
            {
                list.Add(level);
            }
            return list;
        }

        public IList<IsFinishedUniversity> GetListIsFinishedUniversity()
        {
            List<IsFinishedUniversity> list = new List<IsFinishedUniversity>();

            foreach (IsFinishedUniversity item in Enum.GetValues(typeof(IsFinishedUniversity)))
            {
                list.Add(item);
            }
            return list;
        }
    }
}
