namespace TeamLeasing.Models
{
    public class Negotiation
    {
        public int Id { get; set; }
        public int Salary { get; set; }
        public string EmploymentType { get; set; }
        public string AdditionalInformation { get; set; }
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}