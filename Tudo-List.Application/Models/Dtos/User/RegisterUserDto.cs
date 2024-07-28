using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class RegisterUserDto
    {
        [RequiredProperty]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredProperty]
        [Length(UserValidationConstants.NameMinimumLength, UserValidationConstants.NameMaximumLength)]
        public string Name { get; set; }

        [RequiredProperty]
        [Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)]
        public string Password { get; set; }
    }
}
