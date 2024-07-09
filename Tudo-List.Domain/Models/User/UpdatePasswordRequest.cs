using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Validation.Attributes;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Domain.Models.User
{
    public class UpdatePasswordRequest
    {
        [RequiredIntId]
        public int UserId { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.RequiredCurrentPassword)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.RequiredNewPassword)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.RequiredConfirmNewPassword)]
        public string ConfirmNewPassword { get; set; }
    }
}
