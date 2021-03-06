﻿using TeamLeasing.Services.Mail;

namespace TeamLeasing.Services.MailService
{
    public interface ISendEmail : IMailConfiguration
    {
        IMessage EmailMessage { get; set; }
        IMessage CreateMessage( string to, string message, string subject="need help");
        void Send(IMessage message);
    }
}