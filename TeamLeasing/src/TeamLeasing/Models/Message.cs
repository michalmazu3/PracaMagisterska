using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 50, ErrorMessage = "Zle")]
        [Display(Name = "Wiadomość")]
         public string Content { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

    }
}