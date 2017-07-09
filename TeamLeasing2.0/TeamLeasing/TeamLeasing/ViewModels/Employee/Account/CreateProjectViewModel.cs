using System;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "Przewidziany budżet")]
        [Range(0, 1000000, ErrorMessage = "Podaj cene z zakresu 0 - 1000000")]
        public int Budget { get; set; }

        [Required(ErrorMessage = "Podaj tytuł oferty")]
        [Display(Name = "Tytuł")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tytuł musi mieć min 5 znaków a max 100 znaków")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Podaj opis")]
        [Display(Name = "Opis")]
        public string Descritpion { get; set; }

        [Required]
        [Display(Name = "Czas Wykonania")]
        public DateTime ExecutionTime { get; set; }

        [Required]
        [Display(Name = "Liczba potrzebnych programistów")]
        public int NumberOfDeveloperNeeded { get; set; }

        [Required]
        [Display(Name = "Pozostało wakatów")]
        public int VacanciesRemain { get; set; }

        [Display(Name = "Rodzaj projektu")]
        public string ChoosenProjectType { get; set; }
    }
}