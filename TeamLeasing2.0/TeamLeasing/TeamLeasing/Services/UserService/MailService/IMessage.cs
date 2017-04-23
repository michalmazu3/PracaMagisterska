using MimeKit;

namespace TeamLeasing.Services.Mail
{
    public interface IMessage
    {
        MimeMessage Message { get; set; }
    }
}