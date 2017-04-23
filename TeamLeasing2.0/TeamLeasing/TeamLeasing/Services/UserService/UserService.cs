using TeamLeasing.Services.MailService;
using TeamLeasing.Services.UploadService;

namespace TeamLeasing.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ISendEmail _sendEmail;
        private readonly IUploadService _uploadService;

        public  UserService(ISendEmail sendEmail, IUploadService uploadService)
        {
            _sendEmail = sendEmail;
            _uploadService = uploadService;
        }

        public ISendEmail GetSendEmail()
        {
            return _sendEmail;
        }

        public IUploadService GetUpload()
        {
            return _uploadService;
        }
    }
}