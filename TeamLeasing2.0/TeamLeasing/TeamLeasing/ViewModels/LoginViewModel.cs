using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Podaj login")]
        public string Username { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Podaj hasło")]
        public string Pasword { get; set; }

        public string ReturnUrl { get; set; }
    }
}