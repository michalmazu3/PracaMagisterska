using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace TeamLeasing.Services.UploadService
{
    public class UploadService : IUploadService
    {
        private readonly IHostingEnvironment _environment;

        public UploadService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadPhotoFile(string name, string surname, IFormFile photoFile, bool isFileExist=false)
        {
            var pathToFile = $"/UploadFile/Photo/{surname.Trim()}_{name.Trim()}.jpg";
            // var uploadPhoto = Path.Combine(_environment.WebRootPath, pathToFile);
            var uploadPhotoPath = _environment.WebRootPath + pathToFile;
            CheckIsFileExist(isFileExist, uploadPhotoPath);
            await CopyFile(photoFile, uploadPhotoPath);
            return pathToFile;
        }

        public async Task<string> UploadCvFile(string name, string surname, IFormFile cvFile, bool isFileExist=false)
        {
            var pathToFile = $"/UploadFile/Cv/{surname.Trim()}_{name.Trim()}.pdf";
          //  var uploadCv = Path.Combine(_environment.WebRootPath, pathToFile);
            var uploadCvPath = _environment.WebRootPath + pathToFile;

            CheckIsFileExist(isFileExist, uploadCvPath);
             
            await CopyFile(cvFile, uploadCvPath);
            return pathToFile;
        }

        private void CheckIsFileExist(bool isFileExist, string pathToFile)
        {
            if (isFileExist)
            {
                System.IO.File.Delete(pathToFile);
            }
        }

        private async Task CopyFile(IFormFile file, string path)
        {
            if (file != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    
                     await file.CopyToAsync(fileStream);
                }
            }
            else
            {
                throw new Exception(message: "Bład wgrywania pliku");

            }
        }

    }
}