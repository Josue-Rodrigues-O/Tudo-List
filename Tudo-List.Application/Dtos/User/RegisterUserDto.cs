using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Application.Dtos.User
{
    public record RegisterUserDto(
        [RequiredProperty][EmailAddress] string Email,
        [RequiredProperty][Length(UserValidationConstants.NameMinimumLength, UserValidationConstants.NameMaximumLength)] string Name,
        [RequiredProperty][Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)] string Password,
        [RequiredProperty][Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)] string ConfirmPassword
    );
}
