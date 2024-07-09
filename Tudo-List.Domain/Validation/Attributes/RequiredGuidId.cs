using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Domain.Validation.Attributes
{
    public class RequireGuidId() : ValidationAttribute(ValidationErrorMessages.RequiredUserId)
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guidValue && guidValue != Guid.Empty)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
