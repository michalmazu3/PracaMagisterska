using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Offer
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Wynagrodzenie")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Technologia")]
        public string Technology { get; set; }
        [Required]
        [Display(Name = "Poziom")]
        public string Level { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        public virtual Developer Developer { get; set; }
        public  virtual Employee Employee { get; set; }
    }
}