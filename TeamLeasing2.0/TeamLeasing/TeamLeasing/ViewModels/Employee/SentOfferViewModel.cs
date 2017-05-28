using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Employee
{
    public class SentOfferViewModel
    {
        [Display(Name = "Wynagrodzenie")]
        public decimal? ConstSalary { get; set; }

        [Display(Name = "Widełki  od")]
        public decimal? MinSalary { get; set; }

        [Display(Name = "Widełki  od")]
        public decimal? MaxSalary { get; set; }

        [Display(Name = "Status")]
        public string StatusForEmployee { get; set; }

        [Display(Name = "Poziom")]
        public string Level { get; set; }

        [Display(Name = "Rodzaj zatrudnienia")]
        public string EmploymentType { get; set; }

        [Display(Name = "Informacje dodatkowe")]
        public string AdditionalInformation { get; set; }

        [Display(Name = "Technologia")]
        public string Technology { get; set; }

        public int OfferId { get; set; }
        public int DeveloperUserId { get; set; }

        [Display(Name = "Imie")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display (Name = "Trwające negocjacje")]
        public NegotiationViewModel NegotiationViewModel { get; set; }
    }
}