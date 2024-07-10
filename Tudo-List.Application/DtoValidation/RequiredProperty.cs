using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Application.DtoValidation
{
    public class RequiredProperty : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool isValidProperty = value switch
            {
                string strValue => IsValidString(strValue),
                int intValue => IsValidInt(intValue),
                Guid guidValue => IsValidGuid(guidValue),
                _ => false,
            };

            return isValidProperty
                    ? ValidationResult.Success
                    : new ValidationResult(ValidationHelper.GetInvalidPropertyMessage(validationContext.DisplayName));
        }

        private static bool IsValidString(string value)
            => value.ContainsValue();

        private static bool IsValidInt(int value)
            => value > uint.MinValue;

        private static bool IsValidGuid(Guid value)
            => value != Guid.Empty;
    }
}
