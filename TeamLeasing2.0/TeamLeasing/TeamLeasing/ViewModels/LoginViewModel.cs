using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamLeasing.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj login")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        public string  Pasword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
