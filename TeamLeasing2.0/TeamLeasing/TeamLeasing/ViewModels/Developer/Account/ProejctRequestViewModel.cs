using TeamLeasing.Models;

namespace TeamLeasing.ViewModels.Developer.Account
{
    public class ProejctRequestViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public Enums.JobStatusForDeveloper StatusForDeveloper { get; set; }


        public string ProjectType { get; set; }

        public int Budget { get; set; }
    }
}