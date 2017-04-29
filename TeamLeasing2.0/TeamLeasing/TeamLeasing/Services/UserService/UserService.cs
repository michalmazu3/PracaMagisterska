using TeamLeasing.Services.MailService;

namespace TeamLeasing.Services.UploadService
{
    public class UserService : IUserService
    {
        private readonly IUploadService _uploadService;
        private readonly ISendEmail _sendEmail;

        public UserService(IUploadService uploadService, ISendEmail sendEmail)
        {
            _uploadService = uploadService;
            _sendEmail = sendEmail;
        }


        public IUploadService UploadService { get { return _uploadService; } }
        public ISendEmail SendEmailService { get { return _sendEmail; } }
    }
}