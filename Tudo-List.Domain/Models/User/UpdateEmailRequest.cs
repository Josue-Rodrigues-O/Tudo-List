using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Validation.Attributes;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Domain.Models.User
{
    public class UpdateEmailRequest
    {
        [RequiredIntId]
        public int UserId { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.RequiredEmail)]
        public string NewEmail { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.RequiredCurrentPassword)]
        public string CurrentPassword { get; set; }
    }
}
