using System.ComponentModel.DataAnnotations;
using TeamLeasing.ViewModels.Developer;

namespace TeamLeasing.Infrastructure.Attribute
{
    public class RequiredIfMinAndMaxSalaryNull : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var sendingOfferViewModel = (SendingOfferViewModel) validationContext.ObjectInstance;
            if ((sendingOfferViewModel.MinSalary != null || sendingOfferViewModel.MaxSalary != null)
                || (sendingOfferViewModel.MinSalary == null && sendingOfferViewModel.MaxSalary == null))
                return ValidationResult.Success;
            var emailStr = value as string;
            return string.IsNullOrEmpty(emailStr)
                ? new ValidationResult("Wpisz wynagrodzenie, albo uzupełnij widełki płacowe")
                : ValidationResult.Success;
        }
    }
}