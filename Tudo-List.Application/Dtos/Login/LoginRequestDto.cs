using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Dtos.Login
{
    public record LoginRequestDto([EmailAddress][RequiredProperty] string Email, [RequiredProperty] string Password);
}
