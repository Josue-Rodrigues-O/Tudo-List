using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Application.Dtos.User
{
    public record UpdatePasswordDto
    {
        [RequiredProperty]
        public int UserId { get; init; }

        [RequiredProperty]
        public string CurrentPassword { get; init; }

        [RequiredProperty]
        [Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)]
        public string NewPassword { get; init; }

        [RequiredProperty]
        [Length(UserValidationConstants.PasswordMinimumLength, UserValidationConstants.PasswordMaximumLength)]
        public string ConfirmNewPassword { get; init; }
    }
}
