using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Models.Dtos.Login
{
    public class LoginRequestDto
    {
        [RequiredProperty]
        public string Email { get; set; }

        [RequiredProperty]
        public string Password { get; set; }
    }
}
