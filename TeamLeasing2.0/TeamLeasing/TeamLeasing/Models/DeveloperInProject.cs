namespace TeamLeasing.Models
{
    public class DeveloperInProject
    {
        public Enums.JobStatusForDeveloper StatusForDeveloper { get; set; }
        public int DeveloperUserId { get; set; }
        public virtual DeveloperUser DeveloperUser { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}