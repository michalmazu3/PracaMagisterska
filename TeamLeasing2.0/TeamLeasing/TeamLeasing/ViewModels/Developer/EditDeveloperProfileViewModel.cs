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

        [Required(ErrorMessage = "Please Upload a Valid Cv File")]
        [DataType(DataType.Upload)]
        [Display(Name = "Cv")]
        [FileExtensions(Extensions = "pdf")]
        public IFormFile CvFile { get; set; }

        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }


        [Required(ErrorMessage = "Please Upload a Valid Photo File")]
        [DataType(DataType.Upload)]
        [Display(Name = "Zdjęcie")]
        [FileExtensions(Extensions = "jpg")]
        public IFormFile PhotoFile { get; set; }

        [Display(Name = "Technologia")]
        public SelectList Technologies { get; set; }
        [Display(Name = "Poziom")]
        public SelectList Levels { get; set; }
        [Display(Name = "Czy ukończyłeś studia ?")]
        public SelectList IsFinishedUnivesity { get; set; }

        public string ChoosenTechnology { get; set; }
        public string ChoosenLevel { get; set; }
        public string ChoosenIsFinishedUnivesity { get; set; }
    }
}
