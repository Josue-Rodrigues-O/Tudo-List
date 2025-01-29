using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Dtos.User
{
    public record UpdateUserDto([RequiredProperty] int UserId, string? NewName);
}
