using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class ApplyingDeveloper
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
    }
}