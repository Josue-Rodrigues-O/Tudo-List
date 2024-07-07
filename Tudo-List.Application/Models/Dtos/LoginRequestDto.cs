using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Application.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
