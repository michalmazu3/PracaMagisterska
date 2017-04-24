namespace TeamLeasing.Services.Mail
{
    public interface IMailConfiguration
    {
        string SenderName { get; set; }
        string SenderEmail { get; set; }
        string EmailPassword { get; set; }
        string SmtpHost { get; set; }
        int SmtpPort { get; set; }
    }
}