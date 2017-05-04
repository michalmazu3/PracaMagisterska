using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public static class Enums
    {

        public enum JobStatusForDeveloper
        {
            [Display(Name = "Aplikujesz")]
            Applying,
            [Display(Name = "Zrezygnowałeś")]
            Resignation,
            [Display(Name = "Odrzucono")]
            Rejected,
            [Display(Name = "Zaakceptowao")]
            Accepted,
            [Display(Name = "Zakończono oferte")]
            Finished
        }

        public enum JobStatusForEmployee
        {
            [Display(Name = "W toku")]
            InProgress,
            [Display(Name = "Zakończono")]
            Finished,
            [Display(Name = "Zatwierdzono aplikacje")]
            Approve
        }
        public enum Level
        {
            Junior,
            Regular,
            Senior
        }
         public enum IsFinishedUniversity
        {
            [Display(Name = "Ukończono")]
            Finished,
            [Display(Name = "W trakcie")]
            InProgress,
            [Display(Name = "Brak")]
            NotFinished
        }

        public enum EmploymentType
        {
            [Display(Name = "Umowa o prace")]
            UoP,
            [Display(Name = "Umowa zlecenie")]
            UZ,
            [Display(Name = "Umowa o dzieło")]
            UoD,
            [Display(Name = "B2B")]
            B2B,
            [Display(Name = "Dowolna")]
            Any,
        }

        public enum OfferStatus
        {
            InProgress,
            Rejected,
            Accepted
        }

        public enum Province
        {

            Dolnoslaskie,
            KujawskoPomorskie,
            Lubelskie,
            Lubuskie,
            Lodzkie,
            Malopolskie,
            Mazowieckie,
            Opolskie,
            Podkarpackie,
            Podlaskie,
            Pomorskie,
            Slaskie,
            Swietokrzyskie,
            WarminskoMazurskie,
            Wielkopolskie,
            Zachodniopomorskie,
        }
    }
}