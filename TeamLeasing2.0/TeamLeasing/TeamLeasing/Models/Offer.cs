using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Display(Name = "Wynagrodzenie")]
        public decimal? ConstSalary { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        [Display(Name = "Status")]
        public Enums.OfferStatus StatusForDeveloper { get; set; }

        [Display(Name = "Status")]
        public Enums.OfferStatus StatusForEmployee { get; set; }

        [Display(Name = "Poziom")]
        public Enums.Level Level { get; set; }
        [Display(Name = "Rodzaj zatrudnienia")]
        public Enums.EmploymentType EmploymentType { get; set; }
        public string AdditionalInformation { get; set; }
        public bool IsHidden { get; set; }
        [Required]
        [Display(Name = "Technologia")]
        public virtual Technology Technology { get; set; }
        public int DeveloperUserId { get; set; }
        public virtual DeveloperUser DeveloperUser { get; set; }
        public int EmployeeUserId { get; set; }
        public virtual EmployeeUser EmployeeUser { get; set; }
        public virtual Negotiation Negotiation { get; set; }
    }
}