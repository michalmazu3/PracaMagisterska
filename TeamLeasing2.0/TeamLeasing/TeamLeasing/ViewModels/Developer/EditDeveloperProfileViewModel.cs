using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Developer
{
    public class EditDeveloperProfileViewModel
    {

       
       
        [RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage = "Wprowadź jedną dużą litere, cyfre i znak")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Hasło  za krótkie")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        public string RePassword { get; set; }
       
      
        [Display(Name = "Miejscowość")]
        public string City { get; set; }
    
        [Display(Name = "Województwo")]
        public string Province { get; set; }
        
        [Phone(ErrorMessage = "Podaj poprawny numer telefonu")]
        public string Phone { get; set; }

        [StringLength(200, MinimumLength = 4, ErrorMessage = "Text musi być dłuższy niż 4 znaki a któtszy niż 200")]
        [Display(Name = "O sobie")]
        public string About { get; set; }

        [Display(Name = "Studia")]
        public string University { get; set; }
        [Display(Name = "Doświadczenie w latach")]
        [Range(0, 20, ErrorMessage = "Podaj wartość z zakreu 0 - 20")]
        public int? Experience { get; set; }


        [Display(Name = "Cv")]
        public string Cv { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile CvFile { get; set; }

        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        public IFormFile PhotoFile { get; set; }

        [Display(Name = "Technologia")]
        public SelectList Technologies { get; set; }
        [Display(Name = "Poziom")]
        public SelectList Levels { get; set; }
        [Display(Name = "Czy ukończyłeś studia ?")]
        public SelectList IsFinishedUnivesity { get; set; }

        public string ChoosenTechnology { get; set; }
        public Level ChoosenLevel { get; set; }
        public IsFinishedUniversity ChoosenIsFinishedUnivesity { get; set; }
    }
}
