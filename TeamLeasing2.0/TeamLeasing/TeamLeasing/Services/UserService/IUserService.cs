using TeamLeasing.Services.MailService;
using TeamLeasing.Services.UploadService;

namespace TeamLeasing.Services.UserService
{
    public interface IUserService
    {
        ISendEmail GetSendEmail();
        IUploadService GetUpload();


    }
}