using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.Services.Developer
{
    public interface IDeveloperConfigurationInformation
    {
        Task<SelectList> GetSelectListTechnology();
        SelectList GetSelectListLevel();
        SelectList GetSelectListIsFinishedUniversity();

        Task<IList<string>> GetListTechnology();
        IList<Level> GetListLevel();
        IList<IsFinishedUniversity> GetListIsFinishedUniversity();
    }
}