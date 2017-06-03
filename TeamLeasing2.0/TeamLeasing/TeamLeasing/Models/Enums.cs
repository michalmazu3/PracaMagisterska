using System.ComponentModel.DataAnnotations;

namespace TeamLeasing.Models
{
    public static class Enums
    {
        public enum EmploymentType
        {
            [Display(Name = "Umowa o prace")] UoP,
            [Display(Name = "Umowa zlecenie")] UZ,
            [Display(Name = "Umowa o dzieło")] UoD,
            [Display(Name = "B2B")] B2B,
            [Display(Name = "Dowolna")] Any
        }

        public enum IsFinishedUniversity
        {
            [Display(Name = "Ukończono")] Finished,
            [Display(Name = "W trakcie")] InProgress,
            [Display(Name = "Brak")] NotFinished
        }

        public enum JobStatusForDeveloper
        {
            [Display(Name = "Do rozpatrzenia")] Applying,
            [Display(Name = "Rezygnacja")] Resignation,
            [Display(Name = "Odrzucono")] Rejected,
            [Display(Name = "Zaakceptowao")] Accepted,
            [Display(Name = "Zakończono oferte")] Finished
        }

        public enum JobStatusForEmployee
        {
            [Display(Name = "W toku")] InProgress,
            [Display(Name = "Zakończono")] Finished,
            [Display(Name = "Zatwierdzono aplikacje")] Approve
        }

        public enum Level
        {
            [Display(Name = "Junior")] Junior,
            [Display(Name = "Regular")] Regular,
            [Display(Name = "Senior")] Senior
        }

        public enum NegotiationStatus
        {
            [Display(Name = "Oczekiwanie na opowiedź programisty")] WaitingForDeveloperResponse,
            [Display(Name = "Oczekiwanie na opowiedź pracodawcy")] WaitingForEmployeeResponse,
            [Display(Name = "Do rozpatrzenia")] Consider,
            [Display(Name = "Rezygnacja")] Resignation,
            [Display(Name = "Odrzucono")] Rejected,
            [Display(Name = "Zaakceptowao")] Accepted,
            [Display(Name = "Wycofano")] Canceled,

        }


        public enum OfferStatus
        {
            [Display(Name = "Nowa")] New,
            [Display(Name = "Rezygnacja")] Resignation,
            [Display(Name = "Wycofano")] Canceled,
            [Display(Name = "W toku")] InProgress,
            [Display(Name = "Zaakceptowao")] Accepted,
            [Display(Name = "W trakcie negocjacji")] Negotiation,
            [Display(Name = "Odrzucono")] Rejected
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
            Zachodniopomorskie
        }
    }
}