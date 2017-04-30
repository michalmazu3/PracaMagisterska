using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class EditEmployeeAccountViewModel
    {
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Hasło  za krótkie")]
        [DataType(DataType.Password)]
        [Display(Name = "Obecne hasło")]
        public string CurrentPassword { get; set; }


        [StringLength(50, MinimumLength = 4, ErrorMessage = "Hasło  za krótkie")]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        public string ReNewPassword { get; set; }



        [Display(Name = "Miejscowość")]
        public string City { get; set; }
        [Phone(ErrorMessage = "Podaj poprawny numer telefonu")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Firma")]
        public string Company { get; set; }
        public string ChoosenProvince { get; set; }

    }
}