using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Przewidziany budżet")]
        [Range(0, 1000000, ErrorMessage = "Podaj cene z zakresu 0 - 1000000")]
        public int Budget { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        [Range(5, 100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Descritpion { get; set; }

        public bool IsHidden { get; set; }
        public Enums.JobStatusForEmployee StatusForEmployee { get; set; }

        [Display(Name = "Rodzaj projektu")]
        public string ProjectType { get; set; }

        [Required]
        [Display(Name = "Czas Wykonania")]
        public DateTime ExecutionTime { get; set; }

        [Required]
        [Display(Name = "Liczba potrzebnych programistów")]
        public int NumberOfDeveloperNeeded { get; set; }

        [Required]
        [Display(Name = "Pozostało wakatów")]
        public int VacanciesRemain { get; set; }


        public int EmployeeUserId { get; set; }
        public virtual EmployeeUser EmployeeUser { get; set; }
        public virtual ICollection<DeveloperInProject> DeveloperInProject { get; set; }
    }
}