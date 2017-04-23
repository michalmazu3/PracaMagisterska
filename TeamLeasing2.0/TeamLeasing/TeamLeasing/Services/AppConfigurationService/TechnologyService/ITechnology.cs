using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeamLeasing.Services.AppConfigurationService.TechnologyService
{
    public interface ITechnology
    {
        Task<SelectList> GetSelectList();
        Task<IList<string>> GetList();
    }
}