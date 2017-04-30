using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Job.SearchJob
{
    public class JobViewModel
    {
        public int Id { get; set; }
       
        [Display(Name = "Wynagrodzenie")]
        public int Price { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Opis ofert")]
        public string Descritpion { get; set; }
       
        [Display(Name = "Nazwa firmy")]
        public string Company { get; set; }
        [Display(Name = "Miasto")]
        public  string City { get; set; }
        [Display(Name = "Województwo")]
        public  string Province { get; set; }
        [Display(Name = "Technologia")]
        public string Technology { get; set; }
        [Display(Name = "Stanowisko dla ")]
        public Enums.Level Level { get; set; }
        [Display(Name = "Rodzaj ztrudnienia")]
        public string EmploymentType { get; set; }
    }
}