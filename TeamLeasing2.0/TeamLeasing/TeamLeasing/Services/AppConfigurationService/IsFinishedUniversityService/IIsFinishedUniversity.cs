using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService
{
    public interface IIsFinishedUniversity
    {
        IList<Enums.IsFinishedUniversity> GetListIsFinishedUniversity();
        SelectList GetSelectListIsFinishedUniversity();
    }
}