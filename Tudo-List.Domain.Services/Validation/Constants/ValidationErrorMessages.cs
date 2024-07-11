using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Domain.Services.Validation.Constants
{
    public static class ValidationErrorMessages
    {
        private const string propertyUser = nameof(User);
        private const string propertyName = nameof(User.Name);
        private const string propertyEmail = nameof(User.Email);

        public static readonly string NameIsEmpty = ValidationHelper.GetEmptyPropertyMessage(propertyUser, propertyName);
        public static readonly string NameHaveInvalidLength = $"The {propertyUser} {propertyName} must have between {ValidationConstants.NameMinimumLength} and {ValidationConstants.NameMaximumLength} characters!";

        public static readonly string EmailIsEmpty = $"The {propertyUser} {propertyEmail} can't be Null, Empty String or Whitespace!";
    }
}
