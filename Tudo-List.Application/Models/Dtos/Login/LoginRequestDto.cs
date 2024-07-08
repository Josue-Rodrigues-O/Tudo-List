using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Application.Models.Dtos.Login
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = ValidationErrorMessages.RequiredEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationErrorMessages.RequiredPassword)]
        public string Password { get; set; }
    }
}
