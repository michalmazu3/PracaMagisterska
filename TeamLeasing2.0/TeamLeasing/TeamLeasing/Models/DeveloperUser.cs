using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamLeasing.Models
{
    public class DeveloperUser
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
        public string Province { get; set; }
        [Required]
        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date, ErrorMessage = "Wprowadz date w formacie rok.miesiac.dzień")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        public IsFinishedUniversity IsFinishedUniversity { get; set; }

        [Display(Name = "Studia")]
        public string University { get; set; }
        [Display(Name = "Doświadczenie")]
        [Range(0, 20, ErrorMessage = "Podaj wartość z zakreu 0 - 20")]
        public int Experience { get; set; }
        [Display(Name = "Poziom")]
        public Level Level { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Cv")]
        public string Cv { get; set; }

        public string About { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int TechnologyId { get; set; }
         public virtual Technology Technology { get; set; }

        public  ICollection<DeveloperUserJob> Jobs { get; set; }
        public ICollection<Offer> Offers { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }



}