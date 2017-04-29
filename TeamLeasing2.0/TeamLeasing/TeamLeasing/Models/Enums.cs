using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public static class Enums
    {
        public enum JobStatus
        {
            NoApplications,
            InProgress,
            Rejected,
            Accepted
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