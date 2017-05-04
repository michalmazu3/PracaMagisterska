namespace TeamLeasing.Models
{
    public class DeveloperUserJob
    {
        public Enums.JobStatusForDeveloper StatusForDeveloper { get; set; }

        public int DeveloperUserId { get; set; }
        public virtual DeveloperUser DeveloperUser { get; set; }

        public int JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}