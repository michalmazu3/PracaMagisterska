using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.EmploymentTypeService
{
    public class EmploymentType :IEmploymentType
    {
        public IList<Enums.EmploymentType> GetListEmploymentType()
        {
            List<Enums.EmploymentType> list = new List<Enums.EmploymentType>();
            foreach (Enums.EmploymentType level in Enum.GetValues(typeof(Enums.EmploymentType)))
            {
                list.Add(level);
            }
            return list;
        }
        public SelectList GetSelectListEmploymentType()
        {
            List<string> selectList = this.GetListEmploymentType().Select(s => s.ToString())
                .ToList();
            selectList.Insert(0, "");
            return new SelectList(selectList);
        }
    }
}