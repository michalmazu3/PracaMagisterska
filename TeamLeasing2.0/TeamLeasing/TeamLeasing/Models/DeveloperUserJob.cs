namespace TeamLeasing.Models
{
    public class DeveloperUserJob
    {
        public int DeveloperUserId { get; set; }
        public virtual DeveloperUser DeveloperUser { get; set; }

        public int JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}