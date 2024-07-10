using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class RegisterUserDto
    {
        [RequiredProperty]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredProperty]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [RequiredProperty]
        public string Password { get; set; }
    }
}
