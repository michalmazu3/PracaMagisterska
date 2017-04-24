using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Employee
    {

        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Hasło  za krótkie")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
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
        public string Province { get; set; }
        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Niepoprawny numer telefonu")]
        public string Phone { get; set; }
        [Display(Name = "Firma")]

        public ICollection<Offer> Offers { get; set; }
        public  ICollection<Job> Jobs { get; set; }
    }
}