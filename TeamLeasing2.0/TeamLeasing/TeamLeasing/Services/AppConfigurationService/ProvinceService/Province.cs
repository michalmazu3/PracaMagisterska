using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;
using System.Linq;

namespace TeamLeasing.Services.AppConfigurationService.ProvinceService
{
    public class Province : IProvince
    {
        public IList<Enums.Province> GetList()
        {
            List<Enums.Province> list = new List<Enums.Province>();
            foreach (Enums.Province province in Enum.GetValues(typeof(Enums.Province)))
            {
                list.Add(province);
            }
            return list;
        }



        public SelectList GetSelectList()
        {
            List<string> selectList = this.GetList().Select(s => s.ToString())
                                                         .ToList();
            selectList.Insert(0, "");
            return new SelectList(selectList);
        }

    
    }
}