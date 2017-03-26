using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using System.Collections;
using Microsoft.EntityFrameworkCore;
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
            Task<SearchDeveloperViewModel> task = Task.Run(()=>new SearchDeveloperViewModel(_teamLeasingContext));
            return View(await task);
        }




    }
}
