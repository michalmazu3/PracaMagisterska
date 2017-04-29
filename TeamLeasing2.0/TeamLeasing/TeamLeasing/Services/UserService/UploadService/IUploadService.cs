using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TeamLeasing.Services.UploadService
{
    public interface IUploadService
    {
        Task<string> UploadPhotoFile(string name, string surname, IFormFile photoFile, bool isFileExist=false);
        Task<string> UploadCvFile(string Name, string Surname, IFormFile cvFile, bool isFileExist = false);
    }
}