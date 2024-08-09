using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Dtos.Login
{
    public record LoginRequestDto
    {
        [RequiredProperty]
        [EmailAddress]
        public string Email { get; init; }

        [RequiredProperty]
        public string Password { get; init; }
    }
}
