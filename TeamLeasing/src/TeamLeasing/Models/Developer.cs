using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public class Developer
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5,ErrorMessage = "Hasło  za krótkie")]
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
        [Display (Name = "Województwo")]
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
        [Range(0,20,ErrorMessage = "Podaj wartość z zakreu 0 - 20")]
        public int Experience { get; set; }
        [Display(Name = "Poziom")]
        public Level Level { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Cv")]
        public string Cv { get; set; }

        public virtual  Technology Technology { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }

    public enum Level
    {
        Junior,
        Regular,
        Senior
    }

    public enum IsFinishedUniversity
    {
        Finished,
        InProgress,
        NotFinished
    }
}