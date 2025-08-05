using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Application.Dtos.User
{
    public record UpdatePasswordDto(
        [RequiredProperty] int UserId,
        [RequiredProperty] string CurrentPassword,
        [RequiredProperty][Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)] string NewPassword,
        [RequiredProperty][Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)] string ConfirmNewPassword
    );
}
