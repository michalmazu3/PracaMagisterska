using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public bool IsHidden { get; set; }
        [Display(Name = "Status")]
        public JobStatus Status { get; set; }
        
        public virtual Technology Technology { get; set; }

        public int EmployeeUserId { get; set; }
        public virtual EmployeeUser EmployeeUser { get; set; }

        public virtual ICollection<DeveloperUserJob> DeveloperUsers { get; set; }
    }

    public enum JobStatus
    {
        NoApplications,
        InProgress,
        Rejected,
        Accepted
    }
}