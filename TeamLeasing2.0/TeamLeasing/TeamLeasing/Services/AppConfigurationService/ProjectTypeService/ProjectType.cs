using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.DAL;

namespace TeamLeasing.Services.AppConfigurationService.ProjectTypeService
{
    public class ProjectType : IProjectType
    {

        private List<string> Type = new List<string>()
        {
            "WebDevelopment",
            "Mobile",
            "Desktop",
           
        };

 
        public async Task<SelectList> GetSelectListProjectType()
        {
            var list = await this.GetListProjectType();
            list.Insert(0, "");
            return new SelectList(list);
        }
        public async Task<IList<string>> GetListProjectType()
        {
            return await Task.Run(() =>
            {
                return this.Type;
            });

        }

      
    }
}