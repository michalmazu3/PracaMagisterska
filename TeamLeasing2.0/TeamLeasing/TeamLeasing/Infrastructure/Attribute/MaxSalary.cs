using System.ComponentModel.DataAnnotations;
using TeamLeasing.ViewModels.Developer;

namespace TeamLeasing.Infrastructure.Attribute
{
    public class MaxSalary : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                string emailStr;
                var sendingOfferViewModel = (SendingOfferViewModel)validationContext.ObjectInstance;
                if (sendingOfferViewModel.MinSalary != null)
                {
                    if (sendingOfferViewModel.MaxSalary > sendingOfferViewModel.MinSalary)
                        return ValidationResult.Success;
                    if (sendingOfferViewModel.MaxSalary == null)
                    {
                        emailStr = value as string;
                        return string.IsNullOrEmpty(emailStr)
                            ? new ValidationResult("Wpisz maksymalny przedział płacowy")
                            : ValidationResult.Success;
                    }
                    emailStr = value as string;
                    return string.IsNullOrEmpty(emailStr)
                        ? new ValidationResult("Maksymalny przedział cenowy musi być wiekszy niż minimalny")
                        : ValidationResult.Success;
                }

                return ValidationResult.Success;
            }
        }
    

}