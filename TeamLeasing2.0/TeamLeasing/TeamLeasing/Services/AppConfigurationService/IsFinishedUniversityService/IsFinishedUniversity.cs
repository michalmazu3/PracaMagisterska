using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.IsFinishedUniversityService
{
    public class IsFinishedUniversity : IIsFinishedUniversity
    {
        /// <summary>
        /// zwara select liste z enuma wraz z pusta pozcja na poczatku listy
        /// </summary>
        /// <returns></returns>
        public SelectList GetSelectList()
        {
            List<string> selectList = this.GetList().Select(s => s.ToString())
                .ToList();

            selectList.Insert(0, "");
            return new SelectList(selectList);
        }

        public IList<Enums.IsFinishedUniversity> GetList()
        {
            List<Enums.IsFinishedUniversity> list = new List<Enums.IsFinishedUniversity>();

            foreach (Enums.IsFinishedUniversity item in Enum.GetValues(typeof(Enums.IsFinishedUniversity)))
            {
                list.Add(item);
            }
            return list;
        }
    }
}
