using System;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole email jest wymagane!")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pole wiadomość jest wymagane!")]
        [StringLength(500, MinimumLength = 50, ErrorMessage = "Wiadomość ma mieśc od 50 do 500 znaków!")]
        [Display(Name = "Wiadomość")]
         public string Content { get; set; }
        [Required(ErrorMessage = "Pole imię jest wymagane!")]
        [Display(Name = "Imię")]
         public string Name { get; set; }
        [Required(ErrorMessage = "Pole nazwisko jest wymagane!")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SendingDate { get; set; }

    }
}