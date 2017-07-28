using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class ProjectWithApplicationViewModel
    {
        public int ProjectId { get; set; }

        [Display(Name = "Budżet")]
        public int Budget { get; set; }

        [Display(Name = "Projekt")]
        public string Title { get; set; }

        [Display(Name = "Liczba potrzebnych programistów")]
        public int NumberOfDeveloperNeeded { get; set; }

        [Display(Name = "Pozostało wakatów")]
        public int VacanciesRemain { get; set; }

        [Display(Name = "Status")]
        public Enums.JobStatusForEmployee Status { get; set; }

        [Display(Name = "Aplikacje od")]
        public List<ApplyingDeveloper> ApplyingDevelopers { get; set; }
    }
}