using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class CreateJobViewModel
    {
        [Required(ErrorMessage = "Wpisz wynagrodzenie")]
        [Display(Name = "Wynagrodzenie")]
        [Range(0, 100000, ErrorMessage = "Podaj cene z zakresu 0 - 100000")]
        public int Price { get; set; }
        [Required (ErrorMessage = "Podaj tytuł oferty")]
        [Display(Name = "Tytuł")]
        [StringLength(100,MinimumLength = 5,ErrorMessage = "Tytuł musi mieć min 5 znaków a max 100 znaków")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Podaj opis")]
        [Display(Name = "Opis")]
        public string Descritpion { get; set; }
        [Display(Name = "Poziom")]
        public string  ChoosenLevel { get; set; }
        [Display(Name = "Rodzaj zatrudnienia")]
        public string ChoosenEmploymentType { get; set; }
        [Required(ErrorMessage = "Wybierz technologie")]
        [Display(Name = "Technologia")]
        public   string ChoosenTechnology { get; set; }
    }
}