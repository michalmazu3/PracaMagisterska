using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TeamLeasing.ViewModels.Developer.Account
{
    public class Photo
    {
        [Display(Name = "Zdjęcie")]
        public string PhotoPath { get; set; }


        [Required(ErrorMessage = "Please Upload a Valid Photo File")]
        [DataType(DataType.Upload)]
        [Display(Name = "Zdjęcie")]
        [FileExtensions(Extensions = "jpg")]
        public IFormFile PhotoFile { get; set; }
    }
}