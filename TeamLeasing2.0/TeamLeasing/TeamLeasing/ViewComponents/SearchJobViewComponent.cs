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
    public class SearchJobViewComponent :ViewComponent
    {
        private readonly TeamLeasingContext _teamLeasingContext;
        public SearchJobViewComponent(TeamLeasingContext teamLeasingContext)
        {
            _teamLeasingContext = teamLeasingContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _teamLeasingContext.Jobs.ToListAsync();
           return View("Sidebar");
            
        }
    }
}
