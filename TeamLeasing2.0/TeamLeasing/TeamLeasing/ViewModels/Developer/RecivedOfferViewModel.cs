using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Developer
{
    public class RecivedOfferViewModel
    {
        public int OfferId { get; set; }

        [Display(Name = "Wynagrodzenie")]
        public decimal? ConstSalary { get; set; }

        [Display(Name = "Widełki  od")]
        public decimal? MinSalary { get; set; }

        [Display(Name = "Widełki  od")]
        public decimal? MaxSalary { get; set; }

        [Display(Name = "Status")]
        public string StatusForDeveloper { get; set; }

        [Display(Name = "Poziom")]
        public string Level { get; set; }

        [Display(Name = "Rodzaj zatrudnienia")]
        public string EmploymentType { get; set; }

        [Display(Name = "Informacje dodatkowe")]
        public string AdditionalInformation { get; set; }

        [Display(Name = "Technologia")]
        public string Technology { get; set; }

        [Display(Name = "Firma")]
        public string Company { get; set; }

        [Display(Name = "Trwające negocjacje")]
        public NegotiationViewModel NegotiationViewModel { get; set; }
    }
}