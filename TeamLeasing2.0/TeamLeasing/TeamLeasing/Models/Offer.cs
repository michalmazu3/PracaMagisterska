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
        [Display(Name = "Poziom")]
        public Level Level { get; set; }
        public bool IsHidden { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string OfferStatus { get; set; }

        [Required]
        [Display(Name = "Technologia")]
        public virtual Technology Technology { get; set; }
    }

    public enum OfferStatus
    {
        InProgress,
        Rejected,
        Accepted
    }
}