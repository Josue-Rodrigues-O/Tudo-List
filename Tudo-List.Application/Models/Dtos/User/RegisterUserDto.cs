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
        [Length(ValidationConstants.NameMinimumLength, ValidationConstants.NameMaximumLength)]
        public string Name { get; set; }

        [RequiredProperty]
        public string Password { get; set; }
    }
}
