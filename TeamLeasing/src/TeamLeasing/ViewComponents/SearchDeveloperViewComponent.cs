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
            SearchDeveloperViewModel model = new SearchDeveloperViewModel();
            await Task.Run(() =>
            {
                model.UniversityNameValuePairs = CheckAvailableUniversity();
                model.TechnologyNameValuePairs = CheckAvailableTechnology().Result;
                model.LevelNameValuePairs = CheckAvailableLevel().Result;

            });
            return View(model);
        }

        private async Task<List<LevelNameValuePair>> CheckAvailableLevel()
        {
            return await Task.Run(() =>
            {
                List<LevelNameValuePair> list = new List<LevelNameValuePair>()
                {
                    new LevelNameValuePair() { Name = Level.Junior, Value = true},
                    new LevelNameValuePair() { Name = Level.Regular, Value = false},
                    new LevelNameValuePair() { Name = Level.Senior, Value = false},
                };

                return list;
            });
        }

        private async Task<List<TechnologyNameValuePair>> CheckAvailableTechnology()
        {
            List<TechnologyNameValuePair> list = new List<TechnologyNameValuePair>();
            await _teamLeasingContext.Technologies.ForEachAsync(s => list.Add(new TechnologyNameValuePair()
            {
                Name = s.Name,
                Value = false
            }));
            return list;
        }

        private List<UniversityNameValuePair> CheckAvailableUniversity()
        {

            return new List<UniversityNameValuePair>()
            {
                new UniversityNameValuePair() { Name = "brak",Value = false},
                 new UniversityNameValuePair() { Name = "ukończone",Value = false},
            };
        }


    }
}
