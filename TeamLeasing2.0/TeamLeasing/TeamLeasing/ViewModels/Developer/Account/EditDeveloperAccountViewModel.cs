using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;
using TeamLeasing.ViewModels.Developer.Account;

namespace TeamLeasing.ViewModels.Developer
{
    public class EditDeveloperAccountViewModel
    {
        public BasicInformation BasicInformation { get; set; }
        public Cv Cv { get; set; }
        public Photo Photo { get; set; }
    }
}
