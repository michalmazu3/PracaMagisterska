using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TeamLeasing.Services.MailService;

namespace TeamLeasing.Services.Mail
{
    public class SendEmail : ISendEmail
    {
        public IMessage EmailMessage { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }

        public SendEmail(IMessage emailMessage)
        {
            EmailMessage = emailMessage;
            SenderName = "TeamLeasingHelp";
            SenderEmail = "teamleasinghelp@gmail.com";
            EmailPassword = "admin123$";
            SmtpHost = "smtp.gmail.com";
            SmtpPort = 587;
        }

        public IMessage CreateMessage(string to, string message, string subject)
        {
            EmailMessage.Message.From.Add(new MailboxAddress(SenderName, SenderEmail));
            EmailMessage.Message.To.Add(new MailboxAddress("", to));
            EmailMessage.Message.Subject = subject;
            EmailMessage.Message.Body = new TextPart("plain") { Text = message };

            return EmailMessage;
        }

        public void Send(IMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP

                 client.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTlsWhenAvailable);
                 client.Authenticate(SenderEmail, EmailPassword);
                 client.Send(emailMessage.Message);
                 client.Disconnect(true);
            }
        }


    }
}