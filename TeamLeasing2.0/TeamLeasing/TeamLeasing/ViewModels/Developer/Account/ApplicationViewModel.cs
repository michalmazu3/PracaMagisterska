using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Developer.Account
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public Enums.JobStatusForDeveloper StatusForDeveloper { get; set; }

        public string Technology { get; set; }

        public int Price { get; set; }


    }
}