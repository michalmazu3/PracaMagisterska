using MimeKit;

namespace TeamLeasing.Services
{
    public interface IMessage
    {
        MimeMessage Message { get; set; }
    }
}