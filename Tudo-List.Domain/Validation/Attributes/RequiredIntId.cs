using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Domain.Validation.Attributes
{
    public class RequiredIntId() : ValidationAttribute("{0}")
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int intValue && intValue > decimal.Zero)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
