using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels
{
    public class RegistrationDeveloperViewModel
    {
        [Required(ErrorMessage = "Pole email jest wymagane!")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email!")]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Hasło  za krótkie")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Podaj imię")]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj nazwisko")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Podaj miejscowość")]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }
        [Required(ErrorMessage = "Podaj województwo")]
        [Display(Name = "Województwo")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [Phone]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Podaj date urodzenia")]
        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date, ErrorMessage = "Wprowadz date w formacie rok.miesiac.dzień")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

       
         [Display(Name = "Studia")]
        public string University { get; set; }
        [Display(Name = "Doświadczenie w latach")]
        [Range(0, 20, ErrorMessage = "Podaj wartość z zakreu 0 - 20")]
        public int Experience { get; set; }
 
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Cv")]
        public string Cv { get; set; }

        [Display(Name = "Technologia")]
        public SelectList Technologies { get; set; }
        [Display(Name = "Poziom")]
        public SelectList Levels { get; set; }
        [Display(Name = "Czy ukończyłeś studia ?")]
        public SelectList IsFinishedUnivesity { get; set; }

        [Required]
        public string ChoosenTechnology { get; set; }
        [Required]
        public Level ChoosenLevel{ get; set; }
        [Required]
        public IsFinishedUniversity ChoosenIsFinishedUnivesity { get; set; }

    }
}
