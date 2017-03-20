using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace UnitTest
{
    public static class Constants
    {
        public static string SenderName => "gosia";
        public static string SenderEmail => "gosiamazur1422@gmail.com";
        public static string EmailPassword => "rysiek1422";
        public static string SmtpHost => "smtp.gmail.com";
        public static int SmtpPort => 587;
    }
    public static class EmailService 
    {
        public static void SendEmailAsync(string recipientEmail, string subject, string message)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(Constants.SenderName, Constants.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress("", recipientEmail));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message,
            };

            using (var client = new SmtpClient())
            {
                client.Connect(Constants.SmtpHost, Constants.SmtpPort, SecureSocketOptions.StartTlsWhenAvailable);
                client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP
                client.Authenticate(Constants.SenderEmail, Constants.EmailPassword);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }


            //using (var client = new SmtpClient())
            //{

            //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            //    client.Connect(Constants.SmtpHost, Constants.SmtpPort, false);

            //    client.AuthenticationMechanisms.Remove("XOAUTH2");

            //    // Note: only needed if the SMTP server requires authentication
            //    client.Authenticate(Constants.SenderEmail, Constants.EmailPassword);

            //    client.Send(mimeMessage);

            //    client.Disconnect(true);
            //    return Task.FromResult(0);



            //}
        }
    }
}
