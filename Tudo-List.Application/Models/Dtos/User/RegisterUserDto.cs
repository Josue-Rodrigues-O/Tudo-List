using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}
