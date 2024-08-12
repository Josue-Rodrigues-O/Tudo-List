using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Dtos.User
{
    public record UpdateUserDto
    {
        [RequiredProperty]
        public int UserId { get; init; }

        public string? NewName { get; init; }
    }
}
