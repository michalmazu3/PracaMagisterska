using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class JobWithApplicationsviewModel
    {
        public int JobId { get; set; }
        [Display(Name = "Wynagrodzenie")]
        public int Price { get; set; }
        [Display(Name = "Oferta")]
        public string Title { get; set; }
        [Display(Name = "Technologia")]
        public string Technology { get; set; }
        [Display(Name = "Status")]
        public Enums.JobStatusForEmployee Status { get; set; }
        public List<ApplyingDeveloper> ApplyingDevelopers { get; set; }
    }
}