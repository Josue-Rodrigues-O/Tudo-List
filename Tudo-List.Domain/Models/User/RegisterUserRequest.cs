using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Domain.Models.User
{
    public class RegisterUserRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationErrorMessages.RequiredEmail)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationErrorMessages.RequiredName)]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationErrorMessages.RequiredPassword)]
        public string Password { get; set; }
    }
}
