using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Application.Dtos.User
{
    public record RegisterUserDto
    {
        [RequiredProperty]
        [EmailAddress]
        public string Email { get; init; }

        [RequiredProperty]
        [Length(UserValidationConstants.NameMinimumLength, UserValidationConstants.NameMaximumLength)]
        public string Name { get; init; }

        [RequiredProperty]
        [Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)]
        public string Password { get; init; }
    }
}
