using System;
using System.ComponentModel.DataAnnotations;
using TeamLeasing.ViewModels.Developer;

namespace TeamLeasing.Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfNotNull : ValidationAttribute
    {
        private readonly decimal? _obj;

        public RequiredIfNotNull(decimal? obj)
        {
            _obj = obj;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var sendingOfferViewModel = (SendingOfferViewModel) validationContext.ObjectInstance;
            if (_obj == null)
                return ValidationResult.Success;
            var emailStr = value as string;
            return string.IsNullOrEmpty(emailStr)
                ? new ValidationResult("Value is required.")
                : ValidationResult.Success;
        }
    }
}

