using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Employee
{
    public class RegistrationEmployeeViewModel
    {
        [Required(ErrorMessage = "Pole email jest wymagane!")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa zbyt krótka minimum 5 znaków")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage = "Wprowadź jedną dużą litere, cyfre i znak")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Hasło  za krótkie")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
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
        [Required(ErrorMessage = "Podaj nazwe firmy")]
        [Display(Name = "Firma")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [Phone]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Podaj województwo")]
        public string ChoosenProvince { get; set; }
    }
}