﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TeamLeasing.Models;

namespace TeamLeasing.ViewModels
{
    public class RegistrationDeveloperViewModel
    {
        [Required(ErrorMessage = "Pole email jest wymagane!")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres email!")]
        public string Email { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nazwa zbyt krótka minimum 5 znaków")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [RegularExpression(@"^.*(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*\(\)_\-+=]).*$", ErrorMessage =
            "Wprowadź jedną dużą litere, cyfre i znak")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Hasło  za krótkie")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Podaj imię")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj nazwisko")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Podaj miejscowość")]
        [Display(Name = "Miejscowość")]
        public string City { get; set; }

        [Required(ErrorMessage = "Podaj numer telefonu")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Podaj date urodzenia")]
        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date, ErrorMessage = "Wprowadz date w formacie rok.miesiac.dzień")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Studia")]
        public string University { get; set; }

        [Display(Name = "Doświadczenie w latach")]
        [Range(0, 20, ErrorMessage = "Podaj wartość z zakreu 0 - 20")]
        public int? Experience { get; set; }


        [Display(Name = "Cv")]
        public string Cv { get; set; }

        [DataType(DataType.Upload)]
        //  [FileExtensions(Extensions = "pdf", ErrorMessage = "Wgraj plik w formacie pdf")]
        public IFormFile CvFile { get; set; }

        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }

        public IFormFile PhotoFile { get; set; }


        [Required(ErrorMessage = "Wybierz technologie/język")]
        public string ChoosenTechnology { get; set; }

        [Required(ErrorMessage = "Określ swój poziom")]
        public Enums.Level ChoosenLevel { get; set; }

        [Required(ErrorMessage = "Podaj czy ukończyłeś studia")]
        public Enums.IsFinishedUniversity ChoosenIsFinishedUnivesity { get; set; }

        [Required(ErrorMessage = "Podaj województwo")]
        public string ChoosenProvince { get; set; }
    }
}