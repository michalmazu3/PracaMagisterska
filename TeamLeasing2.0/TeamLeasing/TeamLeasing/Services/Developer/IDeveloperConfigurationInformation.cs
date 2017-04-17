using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeamLeasing.Services.Developer
{
    public interface IDeveloperConfigurationInformation
    {
        Task<SelectList> GetTechnologyConfiguration();
        SelectList GetLevelConfiguration();
        SelectList GetIsFinishedUniversityConfiguration();
    }
}