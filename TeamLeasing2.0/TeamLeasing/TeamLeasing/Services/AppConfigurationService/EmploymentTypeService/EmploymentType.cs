using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.EmploymentTypeService
{
    public class EmploymentType : IEmploymentType
    {
        public IList<Enums.EmploymentType> GetListEmploymentType()
        {
            var list = new List<Enums.EmploymentType>();
            foreach (Enums.EmploymentType level in Enum.GetValues(typeof(Enums.EmploymentType)))
                list.Add(level);
            return list;
        }

        public SelectList GetSelectListEmploymentType()
        {
            
            var selectList = GetListEmploymentType().Select(s => s.GetAttribute().Name.ToString())
                .ToList();
            selectList.Insert(0, "");
            return new SelectList(selectList);
        }
    }
}