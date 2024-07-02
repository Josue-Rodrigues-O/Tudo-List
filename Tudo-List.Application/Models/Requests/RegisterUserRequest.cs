using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Application.Models.Requests
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Email is Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}
