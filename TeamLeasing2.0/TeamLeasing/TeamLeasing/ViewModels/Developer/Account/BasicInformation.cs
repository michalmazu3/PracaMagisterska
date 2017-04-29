using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamLeasing.ViewModels.Developer.Account
{
    public class BasicInformation
    {

        [Display(Name = "Miejscowość")]
        public string City { get; set; }
        [Phone(ErrorMessage = "Podaj poprawny numer telefonu")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [StringLength(200, MinimumLength = 4, ErrorMessage = "Text musi być dłuższy niż 4 znaki a któtszy niż 200")]
        [Display(Name = "O sobie")]
        public string About { get; set; }

        [Display(Name = "Studia")]
        public string University { get; set; }
        [Display(Name = "Doświadczenie w latach")]
        [Range(0, 20, ErrorMessage = "Podaj wartość z zakreu 0 - 20")]
        public int? Experience { get; set; }

        public string ChoosenTechnology { get; set; }
        public string ChoosenLevel { get; set; }
        public string ChoosenIsFinishedUnivesity { get; set; }
        public string ChoosenProvince { get; set; }

    }
}
