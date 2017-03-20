using MimeKit;

namespace TeamLeasing.Services
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