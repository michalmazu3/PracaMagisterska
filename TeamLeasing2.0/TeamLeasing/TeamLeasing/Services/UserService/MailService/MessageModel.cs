using MimeKit;

namespace TeamLeasing.Services.Mail
{
    public class MessageModel : IMessage
    {
        public MimeMessage Message { get; set; }

        public MessageModel()
        {
            Message = new MimeMessage();
        }
    }
}