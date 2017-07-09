using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamLeasing.Models;
namespace TeamLeasing.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Wynagrodzenie")]
        [Range(0,100000, ErrorMessage = "Podaj cene z zakresu 0 - 100000")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        [Range(5,100)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string Descritpion { get; set; }
        [Display(Name = "Poziom")]
        public Enums.Level Level { get; set; }
        [Display(Name = "Rodzaj zatrudnienia")]
        public string EmploymentType { get; set; }
        public bool IsHidden { get; set; }
        public Enums.JobStatusForEmployee StatusForEmployee { get; set; }


        public virtual Technology Technology { get; set; }
        public int EmployeeUserId { get; set; }
        public virtual EmployeeUser EmployeeUser { get; set; }
        public virtual ICollection<DeveloperUserJob> DeveloperUsers { get; set; }
    }

   
}