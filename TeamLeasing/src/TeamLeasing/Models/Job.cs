using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Wynagrodzenie")]
        [Range(0,100000, ErrorMessage = "Podaj cene z zakresu 0 - 100000")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string Descritpion { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Technologia")]
        public string Technology { get; set; }
    }
}