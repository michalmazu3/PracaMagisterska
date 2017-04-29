using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class EmployeeUser
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Województwo")]
        public String Province { get; set; }
        
        [Display(Name = "Firma")]
        public string Company { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public  ICollection<Job> Jobs { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}