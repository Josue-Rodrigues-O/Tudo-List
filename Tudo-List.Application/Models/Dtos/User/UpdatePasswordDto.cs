using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class UpdatePasswordDto
    {
        [RequiredProperty]
        public int UserId { get; set; }

        [RequiredProperty]
        public string CurrentPassword { get; set; }

        [RequiredProperty]
        [MinLength(ValidationConstants.PasswordMinimumLength)]
        public string NewPassword { get; set; }

        [RequiredProperty]
        [MinLength(ValidationConstants.PasswordMinimumLength)]
        public string ConfirmNewPassword { get; set; }
    }
}
