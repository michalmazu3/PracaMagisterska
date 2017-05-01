using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.EmploymentTypeService
{
    public interface IEmploymentType
    {
        IList<Enums.EmploymentType> GetListEmploymentType();
        SelectList GetSelectListEmploymentType();
    }
}