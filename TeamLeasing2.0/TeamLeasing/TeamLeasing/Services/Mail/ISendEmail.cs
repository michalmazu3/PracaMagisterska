namespace TeamLeasing.Services.Mail
{
    public interface ISendEmail : IMailConfiguration
    {
        IMessage EmailMessage { get; set; }
        IMessage CreateMessage( string to, string message, string subject="need help");
        void Send(IMessage message);
    }
}