using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;

namespace TeamLeasing.Services.AppConfigurationService.TechnologyService
{
    public class Technology
    {
        private readonly TeamLeasingContext _teamLeasingContext;

        public Technology(TeamLeasingContext teamLeasingContext)
        {
            _teamLeasingContext = teamLeasingContext;
        }
        public async Task<SelectList> GetSelectListTechnology()
        {
            var list = await this.GetListTechnology();
            list.Insert(0, "");
            return new SelectList(list);
        }
        public async Task<IList<string>> GetListTechnology()
        {
            return await _teamLeasingContext.Technologies.Select(s => s.Name)
                .ToListAsync();
        }
    }
}