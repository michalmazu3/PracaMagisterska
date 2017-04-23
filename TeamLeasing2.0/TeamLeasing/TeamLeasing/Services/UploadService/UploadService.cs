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

        public async Task<string> UploadPhotoFile(string name, string surname, IFormFile photoFile)
        {
            var pathToFile = $"/UploadFile/Photo/{surname.Trim()}_{name.Trim()}.jpg";
            // var uploadPhoto = Path.Combine(_environment.WebRootPath, pathToFile);
            var uploadPhoto = _environment.WebRootPath + pathToFile;

            await CopyFile(photoFile, uploadPhoto);
            return pathToFile;
        }

        public async Task<string> UploadCvFile(string name, string surname, IFormFile cvFile)
        {
            var pathToFile = $"/UploadFile/Cv/{surname.Trim()}_{name.Trim()}.pdf";
          //  var uploadCv = Path.Combine(_environment.WebRootPath, pathToFile);
            var uploadCv = _environment.WebRootPath + pathToFile;
            
            await CopyFile(cvFile, uploadCv);
            return pathToFile;
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