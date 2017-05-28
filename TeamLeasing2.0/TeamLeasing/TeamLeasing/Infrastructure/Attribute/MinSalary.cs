using System.ComponentModel.DataAnnotations;
using TeamLeasing.ViewModels.Developer;

namespace TeamLeasing.Infrastructure.Attribute
{
    public class MinSalary : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string emailStr;
            var sendingOfferViewModel = (SendingOfferViewModel) validationContext.ObjectInstance;
            if (sendingOfferViewModel.MaxSalary != null)
            {
                if (sendingOfferViewModel.MinSalary < sendingOfferViewModel.MaxSalary)
                    return ValidationResult.Success;
                if (sendingOfferViewModel.MinSalary == null)
                {
                    emailStr = value as string;
                    return string.IsNullOrEmpty(emailStr)
                        ? new ValidationResult("Wpisz minimalny przedział płacowy")
                        : ValidationResult.Success;
                }
                emailStr = value as string;
                return string.IsNullOrEmpty(emailStr)
                    ? new ValidationResult("Minimalny przedział cenowy musi być mniejszy niż maksymalny")
                    : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}