using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using TeamLeasing.ViewModels;

namespace TeamLeasing.ViewComponents
{
    public class SearchDeveloperViewComponent : ViewComponent
    {
        private readonly TeamLeasingContext _teamLeasingContext;

        public SearchDeveloperViewComponent(TeamLeasingContext teamLeasingContext)
        {
            _teamLeasingContext = teamLeasingContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SearchDeveloperViewModel model = new SearchDeveloperViewModel
            {
                TechnologyNameValuePairs = await CheckAvailableTechnology(),
                UniversityNameValuePairs = CheckAvailableUniversity(),
                LevelNameValuePairs = CheckAvailableLevel()
            };
        
            return View("Developer", model);
        }

        private List<LevelNameValuePair> CheckAvailableLevel()
        {
            return new List<LevelNameValuePair>()
                {
                    new LevelNameValuePair() {Name = Level.Junior, Value = false},
                    new LevelNameValuePair() {Name = Level.Regular, Value = false},
                    new LevelNameValuePair() {Name = Level.Senior, Value = false},
                };
        }

        private async Task<List<TechnologyNameValuePair>> CheckAvailableTechnology()
        {
            return await Task.Run(async() =>
            {
                List<TechnologyNameValuePair> list = new List<TechnologyNameValuePair>();
                using (_teamLeasingContext)
                {
                    return await _teamLeasingContext.Technologies.Select(
                          s => new TechnologyNameValuePair() { Name = s.Name, Value = false }).ToListAsync();
                }
            });
           

        }

        private List<UniversityNameValuePair> CheckAvailableUniversity()
        {

            return new List<UniversityNameValuePair>()
            {
                new UniversityNameValuePair() { Name = IsFinishedUniversity.NotFinished ,Value = false},
                new UniversityNameValuePair() { Name = IsFinishedUniversity.InProgress,Value = false},
                new UniversityNameValuePair() { Name = IsFinishedUniversity.Finished,Value = false},
            };
        }


    }
}
