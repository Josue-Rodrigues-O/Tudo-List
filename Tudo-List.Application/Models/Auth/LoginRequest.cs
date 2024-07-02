using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Application.Models.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
