using System;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Project.SearchProject
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Przewidziany budżet")]
        public int Budget { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }


        [Display(Name = "Opis")]
        public string Descritpion { get; set; }

        [Display(Name = "Rodzaj projektu")]
        public string ProjectType { get; set; }

        [Display(Name = "Czas Wykonania")]
        public DateTime ExecutionTime { get; set; }

        [Display(Name = "Liczba potrzebnych programistów")]
        public int NumberOfDeveloperNeeded { get; set; }

        [Display(Name = "Pozostało wakatów")]
        public int VacanciesRemain { get; set; }


        [Display(Name = "Nazwa firmy")]
        public string Company { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Województwo")]
        public string Province { get; set; }
    }
}