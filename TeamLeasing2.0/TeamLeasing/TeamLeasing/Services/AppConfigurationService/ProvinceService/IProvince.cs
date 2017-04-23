using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.ProvinceService
{
    public interface IProvince
    {
        SelectList GetSelectListProvince();
        IList<Enums.Province> GetListProvince();
    }
}