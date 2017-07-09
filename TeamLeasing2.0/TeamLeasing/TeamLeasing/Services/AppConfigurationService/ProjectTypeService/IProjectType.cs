using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeamLeasing.Services.AppConfigurationService.ProjectTypeService
{
    public interface IProjectType
    {
        Task<SelectList> GetSelectListProjectType();
        Task<IList<string>> GetListProjectType();
    }
}