using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Negotiation
    {
        public int Id { get; set; }

        public int Salary { get; set; }
        public Enums.EmploymentType EmploymentType { get; set; }
        public string AdditionalInformation { get; set; }

        [Display(Name = "Status")]
        public Enums.NegotiationStatus StatusForDeveloper { get; set; }

        [Display(Name = "Status")]
        public Enums.NegotiationStatus StatusForEmployee { get; set; }

        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}