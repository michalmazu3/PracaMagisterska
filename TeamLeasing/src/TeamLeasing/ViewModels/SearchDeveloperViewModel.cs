using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels
{
    public class SearchDeveloperViewModel
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        [Display(Name = "Odd")]
        public int ExpirienceMin { get; set; }

        [Display(Name = "Ddo")]
        public int ExpirienceMax { get; set; }

        public Dictionary<string, bool> Technology { get; set; }
        public Dictionary<Level,bool> Level { get; set; }

        public SearchDeveloperViewModel(TeamLeasingContext teamLeasingContext)
        {
            _teamLeasingContext = teamLeasingContext;
            Technology =CheckAvailableTechnology().Result;
            Level = CheckAvailableLevel().Result;

        }

        private Task<Dictionary<Level, bool>> CheckAvailableLevel()
        {
            return  Task.Run(() =>  
            {
                Dictionary<Level,bool> list = new Dictionary<Level, bool>()
                {
                    [Models.Level.Junior] = false,
                    [Models.Level.Regular] = false,
                    [Models.Level.Senior] = false,
                };
                return list;
            });
        }

        private async Task<Dictionary<string, bool>> CheckAvailableTechnology()
        {
            Dictionary< string,bool> list = new Dictionary<string, bool>();
            await _teamLeasingContext.Technologies.ForEachAsync(s => list.Add(s.Name, false));
            return  list;
        }
    }
}
   