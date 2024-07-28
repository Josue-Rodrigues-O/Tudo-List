using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Services.Validation.Constants
{
    public static class UserValidationConstants
    {
        public const string NameProperty = nameof(User.Name);
        public const string EmailProperty = nameof(User.Email);
        public const string PasswordProperty = "Password";

        public const int NameMinimumLength = 2;
        public const int NameMaximumLength = 100;

        public const int PasswordMinimumLength = 8;
        public const int PasswordMaximumLength = 256;
    }
}
