using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamLeasing.Models;
using TeamLeasing.Services.AppConfigurationService.LevelService;

namespace TeamLeasing.Services.AppConfigurationService.LevelService
{
    public class Level : ILevel
    {
        public IList<Enums.Level> GetList()
        {
            List<Enums.Level> list = new List<Enums.Level>();
            foreach (Enums.Level level in Enum.GetValues(typeof(Enums.Level)))
            {
                list.Add(level);
            }
            return list;
        }
        public SelectList GetSelectList()
        {
            List<string> selectList = this.GetList().Select(s => s.ToString())
                                                         .ToList();
            selectList.Insert(0, "");
            return new SelectList(selectList);
        }
    }
}
