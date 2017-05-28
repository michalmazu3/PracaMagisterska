using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Employee.Account
{
    public class ApplyingDeveloper
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Status aplikacji")]
        public Enums.JobStatusForDeveloper Status { get; set; }
    }
}