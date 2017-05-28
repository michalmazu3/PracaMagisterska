using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels
{
    public class NegotiationViewModel
    {
        [Required]
        [Display(Name = "Wynagrodzenie")]
        public int? Salary { get; set; }
        [Required]
        [Display(Name = "Rodzaj zatrudnienia")]
        public string EmploymentType { get; set; }

        [Display(Name = "Dodatkowe warunki")]
        public string AdditionalInformation { get; set; }
        public int OfferId { get; set; }
    }
}