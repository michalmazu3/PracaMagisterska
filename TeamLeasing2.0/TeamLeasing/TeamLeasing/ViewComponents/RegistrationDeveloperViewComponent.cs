using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;
using TeamLeasing.Models;
using TeamLeasing.ViewModels;

namespace TeamLeasing.ViewComponents
{
    public class RegistrationDeveloperViewComponent : ViewComponent
    {
        private readonly TeamLeasingContext _teamLeasingContext;

        public RegistrationDeveloperViewComponent(TeamLeasingContext _teamLeasingContext)
        {
            this._teamLeasingContext = _teamLeasingContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            RegistrationDeveloperViewModel vm = await FillViewModel();
 
            return View(vm);
        }

        private async Task<RegistrationDeveloperViewModel> FillViewModel()
        {
            return await Task.Run(async () =>
            {
                RegistrationDeveloperViewModel vm = new RegistrationDeveloperViewModel();
                vm.Technologies = await FillViewModelWithtechnology();
                vm.IsFinishedUnivesity = FillViewModelWitchIsFinishedUniversity();
                vm.Levels = FillViewModelWitchLevel();
                return vm;
            });
        }

        private async Task<SelectList> FillViewModelWithtechnology()
        {
            return new SelectList( await _teamLeasingContext.Technologies.Select(s => s.Name).ToListAsync());
        }

        private SelectList FillViewModelWitchLevel()
        {
            List<Level> list = new List<Level>();

            foreach (Level level in Enum.GetValues(typeof(Level)))
            {
                list.Add(level);
             }
            return new SelectList(list);
        }

        private SelectList FillViewModelWitchIsFinishedUniversity()
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
