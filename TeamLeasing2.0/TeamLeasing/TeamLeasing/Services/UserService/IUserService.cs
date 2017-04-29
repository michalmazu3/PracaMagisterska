using TeamLeasing.Services.MailService;

namespace TeamLeasing.Services.UploadService
{
    public interface IUserService
    {
        IUploadService UploadService { get; }
        ISendEmail SendEmailService { get; }
    }
}