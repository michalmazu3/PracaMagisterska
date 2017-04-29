using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TeamLeasing.ViewModels.Developer.Account
{
    public class Cv
    {
        [Display(Name = "Cv")]
        public string CvPath { get; set; }

        [Required(ErrorMessage = "Please Upload a Valid Cv File")]
        [DataType(DataType.Upload)]
        [Display(Name = "Cv")]
        [FileExtensions(Extensions = "pdf")]
        public IFormFile CvFile { get; set; }
    }
}