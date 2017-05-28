using System.ComponentModel.DataAnnotations;
using TeamLeasing.Infrastructure.Attribute;

namespace TeamLeasing.ViewModels.Developer
{
    public class SendingOfferViewModel
    {
        public int DeveloperUserId { get; set; }

        [RequiredIfMinAndMaxSalaryNull]
        [Display(Name = "Wynagrodzenie")]
        [Range(0, int.MaxValue)]
        public decimal? ConstSalary { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Widełki (wynagrodzenie) od")]
        [MinSalary]
        public decimal? MinSalary { get; set; }

        [Range(0, int.MaxValue)]
        [MaxSalary]
        [Display(Name = "Widełki (wynagrodzenie) do")]
        public decimal? MaxSalary { get; set; }

        [Required(ErrorMessage = "Wybierz technologie/język")]
        public string ChoosenTechnology { get; set; }

        [Display(Name = "Poziom")]
        public string ChoosenLevel { get; set; }

        [Display(Name = "Rodzaj zatrudnienia")]
        public string ChoosenEmploymentType { get; set; }

        [Display(Name = "Dodatkowe informacje")]
        public string AdditionalInformation { get; set; }
    }
}