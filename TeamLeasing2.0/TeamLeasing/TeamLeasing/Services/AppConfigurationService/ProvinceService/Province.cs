using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;
using System.Linq;

namespace TeamLeasing.Services.AppConfigurationService.ProvinceService
{
  
    public class Province : IProvince
    {
        public IList<string> GetListProvince()
        {
            List<string> list = new List<string>();
            foreach (Enums.Province province in Enum.GetValues(typeof(Enums.Province)))
            {
                list.Add(province.ToString());
            }
            return list;
        }

        public SelectList GetSelectListProvince()
        {
            List<string> selectList = this.GetListProvince().Select(s => s.ToString())
                                                         .ToList();
            selectList.Insert(0, "");
            return new SelectList(selectList);
        }

    
    }
}