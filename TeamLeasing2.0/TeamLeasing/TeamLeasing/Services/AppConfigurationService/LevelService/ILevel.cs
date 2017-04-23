using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.LevelService
{
    public interface ILevel
    {
        IList<Enums.Level> GetList();
        SelectList GetSelectList();
    }
}