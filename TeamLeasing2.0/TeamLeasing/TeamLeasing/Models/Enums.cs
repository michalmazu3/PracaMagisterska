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

            Dolnośląskie,
            KujawskoPomorskie,
            Lubelskie,
            Lubuskie,
            Łódzkie,
            Małopolskie,
            Mazowieckie,
            Opolskie,
            Podkarpackie,
            Podlaskie,
            Pomorskie,
            Śląskie,
            Świętokrzyskie,
            WarmińskoMazurskie,
            Wielkopolskie,
            Zachodniopomorskie,
        }
    }
}